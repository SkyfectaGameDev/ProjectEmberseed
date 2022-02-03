using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetScript : MonoBehaviour
{
    [SerializeField] public float posX;
    [SerializeField] public float posY;

    void Update()
    {
        transform.localPosition = new Vector2(posX, posY);
    }
}
