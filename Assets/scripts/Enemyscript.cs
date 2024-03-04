using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    float steeringInput = 0;

    float accelerationInput = 0;

    private Rigidbody2D carRigidbody2D;
    private Transform playerCar;
    private Transform ball;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        playerCar = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player car has the "Player" tag
        ball = GameObject.FindGameObjectWithTag("Ball").transform; // Assuming the ball has the "Ball" tag
    }

    private void FixedUpdate()
    {
        // Calculate desired inputs based on the position of the ball and player car
        CalculateInputs();

        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void CalculateInputs()
    {
        // Determine desired acceleration input
        float distanceToBall = Vector2.Distance(transform.position, ball.position);
        float distanceToPlayer = Vector2.Distance(transform.position, playerCar.position);

        // If the ball is closer than the player car, accelerate towards the ball
        // Otherwise, accelerate towards the player car
        accelerationInput = distanceToBall < distanceToPlayer ? 1 : 0;

        // Determine desired steering input
        Vector2 directionToBall = (ball.position - transform.position).normalized;
        float angleToBall = Vector2.SignedAngle(transform.right, directionToBall);

        // Steer towards the ball
        steeringInput = Mathf.Clamp(angleToBall / 90f, -1f, 1f);
    }

    void ApplyEngineForce()
    {
        // Apply engine force based on acceleration input
        Vector2 engineForceVector = transform.right * (accelerationInput * accelerationFactor);
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        // Apply steering based on steering input
        float rotationAngle = steeringInput * turnFactor;
        carRigidbody2D.MoveRotation(carRigidbody2D.rotation - rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        // Kill orthogonal velocity to simulate drifting
        Vector2 forwardVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        Vector2 rightVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);

        carRigidbody2D.velocity = rightVelocity + forwardVelocity * driftFactor;
    }
}
