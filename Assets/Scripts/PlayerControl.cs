using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] int speed; // Base speed of the player
    float speedMultiplier;  //

    [Range(1, 10)]
    [SerializeField] float acceleration;  //

    private bool btnPressed = false;

    [SerializeField] private LayerMask wallLayer; // Layer to check for walls
    [SerializeField] private Transform wallCheckPoint; // Point from which to check for walls
    [SerializeField] private Vector2 wallCheckSize = new Vector2(0.06f, 0.8f); // Size of the box for wall checking


    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Use GetMouseButton to detect hold state
        btnPressed = Input.GetMouseButton(0);
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();
        // Smoothly move multiplier toward target (0 or 1)
        float target = btnPressed ? 1f : 0f;
        speedMultiplier = Mathf.MoveTowards(speedMultiplier, target, acceleration * Time.fixedDeltaTime);

        // Determine horizontal direction from localScale.x sign
        float dir = Mathf.Sign(transform.localScale.x);
        float targetSpeed = speed * speedMultiplier * dir;

        if (isOnPlatform)
        {
            rb.velocity = new Vector2(targetSpeed + platformRb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }


        // Only check wall when player is attempting to move
        if (btnPressed && speedMultiplier > 0f && wallCheckPoint != null)
        {
            bool isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0f, wallLayer);
            if (isWallTouch)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        Vector3 s = transform.localScale;

        s.x = -s.x;
        transform.localScale = s;
    }

    private void OnDrawGizmosSelected() // Visualize wall check area in editor
    {
        if (wallCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(wallCheckPoint.position, wallCheckSize);
        }
    }

    void UpdateSpeedMultiplier()
    {
        if (btnPressed && speedMultiplier < 1f)
        {
            speedMultiplier += acceleration * Time.deltaTime;
            if (speedMultiplier > 1f) speedMultiplier = 1f;
        }
        else if (!btnPressed && speedMultiplier > 0f)
        {
            speedMultiplier -= acceleration * Time.deltaTime;
            if (speedMultiplier < 0f) speedMultiplier = 0f;
        }
    }
}