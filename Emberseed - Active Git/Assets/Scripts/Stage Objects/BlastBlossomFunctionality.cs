using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBlossomFunctionality : MonoBehaviour
{
    [SerializeField] public float charge;
    [SerializeField] private float launchMultX;
    [SerializeField] private float launchMultY;
    [SerializeField] private int uncurl;
    [SerializeField] public int flip;
    [SerializeField] private Sprite open;
    [SerializeField] private Sprite hold;
    [SerializeField] public bool blossomXLock;

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
            player.GetComponent<PlayerMovement>().snapPosition = new Vector3(localScale.position.x, localScale.position.y, 0);
            active = 1;
            sprite.sprite = hold;
            player.GetComponent<PlayerMovement>().gameObject.transform.position = player.GetComponent<PlayerMovement>().snapPosition;
            
            
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
                if (flip == -1)
                    player.GetComponent<PlayerMovement>().transform.localScale = new Vector3(-1, 1, 1);
                if (flip == 1)
                    player.GetComponent<PlayerMovement>().transform.localScale = new Vector3(1, 1, 1);

                player.GetComponent<PlayerMovement>().body.constraints = RigidbodyConstraints2D.None;
                player.GetComponent<PlayerMovement>().body.freezeRotation = true;
                player.GetComponent<PlayerMovement>().state = 1;
                player.GetComponent<PlayerMovement>().spriteColour.a = 1;
                player.GetComponent<SpriteRenderer>().color = player.GetComponent<PlayerMovement>().spriteColour;
                player.GetComponent<PlayerMovement>().body.velocity = new Vector3((charge * launchMultX), (charge * launchMultY), 0);
                player.GetComponent<PlayerMovement>().rollBuffer = uncurl;
                player.GetComponent<PlayerMovement>().xLock = blossomXLock;
                

                active = 0;
                charge = 0f;
                sprite.sprite = open;
            }
        }
    }
}
