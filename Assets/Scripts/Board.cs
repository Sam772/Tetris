using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour {

    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private Tilemap _tileMap;
    public GameState State { get; private set; }
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public RectInt Bounds {
        get {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    public int _score { get; private set; }

    private void Awake() {
        activePiece = GetComponentInChildren<Piece>();

        _score = 0;

        for (int i = 0; i < tetrominoes.Length; i++) {
            tetrominoes[i].Initialize();
        }
    }

    void Start() => ChangeState(GameState.SETUP);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.SETUP:
                HandleStarting();
                break;
            case GameState.RUNNING:
                HandleRunning();
                break;
            case GameState.LOSE:
                HandleLosing();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    public void HandleStarting() {
        // no setup required

        ChangeState(GameState.RUNNING);
    }

    public void HandleRunning() {
        SpawnPiece();
    }

    public void HandleLosing() {
        GameOver();
    }
    

    public void SpawnPiece() {

        if (State != GameState.LOSE) {
            
            int random = UnityEngine.Random.Range(0, tetrominoes.Length);
            TetrominoData data = tetrominoes[random];

            activePiece.Initialize(this, spawnPosition, data);

            if (IsValidPosition(activePiece, spawnPosition)) {
                Set(activePiece);
            } else {
                ChangeState(GameState.LOSE);
            }
        }
    }

    public void GameOver() {
        _tileMap.ClearAllTiles();

        State = GameState.LOSE;

        _gameUI.SetGameOverScore(_score.ToString());
    }

    public void Set(Piece piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            _tileMap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            _tileMap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position) {
        RectInt bounds = Bounds;

        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }

            if (_tileMap.HasTile(tilePosition)) {
                return false;
            }
        }

        return true;
    }

    public void ClearLines() {
        RectInt bounds = Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax) {

            if (IsLineFull(row)) {
                LineClear(row);
                RowClearUpdateScore();
            } else {
                row++;
            }
        }
    }

    public bool IsLineFull(int row) {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++) {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!_tileMap.HasTile(position)) {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row) {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++) {
            Vector3Int position = new Vector3Int(col, row, 0);
            _tileMap.SetTile(position, null);
        }

        while (row < bounds.yMax) {
            for (int col = bounds.xMin; col < bounds.xMax; col++) {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = _tileMap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                _tileMap.SetTile(position, above);
            }

            row++;
        }
    }

    public void PieceSetUpdateScore() {
        _score += 4;
        _gameUI.SetScoreText(_score.ToString());
    }

    public void RowClearUpdateScore() {
        _score += 100;
        _gameUI.SetScoreText(_score.ToString());
    }

}

public enum GameState {
    SETUP = 0,
    RUNNING = 1,
    LOSE = 2,
}