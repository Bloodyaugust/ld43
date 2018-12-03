using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
  Idle,
  Moving,
  Dead
}

public class PlayerController : MonoBehaviour {
  public GameObject BulletPrefab;
  public PlayerState CurrentPlayerState;

  [SerializeField]
  float _fireInterval;
  [SerializeField]
  float _speed;
  float _timeToFire = 0;
  [SerializeField]
  Rect _moveRect;
  Toolbox _toolbox;
  Transform _firePoint;

  void Start() {
    _firePoint = transform.Find("FirePoint");
    _toolbox = Toolbox.Instance;

    CurrentPlayerState = PlayerState.Idle;
  }

  void Update() {
    if (CurrentPlayerState != PlayerState.Dead) {
      CurrentPlayerState = PlayerState.Idle;
    }

    if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && CurrentPlayerState != PlayerState.Dead) {
      CurrentPlayerState = PlayerState.Moving;
    }

    if (CurrentPlayerState == PlayerState.Moving) {
      Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;

      transform.Translate(direction * _speed * Time.deltaTime);
    }

    Vector3 constrainedPosition = transform.position;

    constrainedPosition.x = Mathf.Clamp(constrainedPosition.x, _moveRect.xMin, _moveRect.xMax);
    constrainedPosition.y = Mathf.Clamp(constrainedPosition.y, _moveRect.yMin, _moveRect.yMax);
    transform.position = constrainedPosition;

    _timeToFire -= Time.deltaTime;

    if (CurrentPlayerState != PlayerState.Dead && _timeToFire <= 0) {
      Instantiate(BulletPrefab, _firePoint.position, Quaternion.identity);
      _timeToFire = _fireInterval;
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    CurrentPlayerState = PlayerState.Dead;

    _toolbox.PlayerDied.Invoke();
  }
}
