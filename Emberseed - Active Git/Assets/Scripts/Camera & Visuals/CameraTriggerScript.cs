using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerScript : MonoBehaviour
{
    [SerializeField] public float NewX;
    [SerializeField] public float NewY;

    private GameObject target;

    void Start()
    {
        target = GameObject.Find("Camera Target");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            target.GetComponent<CameraTargetScript>().posX = NewX;
            target.GetComponent<CameraTargetScript>().posY = NewY;
        }
    }




}
