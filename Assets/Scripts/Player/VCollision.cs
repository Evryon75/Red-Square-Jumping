using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Trigger") PlayerMovement.vCollision = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Trigger") PlayerMovement.vCollision = false; ;
    }
}
