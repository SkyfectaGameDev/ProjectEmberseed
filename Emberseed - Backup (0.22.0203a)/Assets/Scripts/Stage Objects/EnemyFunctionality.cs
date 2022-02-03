using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFunctionality : MonoBehaviour
{
    [SerializeField] public int health;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    public GameObject emberPrefab;
    private GameObject player;
    public Transform localScale;
    private int destroyBuffer;
    private int colourBuffer;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        colourBuffer = 0;
        destroyBuffer = -1;
    }

    void FixedUpdate()
    {
        if (destroyBuffer > 0)
            destroyBuffer -= 1;
        if (destroyBuffer == 0)
        {
            Instantiate(emberPrefab, localScale.position, localScale.rotation);
            Instantiate(emberPrefab, localScale.position, localScale.rotation);
            Instantiate(emberPrefab, localScale.position, localScale.rotation);
            Destroy(this.gameObject);
        }
        if (colourBuffer > 0)
            colourBuffer -= 1;
        if (colourBuffer > 2)
            sprite.color = new Color(255, 255, 255);
        else
            sprite.color = new Color(140, 0, 0);


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Player") && (player.GetComponent<PlayerMovement>().body.velocity.y <= 0f))
        {
            colourBuffer = 5;

            if (health > 0)
                health -= 1;
            if (health <= 0)
            {
                destroyBuffer = 2;
            }
            player.GetComponent<PlayerMovement>().Bounce();
        }
    }

}
