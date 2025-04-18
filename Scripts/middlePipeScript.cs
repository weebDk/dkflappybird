using UnityEngine;

public class middlePipeScript : MonoBehaviour
{
    public LogicScript logic;
    private AudioSource audioSource; // Reference to the AudioSource

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) // Assuming layer 3 is the bird
        {
            logic.addScore(1); // Add score
            audioSource.Play(); // Play the sound effect
        }
    }
}
