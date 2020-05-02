using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    public Camera playerCamera;
    public float scale;

    void Update()
    {
        transform.localPosition = playerCamera.transform.position / scale;
        transform.rotation = playerCamera.transform.rotation; 

    }
}
