using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] public float UIHealth;
    public Image healthIcon;
    private GameObject player;
    
    private float barProgress;
    private float lerpSpeed;

    void Start()
    {
        player = GameObject.Find("Player");
        healthIcon = GetComponent<Image>();
        healthIcon.fillAmount = 1f;
        lerpSpeed = 8f * Time.deltaTime;
    }

    void Update()
    {
        UIHealth = player.GetComponent<PlayerMovement>().health;
    }

    void FixedUpdate()
    {
        barProgress = UIHealth / 4;
        healthIcon.fillAmount = Mathf.Lerp(healthIcon.fillAmount, barProgress, lerpSpeed);
    }
}
