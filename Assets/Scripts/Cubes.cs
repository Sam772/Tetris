using System;
using UnityEngine.Tilemaps;
using UnityEngine;

[Serializable]
public struct CubeData {
    [SerializeField] private Cubes _cubes;
    public Cubes Cubes => _cubes;
    [SerializeField] private Tile _tile;
    public Tile Tile => _tile;
    public Vector2Int[] Cells { get; private set; }

    public void Initialise() {
        Cells = TetrisData.Cells[_cubes];
    }

}

[Serializable]
public enum Cubes {
    I = 0,
    O = 1,
    T = 2,
    J = 3,
    L = 4,
    S = 5,
    Z = 6
}