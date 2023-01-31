using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour {
    [SerializeField] private TMP_Text _scoretext;
    [SerializeField] private TMP_Text _highScoretext;
    [SerializeField] private Board _board;

    public void SetScoreText(string scoretext) {
        _scoretext.text = "Score: " + scoretext;
    }

    public void SetGameOverScore(string scoretext, string highscore) {
        _highScoretext.gameObject.SetActive(true);
        _scoretext.text = "You Lose! \nYour Score Was: " + scoretext;
        _highScoretext.text = "\nYour highest score was: " + highscore;
    }

    public void SetHighScoreTextOff() {
        _highScoretext.gameObject.SetActive(false);
    }
    
    private void SetHighScoreTextOn() {
        _highScoretext.gameObject.SetActive(true);
    }
}
