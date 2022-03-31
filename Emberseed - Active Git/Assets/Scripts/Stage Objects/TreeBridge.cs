using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBridge : MonoBehaviour
{
    [SerializeField] private bool toppled;
    [SerializeField] private Vector3 currentAngle;
    [SerializeField] private Vector3 targetAngle;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        toppled = false;
        currentAngle = transform.eulerAngles;
    }

    void FixedUpdate()
    {
        if (toppled == true)
        {
            if (currentAngle != targetAngle)
            {
                currentAngle.z -= 4.5f;
                transform.eulerAngles = currentAngle;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && toppled == false)
        {
            if (player.GetComponent<PlayerMovement>().state == 1)
            {
                toppled = true;
                player.GetComponent<PlayerMovement>().body.velocity = new Vector3(-1, 2);
            }
        }
    }
}
