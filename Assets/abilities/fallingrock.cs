using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingrock : MonoBehaviour
{

    public float start = 10;
    public float dir = -9;
    
    //make the meteor fall until it reaches a point and gets destroyed
    void FixedUpdate ()
    {
        transform.Translate(Vector2.down * 1);
     
        if (transform.position.y > start)
            //mathf.abs gives absolute value
            dir = Mathf.Abs(dir) * -1;
        else if (transform.position.y < -1.8)
            Destroy(gameObject);
    }



}
