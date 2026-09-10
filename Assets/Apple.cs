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

            // Get a reference to the ApplePicker component of Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            // Call the public AppleMissed() method of apScript
            apScript.AppleMissed();
        }
    }
}
