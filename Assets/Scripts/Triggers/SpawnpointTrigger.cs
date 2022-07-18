using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointTrigger : MonoBehaviour
{
    [SerializeField] Vector2 newSpawnpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Death.spawnPoint = newSpawnpoint;
    }
}
