using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWanderer : MonoBehaviour
{
    // Enemy's rigidbody
    Rigidbody2D rb;

    // Enemy Health
    public float maxHealth = 1.0f;
    // Enemy movement speed
    public float speed = 5.0f;
    // Enemy speed boost, applied after losing half of enemy health
    public float boostedSpeedFactor = 2.0f;

    // Indicates wether the enemy is facing right if true, or left if false
    public bool right = true;
    // Distance from the edge that the enemy turns
    public float distance;

    // Controls wether or not the speed boost is applied
    public bool speedBoost = false;
    // Child object in front of the enemy that detects when it is at the end of a platform
    public Transform groundDetection;

    private float health;
    private bool boostApplied = false;
    private RaycastHit2D groundInfo;
    private RaycastHit2D wallInfo;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        if(!right)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    // Moves Enemy left or right
    void move()
    {
        if(right)
        {
            rb.velocity = new Vector2(speed, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0.0f);
        }

        // Edge Detection
        groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down);
        if(groundInfo.collider == null)
        {
            if (right)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                right = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                right = true;
            }
        }

        // Right Wall Detection
        wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, distance);
        if (wallInfo.collider != null)
        {
            if (right)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                right = false;
            }
        }

        // Left Wall Detection
        wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, distance);
        if (wallInfo.collider != null)
        {
            if (!right)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                right = true;
            }
        }
    }

    public void damage(float t_damage)
    {
        health -= t_damage;
        if (health <= 0.0f)
        {
            Destroy(this.gameObject);
        }
        if(speedBoost && health <= maxHealth / 2 && !boostApplied)
        {
            speed *= boostedSpeedFactor;
        }
    }

    public float getHealth()
    {
        return health;
    }
}
