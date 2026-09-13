using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyLevel
{
    public float speed;
    public float changeDirChance;
    public float appleDropDelay;
}
public class AppleTree : MonoBehaviour
{
   [Header("Inscribed")]
   // Prefab for instantiating apples
   public GameObject applePrefab;
   public GameObject poisonApplePrefab;

   public List<DifficultyLevel> difficultyLevels;

   public float levelDuration = 10f;
//    // Speed at which the AppleTree moves
//    public float speed = 1f;

   // Distance where AppleTree turns around
   public float leftAndRightEdge = 10f;

//    // Change that the AppleTree will change directions
//    public float changeDirChance = 0.1f;

//    // Seconds between Apples instantiations
//    public float appleDropDelay = 1f;

   [Range(0f, 1f)]
   public float poisonChance = 0.15f;

   // Current movement speed
   private float speed;

   // Current difficulty level
   private int level = 0;

   // Time for next difficulty increase
   private float nextLevelTime;
   private bool lastAppleWasPoison = false;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = difficultyLevels[level].speed;
        nextLevelTime = Time.time + levelDuration;
        
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
        Invoke( "DropApple", difficultyLevels[level].appleDropDelay);
    }

    void UpdateDifficulty()
    {
        if(Time.time >= nextLevelTime && level < difficultyLevels.Count - 1)
        {
            level++;

            float direction = Mathf.Sign(speed);

            speed = difficultyLevels[level].speed * direction;

            nextLevelTime += levelDuration;

            Debug.Log("Difficulty Level: " + level);
        }
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

        UpdateDifficulty();
    }

    void FixedUpdate()
    {
        // Random direction changes are now time-based due to FixedUpdate()
        if ( Random.value < difficultyLevels[level].changeDirChance )
        {
            speed *= -1; // Change direction
        }
    }
}
