using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFollower : MonoBehaviour
{
    // Enemy's rigidbody
    Rigidbody2D rb;

    // Enemy Health
    public float maxHealth = 5.0f;
    // Enemy movement speed
    public float speed = 5.0f;
    // Player GameObject
    public GameObject player;
    // Current Health
    private float health;

    // Enemy Invincibility State
    // Current Invinibility state
    public bool invincible = false;
    // Distance from the player for the enemy to change state
    public float stateDist = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible && Vector2.Distance(player.GetComponent<Rigidbody2D>().position, rb.position) < stateDist)
        {
            invincible = false;
        }
    }

    public void damage(float t_damage)
    {
        if (!invincible)
        {
            health -= t_damage;
            if (health <= 0.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public float getHealth()
    {
        return health;
    }
}
