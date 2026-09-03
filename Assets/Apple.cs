using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottonY = -20f;

    void Update()
    {
        if ( transform.position.y < bottonY )
        {
            Destroy( this.gameObject );
        }
    }
}
