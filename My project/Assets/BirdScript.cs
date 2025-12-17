using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    // references
    public Rigidbody2D myRigidBody;
    private SpriteRenderer spriteRenderer;
    public Sprite[] flapSprites;
    public LogicScript logic;
    public AudioSource flapSound;
    public AudioSource die;
    public AudioSource hit;

    // logic variables
    public float flapStrength = 2.5f;
    public bool isAlive = true;
    public bool outOfBounds = false;
    public bool hitSoundPlayed = false;
    public float normalGravity = 0.8f;

    // flap animation variables
    public float flapAnimSpeed = 0.1f; 
    private int currentFlapFrame = 0;
    private float flapAnimTimer = 0f;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!logic.isGameStarted)
        {
            myRigidBody.gravityScale = 0;
            return;
        }
        else if (myRigidBody.gravityScale == 0)
        {
            // Enable gravity when the game starts
            myRigidBody.gravityScale = normalGravity;
        }
        animateFlap();

        if ((Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame || (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)) && isAlive)
        {
            myRigidBody.linearVelocity = Vector2.up * flapStrength;
            transform.rotation = Quaternion.Euler(0, 0, 25); // upflap rotation
            flapSound.Play();
        }
        // falling rotation
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(myRigidBody.linearVelocity.y * 5, -90, 25));

        if ((transform.position.y > 2 || transform.position.y < -2) && !outOfBounds) // boundary check
        {
            die.Play();
            logic.gameOver();
            isAlive = false;
            outOfBounds = true;
            Debug.Log("Out of bounds");
        }

    }
    private void animateFlap()
    {
        flapAnimTimer += Time.deltaTime;
        if(flapAnimTimer >= flapAnimSpeed)
        {
            flapAnimTimer = 0f;
            currentFlapFrame = (currentFlapFrame + 1) % flapSprites.Length;
            spriteRenderer.sprite = flapSprites[currentFlapFrame];
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!hitSoundPlayed && isAlive)
        {
            hit.Play();
            hitSoundPlayed = true;
            isAlive = false;
        }
        Debug.Log("Hit detected");
    }
}
