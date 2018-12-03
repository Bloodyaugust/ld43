using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
  [SerializeField]
  float _speed;
  Renderer _renderer;

  void Start () {
    _renderer = GetComponent<Renderer>();
  }

  void Update () {
    Vector2 newOffset = new Vector2(0, Time.time * _speed);

    _renderer.material.mainTextureOffset = newOffset;
  }
}
