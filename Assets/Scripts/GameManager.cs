using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private CubeData[] _cubeData;
    [SerializeField] private Tilemap _tileMap;
    public Piece ActivePiece { get; private set; }
    public Vector3Int SpawnPosition;
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    [SerializeField] private string _sceneName;
    [SerializeField] private string _nextSceneName;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    public GameState State { get; private set; }

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
            case GameState.WIN:
                HandleWinning();
                break;
            case GameState.LOSE:
                HandleLosing();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    private void HandleStarting() {

        ActivePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < _cubeData.Length; i++) {
            _cubeData[i].Initialise();
        }

        SpawnPiece();

        ChangeState(GameState.RUNNING);
    }

    private void SpawnPiece() {
        int random = UnityEngine.Random.Range(0, _cubeData.Length);
        CubeData cubeData = _cubeData[random];

        ActivePiece.Initalise(this, SpawnPosition, cubeData);

        SetPiece(ActivePiece);
    }

    private void SetPiece(Piece piece) {
        for (int  i = 0; i < piece.Cells.Length; i++) {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            _tileMap.SetTile(tilePosition, piece.CubeData.Tile);
        }
    }

    private void HandleRunning() {

    }

    private void HandleWinning() {
        _winScreen.SetActive(true);
        _loseScreen.SetActive(false);
    }

    private void HandleLosing() {
        _winScreen.SetActive(false);
        _loseScreen.SetActive(true);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(_nextSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ReloadScene();
    }
}

[Serializable]
public enum GameState {
    SETUP = 0,
    RUNNING = 1,
    WIN = 2,
    LOSE = 3,
}