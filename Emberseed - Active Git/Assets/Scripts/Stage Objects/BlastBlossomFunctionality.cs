using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBlossomFunctionality : MonoBehaviour
{
    [SerializeField] public float charge;
    [SerializeField] private float launchMult;

    public Transform localScale;
    public GameObject indicator;
    private GameObject player;
    private int active;

    private Vector3 chargeSize;

    private void Start()
    {
        player = GameObject.Find("Player");
        active = 0;
        charge = 0f;
        chargeSize = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        chargeSize.x = charge * 0.04f;
        chargeSize.y = charge * 0.04f;
        indicator.transform.localScale = chargeSize;

        if (active == 1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                charge += 0.1f;
                if (charge > 3)
                    charge = 3;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            active = 1;
            player.GetComponent<PlayerMovement>().snapPosition = new Vector3(localScale.position.x, localScale.position.y, 0);
        }
    }

    private void Update()
    {
        if (active == 1)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                player.GetComponent<PlayerMovement>().body.constraints = RigidbodyConstraints2D.None;
                player.GetComponent<PlayerMovement>().body.freezeRotation = true;
                player.GetComponent<PlayerMovement>().state = 1;
                player.GetComponent<PlayerMovement>().spriteColour.a = 1;
                player.GetComponent<SpriteRenderer>().color = player.GetComponent<PlayerMovement>().spriteColour;
                player.GetComponent<PlayerMovement>().body.velocity = new Vector2(0, ((charge * launchMult) + player.GetComponent<PlayerMovement>().ySpeed));
                player.GetComponent<PlayerMovement>().rollBuffer = 60;

                active = 0;
                charge = 0f;
            }
        }
    }
}
