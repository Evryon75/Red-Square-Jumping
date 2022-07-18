using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Trigger") PlayerMovement.hCollision = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Trigger") PlayerMovement.hCollision = false; ;
    }
}
