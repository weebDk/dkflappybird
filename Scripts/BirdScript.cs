using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    public LogicScript logic; // Reference to the LogicScript
    public float strength = 5f; // Speed of the bird
    public bool birdIsAlive = true;
    private InputAction jumpAction;

    private AudioSource audioSource; // Reference to the AudioSource
    public AudioClip flapSound; // Sound for flapping
    public AudioClip gravitySound; // Sound for gravity interaction
    public AudioClip hitSound; // Sound for game over

    private bool isFalling = false; // Track if the bird is falling

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        // Set up the input action
        jumpAction = new InputAction(binding: "<Keyboard>/space");
        jumpAction.AddBinding("<Touchscreen>/primaryTouch/tap");
        jumpAction.Enable();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.triggered && birdIsAlive)
        {
            myrigidbody.linearVelocity = Vector2.up * strength; // Apply upward force
            PlaySound(flapSound); // Play the flap sound
            isFalling = false; // Reset falling state
        }

        // Check if the bird is falling (negative velocity on the Y-axis)
        if (myrigidbody.linearVelocity.y < 0 && !isFalling && birdIsAlive)
        {
            PlaySound(gravitySound); // Play the gravity sound
            isFalling = true; // Set falling state
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;
        PlaySound(hitSound);
    }

    // Helper method to play a sound
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Play the sound
        }
    }
}
