using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {}

  public GameplayData GameplayDataInstance;
  public int EnemiesKilled = 0;
  public int MostEnemiesKilled = 0;
  public UnityEvent BulletHit;
  public UnityEvent EnemyDied;
  public UnityEvent GameStart;
  public UnityEvent PlayerDied;

  GameObject _player;
  PlayerController _playerController;

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

    MostEnemiesKilled = PlayerPrefs.GetInt("MostEnemiesKilled", 0);
	}

  void Start () {
    GameStart.Invoke();
  }

  void OnBulletHit () {
    ProCamera2DShake.Instance.Shake("BulletHit");
  }

  void OnEnemyDied () {
    EnemiesKilled++;
  }

  void OnGameStart () {
    EnemiesKilled = 0;
    _playerController.CurrentPlayerState = PlayerState.Idle;
  }

  void OnPlayerDied () {
    if (EnemiesKilled > MostEnemiesKilled) {
      MostEnemiesKilled = EnemiesKilled;
      PlayerPrefs.SetInt("MostEnemiesKilled", MostEnemiesKilled);
    }

    PlayerPrefs.Save();
  }

  public void InvokeGameStart () {
    GameStart.Invoke();
  }
}
