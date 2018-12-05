using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  [SerializeField]
  float _speed;
  Toolbox _toolbox;

  void Start () {
    _toolbox = Toolbox.Instance;
  }

  void Update() {
    transform.Translate(new Vector3(0, _speed * Time.deltaTime, 0));
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Enemy") {
      _toolbox.BulletHit.Invoke();
    }
    Destroy(gameObject);
  }
}
