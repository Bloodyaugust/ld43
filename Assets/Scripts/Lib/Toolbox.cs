using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;
using TMPro;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {}

  public GameObject ScoreText;
  public GameObject EndScorePanel;
  public TextMeshProUGUI EndScoreText;
  public UnityEvent BulletHit;
  public UnityEvent EnemyDied;
  public UnityEvent GameStart;
  public UnityEvent PlayerDied;

  GameObject _player;
  int _enemiesKilled = 0;
  int _mostEnemiesKilled = 0;
  PlayerController _playerController;
  TextMeshProUGUI _scoreText;

	void Awake () {
    _player = GameObject.Find("abraham");
    _playerController = _player.GetComponent<PlayerController>();

    BulletHit = new UnityEvent();
    EnemyDied = new UnityEvent();
    GameStart = new UnityEvent();
    PlayerDied = new UnityEvent();

    BulletHit.AddListener(OnBulletHit);
    EnemyDied.AddListener(OnEnemyDied);
    GameStart.AddListener(OnGameStart);
    PlayerDied.AddListener(OnPlayerDied);

    _mostEnemiesKilled = PlayerPrefs.GetInt("MostEnemiesKilled", 0);
	}

  void Start () {
    _scoreText = ScoreText.GetComponent<TextMeshProUGUI>();

    GameStart.Invoke();
  }

  void OnBulletHit () {
    ProCamera2DShake.Instance.Shake("BulletHit");
  }

  void OnEnemyDied () {
    _enemiesKilled++;
    _scoreText.SetText(_enemiesKilled.ToString());
  }

  void OnGameStart () {
    _playerController.CurrentPlayerState = PlayerState.Idle;
    EndScorePanel.SetActive(false);
  }

  void OnPlayerDied () {
    EndScorePanel.SetActive(true);

    EndScoreText.SetText("Score: {0}\r\nHigh Score: {1}", _enemiesKilled, _mostEnemiesKilled);

    if (_enemiesKilled > _mostEnemiesKilled) {
      _mostEnemiesKilled = _enemiesKilled;
      PlayerPrefs.SetInt("MostEnemiesKilled", _mostEnemiesKilled);
    }

    _enemiesKilled = 0;
    _scoreText.SetText(_enemiesKilled.ToString());

    PlayerPrefs.Save();
  }

  public void InvokeGameStart () {
    GameStart.Invoke();
  }
}
