using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public AudioClip siu; // Assign this in the Inspector
    public AudioSource audioSource;
    public TopDownCarController car1;
    public TopDownCarController car2;
    public Rigidbody2D myRigidBody;
    public LogicScript logic;
    private bool isResetting = false;
    public bool isdoublep = false;
    public bool isdoublee = false;
    public Color blinkColor; // Color to blink the background
    private Camera mainCamera; // Reference to the main camera

    void Awake()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.position = new Vector2(0, Random.Range(-3f, 3f));
        mainCamera = Camera.main; // Get the reference to the main camera
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isResetting && (myRigidBody.position.x < -13 || myRigidBody.position.x > 13))
        {
            StartCoroutine(ResetPositionsAfterDelay(2f));
            StartCoroutine(BlinkBackground(2f, 0.2f)); // Start blinking background
            if (myRigidBody.position.x < -13)
            {
                if (isdoublep)
                {
                    logic.addPowerUpEnemyScore();
                }
                else
                {
                    logic.addEnemyScore();
                }
                PlaySiuSound(); // Play SIU sound effect
            }
            if (myRigidBody.position.x > 11.5)
            {
                if (isdoublee)
                {
                    logic.addPowerUpPlayerScore();
                }
                else
                {
                    logic.addPlayerScore();
                }
                PlaySiuSound(); // Play SIU sound effect
            }
        }
    }

    public void changep()
    {
        isdoublep = !isdoublep;
    }

    public void changee()
    {
        isdoublee = !isdoublee;
    }

    IEnumerator ResetPositionsAfterDelay(float delay)
    {
        isResetting = true;
        yield return new WaitForSeconds(delay);

        myRigidBody.position = new Vector2(0, Random.Range(-0.3f, 0.3f));
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

    void PlaySiuSound()
    {
        audioSource.PlayOneShot(siu); // Play SIU sound effect
    }
}
