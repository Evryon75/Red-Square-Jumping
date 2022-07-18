using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public static Vector2 spawnPoint;
    public static int DEATH_FRAMES = 150;
    public static float deathAnimFrames = DEATH_FRAMES;
    public static bool died;
    public static bool animated;
    private static SpriteRenderer playerCol;
    [SerializeField] private Animator transition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        died = true;
        animated = false;
    }
    public  static void setPlayerCold(SpriteRenderer pCol)
    {
        playerCol = pCol;
    }
    private void FixedUpdate()
    {
        if (died)
        {
            PlayerMovement.dashKey = false;
            deathAnimFrames -= Time.deltaTime;
            if (deathAnimFrames > 0)
            {
                PlayerMovement.player.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                PlayerMovement.reset();
                playerCol.color = Color.red;
                PlayerMovement.player.constraints = RigidbodyConstraints2D.FreezeRotation;
                deathAnimFrames = DEATH_FRAMES;
                animated = false;
                died = false;
                PlayerMovement.player.gameObject.SetActive(false);
                PlayerMovement.player.gameObject.SetActive(true);
                PlayerMovement.player.position = spawnPoint;
            }
        }
    }
}
