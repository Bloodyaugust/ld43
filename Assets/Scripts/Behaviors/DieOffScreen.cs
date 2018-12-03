using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOffScreen : MonoBehaviour {
  void OnBecameInvisible () {
    Destroy(gameObject);
  }
}
