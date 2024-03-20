using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    TopDownCarController topDownCarController;
    ballScript ball;
    Vector3 originalSize;
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
        }

        else if (collider.name == "truck(Clone)")
        {
            StartCoroutine(BlockGoal());
        }
        else if (collider.name == "doublePoint(Clone)")
        {
            StartCoroutine(DoubleScore());
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
}