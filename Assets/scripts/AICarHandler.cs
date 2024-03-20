using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICarHandler : MonoBehaviour
{
    TopDownCarController topDownCarController;
    ballScript ball;
    Vector3 originalSize;
    public Image grow;
    public Image doublePoint;
    public Image block;
    public bool collisionTrigger = false;

    public LogicScript logic;
    public CarInputHandler playerCar;
    public GameObject blocker;
    public enum ControlType
    {
        AI, // Add an AI control type
    }

    public ControlType controlType;

    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<ballScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;

        // If this is an AI car, start the AI control coroutine
        if (controlType == ControlType.AI)
        {
            StartCoroutine(AIControl());
        }
    }

    // AI control coroutine
    IEnumerator AIControl()
    {
        while (true)
        {
            Vector2 carPosition = transform.position;
            // Get the ball position
            Vector2 ballPosition = ball.transform.position;

            // Determine the side of the ball opposite to the opposition's goal
            Vector2 oppositeSide = new Vector2(-13, -1);

            // Calculate the direction from the car to the opposite side of the ball
            Vector2 chaseDirection = (oppositeSide - ballPosition);

            // Apply input based on the chase direction
            Vector2 inputVector = (carPosition-ballPosition);
            Debug.Log(inputVector);
            // Apply input to the car controller
            topDownCarController.SetInputVector(inputVector);

            // Adjust the delay based on AI behavior speed
            yield return new WaitForSeconds(0.01f); // Adjust this delay as needed
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collisionTrigger = true;
        GameObject collider = other.gameObject;
        if (collider != null)
        {
            Destroy(collider);
        }

        if (collider.name == "growthPotion(Clone)")
        {
            StartCoroutine(GrowthAndBack());
            StartCoroutine(ActivatePowerupUI(grow, grow.sprite));
        }
        else if (collider.name == "truck(Clone)")
        {
            StartCoroutine(BlockGoal());
            StartCoroutine(ActivatePowerupUI(block, block.sprite));
        }
        else if (collider.name == "doublePoint(Clone)")
        {
            StartCoroutine(DoubleScore());
            StartCoroutine(ActivatePowerupUI(doublePoint, doublePoint.sprite));
        }
    }

    IEnumerator GrowthAndBack()
    {
        if (transform.localScale == originalSize)
        {
            transform.localScale *= 1.75f;
        }
        yield return new WaitForSeconds(10);
        transform.localScale = originalSize;
    }

    IEnumerator BlockGoal()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Truck"), LayerMask.NameToLayer("Field"));
        while(blocker.transform. position.y < -2)
        {
            blocker.transform.position = new Vector3(blocker.transform.position.x, blocker.transform.position.y + 0.1f, blocker.transform.position.z);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(8);
        while (blocker.transform.position.y > -11)
        {
            blocker.transform.position = new Vector3(blocker.transform.position.x, blocker.transform.position.y - 0.1f, blocker.transform.position.z);
            yield return new WaitForSeconds(0.02f);
        }      
    }

    IEnumerator DoubleScore(){
        if(playerCar.tag == "Player")
        {
            ball.changep();
            yield return new WaitForSeconds(10);
            ball.changee();
        }
        else
        {
            ball.changee();
            yield return new WaitForSeconds(10);
            ball.changee();
        }
    }

    IEnumerator ActivatePowerupUI(Image image, Sprite icon)
    {
        image.sprite = icon; // Set the powerup icon
        image.gameObject.SetActive(true); // Activate the UI element

        float timer = 0f;
        while (timer < 10)
        {
            // Toggle the visibility of the image every 0.5 seconds
            image.gameObject.SetActive(!image.gameObject.activeSelf);

            // Wait for a short interval
            yield return new WaitForSeconds(0.5f);

            // Increment the timer
            timer += 0.5f;
        }

        // Ensure the image is deactivated after blinking
        image.gameObject.SetActive(false);
    }
}
