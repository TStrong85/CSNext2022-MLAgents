using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Expose a reference of the rigidbody to be set in the inspector. This allows us to manipuate the referenced object's physics via script.
    public Rigidbody2D rb;

    // Update is called once per frame.
    void Update()
    {
        // Get whether the spacebar is being held down on this particular frame (GetKeyDown is true only on the frame it's pressed).
        if (Input.GetKey(KeyCode.Space))
        {
            // Move this object to the right at a rate of 1 unit per second.
            //   Time.deltaTime is the about of time in seconds since the last frame.
            //transform.position += Vector3.right * Time.deltaTime;  

            // Set this object's velocity to make it move upward.
            rb.velocity = Vector3.up;
        }
    }

    // OnCollisionEnter2D is called on the frame that this object comes into contact with another object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Print the name of the object hit to the console.
        Debug.Log(collision.gameObject.name);
    }

}
