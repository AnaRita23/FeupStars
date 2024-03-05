using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Rigidbody2D car1;
    public Rigidbody2D car2;
    public LogicScript logic;
    private bool isResetting = false;
    public Color blinkColor; // Color to blink the background
    private Camera mainCamera; // Reference to the main camera

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // Get the reference to the main camera
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isResetting && (myRigidBody.position.x < -17.8 || myRigidBody.position.x > 17.8))
        {
            StartCoroutine(ResetPositionsAfterDelay(2f));
            if (myRigidBody.position.x < -17.8)
            {
                logic.addEnemyScore();
            }
            if (myRigidBody.position.x > 17.8)
            {
                logic.addPlayerScore();
            }
            StartCoroutine(BlinkBackground(2f, 0.2f)); // Start blinking background
        }
    }

    IEnumerator ResetPositionsAfterDelay(float delay)
    {
        isResetting = true;
        yield return new WaitForSeconds(delay);

    

        myRigidBody.position = Vector2.zero;
        myRigidBody.velocity = Vector2.zero;
        myRigidBody.MoveRotation(0);

        car1.position = new Vector2(-10, 0);
        car1.velocity = Vector2.zero;
        car1.MoveRotation(0);

        car2.position = new Vector2(10, 0);
        car2.velocity = Vector2.zero;
        car2.MoveRotation(0);

        isResetting = false;
    }

    IEnumerator BlinkBackground(float duration, float interval)
    {
        float timer = 0f;
        Color originalColor = mainCamera.backgroundColor;

        while (timer < duration)
        {
            // Change background color to blinkColor
            mainCamera.backgroundColor = blinkColor;
            yield return new WaitForSeconds(interval);
            // Change background color back to original
            mainCamera.backgroundColor = originalColor;
            yield return new WaitForSeconds(interval);
            timer += interval * 2; // increment timer by the time for both intervals
        }
    }
}
