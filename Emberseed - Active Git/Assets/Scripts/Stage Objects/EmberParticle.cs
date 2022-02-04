using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberParticle : MonoBehaviour
{
    private Rigidbody2D body;
    private GameObject player;

    Vector3 dir;

    private float spawnXVelocity;
    private float spawnYVelocity;
    private int spawnBuffer;

    void Start()
    {
        spawnXVelocity = Random.Range(-0.8f, 0.8f);
        spawnYVelocity = Random.Range(3f, 4f);
        player = GameObject.Find("Player");
        body = GetComponent<Rigidbody2D>();
        spawnBuffer = 30;
        body.gravityScale = 0.8f;
        body.velocity = new Vector2(spawnXVelocity, spawnYVelocity);
    }

    void FixedUpdate()
    {
        if (spawnBuffer > 0)
            spawnBuffer -= 1;
    }

    private void Update()
    {
        if(spawnBuffer == 0)
        {
            body.gravityScale = 0;
            dir = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            body.velocity = new Vector2(dir.x * 9, dir.y * 9);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Player") && spawnBuffer == 0)
        {
            player.GetComponent<PlayerMovement>().ember += 1;
            player.GetComponent<PlayerMovement>().emberTint.a += 0.15f;
            Destroy(this.gameObject);
        }
    }    
}
