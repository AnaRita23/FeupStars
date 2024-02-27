using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float accelerationForce = 10f;
    public float brakeForce = 500f;
    public float rotationSpeed = 100f; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 forwardDirection = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
            myRigidBody.AddForce(transform.right * accelerationForce, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            
            myRigidBody.AddForce(-myRigidBody.velocity.normalized * brakeForce * Time.deltaTime, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}