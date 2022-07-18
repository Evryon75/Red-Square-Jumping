using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 offset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraOffsets.offsets = offset;
    }
}
