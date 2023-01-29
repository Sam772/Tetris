using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    [SerializeField] private GameManager _gameManager;
    public CubeData CubeData { get; private set; }
    public Vector3Int Position { get; private set; }
    public Vector3Int[] Cells { get; private set; }

    public void Initalise(GameManager gameManager, Vector3Int position, CubeData cubedata) {
        _gameManager = gameManager;
        Position = position;
        CubeData = cubedata;

        if (Cells == null) {
            Cells = new Vector3Int[CubeData.Cells.Length];
        }

        for (int i = 0; i < CubeData.Cells.Length; i++) Cells[i] = (Vector3Int) CubeData.Cells[i];
        
    }
}
