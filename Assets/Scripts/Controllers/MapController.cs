using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
  public float IsaacRate;
  public float ObstacleRate;
  public GameObject IsaacPrefab;
  public GameObject[] ObstaclePrefabs;
  public Transform SpawnRow;

  bool _enabled = true;
  float _timeToIsaac = 0;
  float _timeToObstacle = 0;
  Toolbox _toolbox;

  void Start() {
    _toolbox = Toolbox.Instance;

    _toolbox.GameStart.AddListener(OnGameStart);
    _toolbox.PlayerDied.AddListener(OnPlayerDied);
  }

  void Update() {
    if (_enabled) {
      _timeToIsaac -= Time.deltaTime;
      _timeToObstacle -= Time.deltaTime;

      if (_timeToIsaac <= 0) {
        Transform newIsaac = Instantiate(IsaacPrefab, SpawnRow.position, Quaternion.identity).transform;
        newIsaac.position = new Vector3(SpawnRow.position.x + Random.Range(-2.3f, 2.3f), SpawnRow.position.y, SpawnRow.position.z);
        _timeToIsaac = IsaacRate;
      }
      if (_timeToObstacle <= 0) {
        Transform newObstacle = Instantiate(ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Length)], SpawnRow.position, Quaternion.identity).transform;
        newObstacle.position = new Vector3(SpawnRow.position.x + Random.Range(-2.3f, 2.3f), SpawnRow.position.y, SpawnRow.position.z);
        _timeToObstacle = ObstacleRate;
      }
    }
  }

  void OnGameStart () {
    _enabled = true;
    _timeToIsaac = IsaacRate;
    _timeToObstacle = ObstacleRate;
  }

  void OnPlayerDied () {
    _enabled = false;
  }
}
