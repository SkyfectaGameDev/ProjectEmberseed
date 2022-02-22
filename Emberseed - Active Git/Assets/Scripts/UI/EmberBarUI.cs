using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmberBarUI : MonoBehaviour
{
    [SerializeField] public int UIEmber;
    public Image emberBar;
    private GameObject player;
    
    private float barProgress;
    private float lerpSpeed;
    
    private Material material;

    void Start()
    {
        player = GameObject.Find("Player");
        emberBar = GetComponent<Image>();
        material = GetComponent<Image>().material;
        emberBar.fillAmount = 0.05f;
        lerpSpeed = 4f * Time.deltaTime;
    }

    void FixedUpdate()
    {
        UIEmber = player.GetComponent<PlayerMovement>().ember;
        material.SetColor("_Tint", player.GetComponent<PlayerMovement>().emberTint);

        EmberBarFiller();
    }

    void EmberBarFiller()
    {
        barProgress = 0.05f + (UIEmber * 0.03f);
        emberBar.fillAmount = Mathf.Lerp(emberBar.fillAmount, barProgress, lerpSpeed);
    }

}
