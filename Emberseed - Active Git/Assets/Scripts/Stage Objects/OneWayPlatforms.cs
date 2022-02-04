using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatforms : MonoBehaviour
{
    private GameObject oneWay;

    private EdgeCollider2D edge2D;
    private PlatformEffector2D effector;
    public float waitTime;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        edge2D = GetComponent<EdgeCollider2D>();
    }
    private void FixedUpdate()
    {
        // ----- Vertical Input Values -----
        float VerticalInput = Input.GetAxisRaw("Vertical");

        // ----- Holding Down & Jump Makes You Fall Through One-Way Platforms -----
        if ((VerticalInput < -0.01f) && Input.GetKey(KeyCode.Space))
        {
            waitTime = 10f;
            effector.rotationalOffset = 180f;
            edge2D.enabled = false;
        }


        // ----- Platforms Don't Become Solid Again Until Down Is Released -----
        if ((VerticalInput > -0.01f) && waitTime == 0f)
        {
            edge2D.enabled = true;
        }
        
        if (waitTime > 0f)
        {
            waitTime -= 1f;
        }

        if (waitTime == 1f)
        {
            effector.rotationalOffset = 0f;
        }
        
    }
}
