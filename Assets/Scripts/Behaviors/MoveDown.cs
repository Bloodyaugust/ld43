﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
  bool _enabled = true;
  [SerializeField]
  bool _speedMatchBackground;
  [SerializeField]
  float _speed;
  Toolbox _toolbox;

  void Start() {
    _toolbox = Toolbox.Instance;

    _toolbox.GameStart.AddListener(OnGameStart);
    _toolbox.PlayerDied.AddListener(OnPlayerDied);

    if (_speedMatchBackground) {
      _speed = _toolbox.GameplayDataInstance.BackgroundSpeed;
    }
  }

  void Update() {
    if (_enabled) {
      transform.Translate(new Vector3(0, -_speed * Time.deltaTime, 0));
    }
  }

  void OnGameStart() {
    _enabled = true;
  }

  void OnPlayerDied() {
    _enabled = false;
  }
}
