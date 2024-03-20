using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarInputHandler : MonoBehaviour
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
        WASD,
        ArrowKeys
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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        switch (controlType)
        {
            case ControlType.WASD:
                inputVector.x = Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0);
                inputVector.y = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);
                break;
            case ControlType.ArrowKeys:
                inputVector.x = Input.GetKey("right") ? 1 : (Input.GetKey("left") ? -1 : 0);
                inputVector.y = Input.GetKey("down") ? 1 : (Input.GetKey("up") ? -1 : 0);
                break;
        }

        topDownCarController.SetInputVector(inputVector);
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