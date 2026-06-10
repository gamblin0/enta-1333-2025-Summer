using UnityEngine;

//This is a ScriptableObject for easy customization of grid dimentions and orientation
[CreateAssetMenu(fileName = "GridSettings", menuName = "Game/GridSettings")]

public class GridSettings : ScriptableObject
{
    [SerializeField] private int _gridsizeX =10;
    [SerializeField] private int _gridsizeY = 10;
    [SerializeField] private float _nodeSize = 1;
    [SerializeField] private bool _useXZPlane = true;
    [SerializeField] private bool _AllowDiagonal;

    public int GridsizeX => _gridsizeX;
    public int GridsizeY => _gridsizeY;
    public float NodeSize => _nodeSize;
    public bool UseXZPlane => _useXZPlane;

    


}
