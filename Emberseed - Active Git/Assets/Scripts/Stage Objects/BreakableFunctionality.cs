using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFunctionality : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject debris;
    private BoxCollider2D boxCollider;
    public Transform localScale;

    void Start()
    {
        player = GameObject.Find("Player");
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().rollBuffer == 0)
            boxCollider.usedByComposite = true;
        else
            boxCollider.usedByComposite = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && player.GetComponent<PlayerMovement>().rollBuffer > 0)
        {
            Instantiate(debris, localScale.position, localScale.rotation);
            Instantiate(debris, localScale.position, localScale.rotation);
            Destroy(this.gameObject);
        }
            
    }

}
