using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    public ScoreCounter scoreCounter;

    void Start()
    {
        basketList = new List<GameObject>();
        for (int i=0; i <numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>( basketPrefab );
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + ( basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add( tBasketGO );
        }
    }

    public void AppleMissed()
    {
        // Destroy all of the falling Apples
        string[] appleTags = { "Apple", "GoldApple", "PoisonApple" };

        foreach ( string tag in appleTags )
        {
            GameObject[] apples = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject apple in apples)
            {
               Destroy( apple );     
            }

        }

        // Destroy one of Baskets
        // Get the index of the last Basket in basketList
        int basketIndex = basketList.Count - 1;
        // Get a reference to that Basket GameObject
        GameObject basketGO = basketList[basketIndex];
        // Remove the Basket from list and destroy the GameObject
        basketList.RemoveAt( basketIndex );
        Destroy( basketGO );

        // If there are no Baskets left, restart the game
        if ( basketList.Count == 0)
        {
            ScoreCounter.finalScore = scoreCounter.score;
            
            SceneManager.LoadScene( "GameOverScene" );
        }
    }
}
