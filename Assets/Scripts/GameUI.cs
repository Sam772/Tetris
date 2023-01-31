using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour {
    [SerializeField] private TMP_Text _scoretext;
    [SerializeField] private Button _playAgainButton;

    public void SetScoreText(string scoretext) {
        _scoretext.text = "Score: " + scoretext;
    }

    public void SetGameOverScore(string scoretext) {
        _scoretext.text = "You Lose! Your Score Was: " + scoretext;
    }

    public void PlayAgainButton() {
        // let the user play again
    }
}
