using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // input teclas W, S, A e D
        float moveInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");

        // movimento
        rigidBody.velocity = transform.right * moveInput * moveSpeed;

        // rota��o
        float rotationAmount = -rotationInput * rotationSpeed * Time.fixedDeltaTime; // inverte dire��o rota��o
        rigidBody.MoveRotation(rigidBody.rotation + rotationAmount);
    }
}
