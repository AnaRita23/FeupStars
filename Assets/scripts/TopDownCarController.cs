using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    Rigidbody2D carRigidbody2D; 

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.right, carRigidbody2D.velocity);
        
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        if (carRigidbody2D.velocity.magnitude > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else carRigidbody2D.drag = 0;

        Vector2 engineForceVector = transform.right * (accelerationInput * accelerationFactor);

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = rightVelocity + forwardVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        accelerationInput = inputVector.y;
        steeringInput = inputVector.x;
    }
}
