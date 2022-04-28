using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Variable to determine how often to spawn objects. Changing this threshold overtime can be used to increase difficulty;
    public float timeBetweenSpawns = 3;
    // Reference to the object to spawn. This can be any gameobject or prefab, but for this demo we'd like to spawn obstacles
    public GameObject prefabToSpawn;

    // Variable to keep track of time internally. Adding a random value when reseting it can create variance in frequency
    private float timeSinceLastSpawn = 0;

    // Update is called once per frame
    void Update()
    {
        // increment the timer with the time elapsed since it was last updated
        timeSinceLastSpawn += Time.deltaTime;

        // Check if enough time has elapsed to spawn another obstacle
        if(timeSinceLastSpawn > timeBetweenSpawns)
        {
            // Reset the timer!
            timeSinceLastSpawn = 0;

            SpawnObject();
        }
    }

    // Function to separate the functinality of spawning objects from the update loop.
    //   (Maybe some event can spawn more objects, or a button on the keyboard can spawn objects to help with debugging)
    void SpawnObject()
    {
        // Spawn the prefab that has been assigned in the inspector at the position of this spawner object
        GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);

        // Grab a reference to the spawned object's sprite so that we can change it's color
        SpriteRenderer spawnedSprite = spawnedObject.GetComponent<SpriteRenderer>();
        spawnedSprite.color = Color.green;
    }
}
