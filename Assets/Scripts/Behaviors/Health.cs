using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
  float _currentHealth;
  [SerializeField]
  float _health;
  Toolbox _toolbox;

  void Start() {
    _toolbox = Toolbox.Instance;
  }

  void OnCollisionEnter2D(Collision2D collision) {
    _health--;

    if (_health <= 0) {
      _toolbox.EnemyDied.Invoke();
      Destroy(gameObject);
    }
  }
}
