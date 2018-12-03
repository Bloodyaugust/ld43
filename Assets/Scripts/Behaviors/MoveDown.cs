using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
  public float Speed;

  bool _enabled = true;
  Toolbox _toolbox;

  void Start() {
    _toolbox = Toolbox.Instance;

    _toolbox.GameStart.AddListener(OnGameStart);
    _toolbox.PlayerDied.AddListener(OnPlayerDied);
  }

  void Update() {
    if (_enabled) {
      transform.Translate(new Vector3(0, -Speed * Time.deltaTime, 0));
    }
  }

  void OnGameStart() {
    _enabled = true;
  }

  void OnPlayerDied() {
    _enabled = false;
  }
}
