using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float moveSpeed = 0.6f;
    public float deadZone = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // none here!
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        
        if(transform.position.x < deadZone)
        {
            Destroy(gameObject);
            Debug.Log("Pipe Destroyed");
        }
    }
}
