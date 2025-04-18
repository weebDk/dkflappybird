using UnityEngine;

public class PipeSpawning : MonoBehaviour
{
    public GameObject Pipes;
    public float spawnRate = 2f; // Time in seconds between spawns
    public float timer = 0f; // Timer to track time since last spawn
    public float heightOffset = 7f; // Height offset for the pipe spawn position


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawn();
            timer = 0f; // Reset the timer after spawning
        }
    }
    void spawn() 
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(Pipes, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
