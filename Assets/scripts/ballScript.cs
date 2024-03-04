using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Rigidbody2D car1;
    public Rigidbody2D car2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myRigidBody.position.x < -17.2 || myRigidBody.position.x > 17.2)
        {
            myRigidBody.position = new Vector2(0, 0);
            myRigidBody.velocity = new Vector2(0, 0);

            car1.position = new Vector2(-10, 0);
            car1.velocity = new Vector2(0, 0);

            car2.position = new Vector2(10, 0);
            car2.velocity = new Vector2(0, 0);
        }

    }
}
