using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Expose a reference of the rigidbody to be set in the inspector. This allows us to manipuate the referenced object's physics via script.
    public Rigidbody2D rb;

    // Start is called before the first frame update
    public void Start()
    {
        // Check whether an rigidbody has been assigned in the inspector
        if (rb == null)
        {
            // Try to avoid null reference errors by automatically grabbing a reference to this object's rigidbody (if it exists)
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set the obstacle's sideways velocity each frame, and inherit it's vertical velocity from last frame
        rb.velocity = (Vector3.left) + (rb.velocity.y * Vector3.up);
    }
}
