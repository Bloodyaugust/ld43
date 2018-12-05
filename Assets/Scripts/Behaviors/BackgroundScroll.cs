using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
  Camera _mainCamera;
  [SerializeField]
  float _speed;
  [SerializeField]
  GameObject _backgroundPrefab;
  GameObject[][] _background;
  int _backgroundHeight;
  int _backgroundWidth;

  void CreateBackground () {
    _background = new GameObject[_backgroundHeight][];

    for (int i = 0; i < _background.Length; i++) {
      _background[i] = new GameObject[_backgroundWidth];

      for (int i2 = 0; i2 < _background[i].Length; i2++) {
        _background[i][i2] = Instantiate(_backgroundPrefab, new Vector3(-_backgroundWidth / 2 + i2 + 0.5f, i - _backgroundHeight / 2 + 0.5f, _backgroundPrefab.transform.position.z), Quaternion.identity);
      }
    }
  }

  void SizeBackground () {
    float aspectRatio = (float)Screen.width / (float)Screen.height;

    _backgroundHeight = Mathf.CeilToInt(_mainCamera.orthographicSize * 2);
    _backgroundWidth = Mathf.CeilToInt(aspectRatio * _backgroundHeight);

    _backgroundHeight++;
  }

  void Start () {
    _mainCamera = Camera.main;


    SizeBackground();
    CreateBackground();
  }

  void Update () {
    Vector3 translateBy = new Vector3(0, _speed * Time.deltaTime, 0);
    
    for (int i = 0; i < _background.Length; i++) {
      for (int i2 = 0; i2 < _background[i].Length; i2++) {
        _background[i][i2].transform.Translate(translateBy);

        if (_background[i][i2].transform.position.y <= -_backgroundHeight / 2 - 0.5f) {
          _background[i][i2].transform.position = new Vector3(_background[i][i2].transform.position.x, _backgroundHeight / 2 + 0.5f, _background[i][i2].transform.position.z);
        }
      }
    }
  }
}
