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
    private RaycastHit2D groundInfo;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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
        groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false)
        {
            if (right)
                right = false;
            else
                right = true;
        }
    }
}
