using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float mouseRotationSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float xRot = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * mouseRotationSpeed;
        Cursor.lockState = CursorLockMode.Locked;

        transform.Rotate(new Vector3(0, xRot, yRot));

    }
}
