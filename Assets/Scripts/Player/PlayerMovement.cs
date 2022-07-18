using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Rigidbody2D player;

    private float horizontalInput;
    private bool jumpKey;
    private static int jumpTime;
    private bool halveCheck;

    public static bool dashKey;
    private static int dashTime;
    private static int dashCooldown;
    private static int dashFreeze;
    private const int DASH_COOLDOWN_TIME = 10;
    private const int DASH_DURATION = 8;
    private const int DASH_SPEED = 20;

    public static bool hCollision;
    public static bool vCollision;
    public static float lastDirection;

    private static TrailRenderer trail;

    [SerializeField] private ParticleSystem dust;
    [SerializeField] private ParticleSystem deathAnim;
    [SerializeField] private ParticleSystem dashAnim;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.1f;
        trail = GetComponent<TrailRenderer>();
        player = GetComponent<Rigidbody2D>();

        lastDirection = 1;
        reset();

        hCollision = false;
        vCollision = false;

        Death.setPlayerCold(GetComponent<SpriteRenderer>());
    }

    public static void reset()
    {
        jumpTime = 0;
        dashTime = 8;
        dashCooldown = 0;
        dashFreeze = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpKey = Input.GetKey(KeyCode.A);
        if (Input.GetKey(KeyCode.D) && dashCooldown == 0) { dashKey = true; }
        if (horizontalInput != 0 && !dashKey) lastDirection = horizontalInput;
        if ((horizontalInput != 0 || dashKey) && (vCollision || hCollision)) dust.Play();

        deathAnim.startColor = GetComponent<SpriteRenderer>().color;
        dust.startColor = GetComponent<SpriteRenderer>().color;

        if (Death.died && !Death.animated)
        {
            deathAnim.Play();
            Death.animated = true;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }

        if (dashKey && dashFreeze > 0)
        {
            dashFreeze--;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1.1f;
        }
    }
    private void FixedUpdate()
    {
        //Movement
        player.AddForce(new Vector2(horizontalInput * 4, 0), ForceMode2D.Impulse);
        if (Mathf.Abs(player.velocity.x) > 16) { player.velocity = new Vector2(horizontalInput * 16, player.velocity.y); }
        player.velocity = new Vector2(player.velocity.x * 0.75f, player.velocity.y);


        //Jumping
        if (jumpKey && jumpTime > 0)
        {
            jumpTime--;
            player.velocity = new Vector2(player.velocity.x, 8);
        }
        else if (!jumpKey && jumpTime != 8) jumpTime = 0;
        if (player.velocity.y != 0 && jumpTime == 8) jumpTime = 0;
        if (!jumpKey && jumpTime != 8 && halveCheck)
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
            halveCheck = false;
        }

        //Dashing
        if (dashKey && dashTime > 0)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            if (dashTime == DASH_DURATION)
            {
                dashFreeze = 1;
                dashCooldown = DASH_COOLDOWN_TIME;
                dashAnim.Play();
            }
            dashTime--;
            player.velocity = new Vector2(DASH_SPEED * lastDirection, 0);
        }
        if (dashCooldown > 0) dashCooldown--;
        
        //Air friction
        if (player.velocity.y < -15) player.velocity = new Vector2(player.velocity.x, -15);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (dashCooldown == 0 && dashKey)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            dashKey = false;
            dashCooldown = DASH_COOLDOWN_TIME;
            dashTime = DASH_DURATION;
        }
        if (vCollision)
        {
            jumpTime = 8;
            halveCheck = true;
        }
        if (hCollision && player.velocity.y < -6)
        {
            player.velocity = new Vector2(player.velocity.x, -6);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpTime = 8;
        halveCheck = true;
        if (hCollision)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            dashKey = false;
            dashTime = DASH_DURATION;
        }
    }
}
