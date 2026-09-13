using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
   [Header("Inscribed")]
   // Prefab for instantiating apples
   public GameObject applePrefab;
   public GameObject poisonApplePrefab;

   // Speed at which the AppleTree moves
   public float speed = 1f;

   // Distance where AppleTree turns around
   public float leftAndRightEdge = 10f;

   // Change that the AppleTree will change directions
   public float changeDirChance = 0.1f;

   // Seconds between Apples instantiations
   public float appleDropDelay = 1f;

   [Range(0f, 1f)]
   public float poisonChance = 0.15f;

   private bool lastAppleWasPoison = false;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start dropping apples
        Invoke( "DropApple", 2f );
    }

    void DropApple()
    {
        GameObject prefabToSpawn;

        if (!lastAppleWasPoison && Random.value < poisonChance)
        {
            prefabToSpawn = poisonApplePrefab;
            lastAppleWasPoison = true;
        }
        else
        {
            prefabToSpawn = applePrefab;
            lastAppleWasPoison = false;
        }

        GameObject apple = Instantiate<GameObject>( prefabToSpawn );
        apple.transform.position = transform.position;
        Invoke( "DropApple", appleDropDelay);
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Change Direction
        if ( pos.x < -leftAndRightEdge )
        {
            speed  = Mathf.Abs( speed ); // Move right
        }
        else if ( pos.x > leftAndRightEdge )
        {
            speed = -Mathf.Abs( speed ); // Move left
        }
    }

    void FixedUpdate()
    {
        // Random direction changes are now time-based due to FixedUpdate()
        if ( Random.value < changeDirChance )
        {
            speed *= -1; // Change direction
        }
    }
}
