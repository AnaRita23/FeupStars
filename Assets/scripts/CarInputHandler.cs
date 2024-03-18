using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    TopDownCarController topDownCarController;
    Vector3 originalSize;
    public bool collisionTrigger = false;

    public LogicScript logic;
    public CarInputHandler playerCar;
    public enum ControlType
    {
        WASD,
        ArrowKeys
    }

    public ControlType controlType;

    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
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

        else if (collider.name == "loosePoint(Clone)")
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<CarInputHandler>();

            logic.loosePlayerPoint();
        }
        else if (collider.name == "doublePoint(Clone)")
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<CarInputHandler>();

            logic.addPowerUpPlayerScore();
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
}
