using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGUI : MonoBehaviour {
    [SerializeField] private TMP_Text _scoretext;

    public void SetScoreText(string scoretext) {
        _scoretext.text = "Score: " + scoretext;
    }

    public void SetGameOverScore(string scoretext) {
        _scoretext.text = "Your Score Was: " + scoretext;
    }
}
