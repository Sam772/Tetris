using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino {
    I = 0, 
    J = 1, 
    L = 2, 
    O = 3, 
    S = 4, 
    T = 5, 
    Z = 6
}

[System.Serializable]
public struct TetrominoData {
    public Tile tile;
    public Tetromino tetromino;

    public Vector2Int[] cells { get; private set; }
    public Vector2Int[,] wallKicks { get; private set; }

    public void Initialize() {
        cells = Data.Cells[tetromino];
        wallKicks = Data.WallKicks[tetromino];
    }
}