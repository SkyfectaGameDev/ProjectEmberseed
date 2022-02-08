using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBlossomFunctionality : MonoBehaviour
{
    [SerializeField] public float charge;
    [SerializeField] private float launchMult;
    [SerializeField] private int uncurl;
    [SerializeField] private Sprite open;
    [SerializeField] private Sprite hold;

    private SpriteRenderer sprite;
    private Animator anim;
    public Transform localScale;
    private GameObject player;
    private int active;
    private Vector3 chargeSize;

    private void Start()
    {
        player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        active = 0;
        charge = 0f;
        chargeSize = new Vector3(1, 0, 0);
    }

    // ----- Charge Up & Squish -----
    private void FixedUpdate()
    {
        chargeSize.y = 1 - (charge * 0.1f);
        transform.localScale = chargeSize;

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

    // ----- Snap Player to Position -----
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            active = 1;
            sprite.sprite = hold;
            player.GetComponent<PlayerMovement>().snapPosition = new Vector3(localScale.position.x, localScale.position.y, 0);
        }
    }

    // ----- Launch Player -----
    private void Update()
    {
        anim.SetFloat("Charge", charge);

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
                player.GetComponent<PlayerMovement>().rollBuffer = uncurl;

                active = 0;
                charge = 0f;
                sprite.sprite = open;
            }
        }
    }
}
