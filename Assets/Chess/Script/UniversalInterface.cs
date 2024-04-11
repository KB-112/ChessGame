using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct GridChess
{
    [Range(0, 7)]
    public int xCoord;
    [Range(0, 7)]
    public int yCoord;
}
[System.Serializable]
public struct SliderValue
{
    [Range(1, 8)]
    public int xCoordinates;
    [Range(1, 8)]
    public int yCoordinates;
}
public interface IStoreChessPiece {

  public  Dictionary<GameObject, string> storeObject();
}

