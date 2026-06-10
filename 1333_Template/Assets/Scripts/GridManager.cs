using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //variable to allow us to plug in our GridSettings scriptable Object
    [SerializeField] private GridSettings _gridSettings;
    public GridSettings GridSettings => _gridSettings;

    private GridNode[,] gridNodes;

#if UNITY_EDITOR
    [Header("Debug for editor playmode only")]
    [SerializeField] private List<GridNode> Allnodes = new();
#endif

    public bool IsInitialized { get; private set; } = false;

    public void InitializeGrid()
    {
        //initializing our array of gridnode structs using the dimensions from the scriptable objects
        gridNodes = new GridNode[_gridSettings.GridsizeX, _gridSettings.GridsizeY];

        //nest for loop to iterate over all GridNodes
        //each grid position, instantiate a new GridNode struct, give it some default values and add it to our GridNodes 2D array
        for(int x = 0; x < _gridSettings.GridsizeX; x++)
        {
            for(int y = 0;  y < _gridSettings.GridsizeY; y++)
            {
                Vector3 worldPos = _gridSettings.UseXZPlane ? new Vector3(x, 0, y) * _gridSettings.NodeSize : new Vector3(x, y, 0) * _gridSettings.NodeSize;
                /*
                 * if is using xz plane worldpos = new vector3 (x,0,y)* gridsettings node size
                 *[(condition) ? result if true : result if false]
                */

                GridNode node = new GridNode
                {
                    Name = $"Cell_{(x + _gridSettings.GridsizeX * x + y)}",
                    WorldPosition = worldPos,
                    //Walkable = true, //Default all nodes to walkable, modified later
                    //Weight = 1 //default weight, modified later

                };

                gridNodes[x,y] = node;
            }
        }
        IsInitialized = true;
    }
#if UNITY_EDITOR //only works in editor, doesnt work on build
    private void PopulateDebugList()
    {
        //clear our debug list of GridNodes 
        //then for each already existing GridNode, create a new GridNode, grab it's info, set it to newly created GridNode
        //for debug purposes
        Allnodes.Clear();
        for(int x = 0; x < _gridSettings.GridsizeX;x++)
        {
            for(int y = 0; y < _gridSettings.GridsizeY;y++)
            {
                GridNode node = gridNodes[x, y];
                Allnodes.Add(new GridNode
                {
                    Name = $"Cell_{x}+{y}",
                    WorldPosition = node.WorldPosition,
                    //Walkable = node.Walkable,
                    //Weight = node.Weight
                });
            }
        }
    }
#endif

    //Function to retrieve GridNode data efficiently
    // || is or
    public GridNode GetNode(int x, int y)
    {
        //first check if the function arguments are out of bounds of the grid
        //otherwise return proper GridNode
        if (x < 0 || x >= _gridSettings.GridsizeX || y < 0 || y >= _gridSettings.GridsizeY)
        {
            throw new System.IndexOutOfRangeException("Grid node indices out of range");
        }
            
        return gridNodes[x, y];
        
    }

    //public void SetWalkable(int x, int y, bool walkable)
    //{
    //    gridNodes[x, y].Walkable = walkable;
    //}

    private void OnDrawGizmos()
    {
        if (gridNodes == null || _gridSettings == null) return;

        Gizmos.color = Color.green;

        //draw the gridnode gizmos, size is 0.9  of the gridnode for visual clarity, so the stuff dont overlap
        for (int x = 0; x < _gridSettings.GridsizeX; x++)
        {
            for (int y = 0; y < _gridSettings.GridsizeY; y++)
            {
                GridNode node = gridNodes[x, y];
                Gizmos.color = node.Walkable ? Color.green : Color.red;
                Gizmos.DrawWireCube(node.WorldPosition, Vector3.one * GridSettings.NodeSize * 0.9f);
            }
        }
    }


    //Create a custom editor button that when pressed, calls PopulateDebugList and refreshes the Editor GUI
    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            //first draw the normal inspector GUI
            DrawDefaultInspector();

            //then look at the GridManager class this is attached to and call the PopulateDebiugList function
            GridManager grid = (GridManager)target;

            if(grid.IsInitialized)
            {
                if(GUILayout.Button("Refresh Grid Debug View"))
                {
                    grid.PopulateDebugList();
                }
            }
        }
    }
}
