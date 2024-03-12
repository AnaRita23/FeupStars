using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    TopDownCarController topDownCarController;
    Vector3 originalSize;

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

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        topDownCarController.SetInputVector(inputVector);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject growthPotion = other.gameObject;
        if (growthPotion != null)
        {
            Destroy(growthPotion);
        }
        StartCoroutine(GrowthAndBack());

    }

    IEnumerator GrowthAndBack()
    {
        if(transform.localScale == originalSize)
        {
            transform.localScale *= 1.75f;
        }
        yield return new WaitForSeconds(10);
        transform.localScale = originalSize;

    }
}
