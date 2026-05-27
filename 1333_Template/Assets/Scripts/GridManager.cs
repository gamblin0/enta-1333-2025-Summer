using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //variable to allow us to plug in our GridSettings scriptable Object
    [SerializeField] private GridSettings _gridSettings;
    public GridSettings GridSettings => _gridSettings;

    private GridNode[,] gridnodes;

#if UNITY_EDITOR
    [Header("Debug for editor playmode only")]
    [SerializeField] private List<GridNode> Allnodes = new();
#endif

    public bool IsInitialized { get; private set; } = false;

    public void InitializeGrid()
    {
        //initializing our array of gridnode structs using the dimensions from the scriptable objects
        gridnodes = new GridNode[_gridSettings.GridsizeX, _gridSettings.GridsizeY];

        //nest for loop to iterate over all GridNodes
        for(int x = 0; x < _gridSettings.GridsizeX; x++)
        {
            for(int y = 0;  y < _gridSettings.GridsizeY; y++)
            {

            }
        }
    }
}
