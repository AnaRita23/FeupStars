using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyscript : MonoBehaviour
{
    public Transform ball;
    public Transform playerGoal;

    public float movementSpeed = 5f;
    public float shootDistance = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calculate direction to the ball
        Vector2 directionToBall = ball.position - transform.position;

        // Move towards the ball
        rb.velocity = directionToBall.normalized * movementSpeed;

    }

}

