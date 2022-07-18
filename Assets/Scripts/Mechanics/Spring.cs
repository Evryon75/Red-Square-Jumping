using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement.player.velocity = new Vector2(PlayerMovement.player.velocity.x, 30);
    }
}
