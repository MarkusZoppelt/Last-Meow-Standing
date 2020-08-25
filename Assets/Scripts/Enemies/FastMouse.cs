using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMouse : EnemyMouse
{
    public float waitPeriod;
    public float runPeriod;
    private float timer = 0;
    private bool isRunning = false;
    private Vector2 runDirection;

    internal override void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        var playerdirection = target.position - transform.position;
        Move(playerdirection.normalized);

        if (runDirection.x < 0)
        {
            characterRenderer.flipX = true;
        }
        if (runDirection.x > 0)
        {
            characterRenderer.flipX = false;
        }
    }

    protected override void Move(Vector2 direction) 
    {
        timer += Time.deltaTime;

        if (isRunning)
        {
            if (timer > runPeriod)
            {
                timer -= runPeriod;
                isRunning = false;
            }
            body.velocity = runDirection * movementSpeed;
        }
        else
        {
            if (timer > waitPeriod)
            {
                timer -= waitPeriod;
                isRunning = true;
                runDirection = direction;
            }
            body.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag != "Player")
        {
            return;
        }

        var playerHealth = collision.collider.gameObject.GetComponent<PlayerHealthSystem>();
        if(playerHealth == null)
        {
            return;
        }

        playerHealth.DeactivateRandomComponent();

        var ownHealth = gameObject.GetComponent<EnemyHealth>();
        ownHealth.Die();
    }
}
