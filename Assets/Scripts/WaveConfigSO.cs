using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
  [SerializeField] List<GameObject> enemyPrefabs;
  [SerializeField] Transform pathPrefab;
  [SerializeField] float moveSpeed = 5f;

  public float GetMoveSpeed()
  { return moveSpeed; }

  public int GetEnemyCount()
  { return enemyPrefabs.Count; }

  public GameObject GetEnemyPrefab(int index)
  { return enemyPrefabs[index]; }

  public Transform GetStartingWaypoint()
  { return pathPrefab.GetChild(0); }

  public List<Transform> GetWaypoints()
  {
    List<Transform> waypoints = new List<Transform>();
    foreach (Transform waypoint in pathPrefab)
    {
      waypoints.Add(waypoint);
    }
    return waypoints;
  }
}
