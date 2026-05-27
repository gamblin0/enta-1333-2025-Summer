using UnityEngine;

[System.Serializable]
public struct GridNode
{
    public string Name; // an index to keep track and organize nodes
    public Vector3 WorldPosition;
    public bool Walkable;
    public int Weight;
}
