using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public TopDownCarController car1;
    public TopDownCarController car2;
    public Rigidbody2D myRigidBody;
    public LogicScript logic;
    private bool isResetting = false;
    public Color blinkColor; // Color to blink the background
    private Camera mainCamera; // Reference to the main camera

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.position = new Vector2(0,Random.Range(-3f,3f));
        mainCamera = Camera.main; // Get the reference to the main camera
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isResetting && (myRigidBody.position.x < -11.5 || myRigidBody.position.x > 11.5))
        {
            StartCoroutine(ResetPositionsAfterDelay(2f));
            StartCoroutine(BlinkBackground(2f, 0.2f)); // Start blinking background
            if (myRigidBody.position.x < -11.5)
            {
                logic.addEnemyScore();
            }
            if (myRigidBody.position.x > 11.5)
            {
                logic.addPlayerScore();
            }
        }
    }

    IEnumerator ResetPositionsAfterDelay(float delay)
    {
        isResetting = true;
        yield return new WaitForSeconds(delay);

    
        myRigidBody.position = new Vector2(0,Random.Range(-0.3f,0.3f));
        myRigidBody.velocity = Vector2.zero;
        myRigidBody.rotation = 0;

        car1.Reset();

        car2.Reset();

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
