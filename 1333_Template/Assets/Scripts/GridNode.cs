using UnityEngine;

[System.Serializable]
public struct GridNode
{
    public TerrainType TerrainType;
    public Color GizmoColor;

    public string Name; // an index to keep track and organize nodes
    public Vector3 WorldPosition;
    public bool Walkable => TerrainType != null && TerrainType.Walkable;
    public int Weight => TerrainType == null ? 1: TerrainType.Weight;
}
