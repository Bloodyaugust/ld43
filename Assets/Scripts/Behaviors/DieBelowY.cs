using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBelowY : MonoBehaviour {
  public float Y;

  void Update() {
    if (transform.position.y < Y) {
      Destroy(gameObject);
    }
  }
}
