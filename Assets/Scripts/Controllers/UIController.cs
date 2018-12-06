using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
  TextMeshProUGUI _endRunScoreText;
  TextMeshProUGUI _scoreText;
  Toolbox _toolbox;
  GameObject _endRunPanel;

  void Start () {
    _endRunPanel = GameObject.Find("EndRun Panel").gameObject;
    _endRunScoreText = GameObject.Find("EndRun Text").gameObject.GetComponent<TextMeshProUGUI>();
    _scoreText = GameObject.Find("Score").gameObject.GetComponent<TextMeshProUGUI>();
    _toolbox = Toolbox.Instance;

    _toolbox.EnemyDied.AddListener(OnEnemyDied);
    _toolbox.GameStart.AddListener(OnGameStart);
    _toolbox.PlayerDied.AddListener(OnPlayerDied);
  }

  void Update () {
    if (_endRunPanel.activeInHierarchy) {
      if (Input.GetKeyDown("r")) {
        _toolbox.GameStart.Invoke();
      }
    }
  }

  void OnEnemyDied () {
    _scoreText.SetText(_toolbox.EnemiesKilled.ToString());
  }

  void OnGameStart () {
    _endRunPanel.SetActive(false);
  }

  void OnPlayerDied () {
    _endRunPanel.SetActive(true);
    _endRunScoreText.SetText("Score: {0}\r\nHigh Score: {1}", _toolbox.EnemiesKilled, _toolbox.MostEnemiesKilled);
    _scoreText.SetText("0");
  }
}
