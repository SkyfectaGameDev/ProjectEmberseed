using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisFunctionality : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform localScale;
    private GameObject player;
    private Vector3 playerVel;
    private float xVariation;
    private float yVariation;
    private int destroyTime;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        xVariation = Random.Range(-2, 0);
        yVariation = Random.Range(-2, 4);
        destroyTime = 90;

        playerVel = player.GetComponent<PlayerMovement>().body.velocity;
        body.velocity = new Vector3(playerVel.x + xVariation, playerVel.y + yVariation, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        destroyTime--;

        if (destroyTime == 0)
            Destroy(this.gameObject);
    }
}
