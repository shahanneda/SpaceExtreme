using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float mouseRotationSpeed = 0.5f;

    private SpaceObjectPhysics spaceObjectPhysics;

    public bool mapOn = false;

    public float rocketForce = 0.5f;

    private Camera playerCamera;

    private Rigidbody rb;

    public bool mouseControl = true;

    public bool hasLanded = false;
    // Start is called before the first frame update
    void Start()
    {
        spaceObjectPhysics = GetComponent<SpaceObjectPhysics>();
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        playerBody = GetComponenetInChildren<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !mapOn) {
            spaceObjectPhysics.velocity += -transform.right * rocketForce * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            mapOn = !mapOn;
            print(playerCamera); 
            if (mapOn) {
                playerCamera.enabled = false;
                GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>().enabled = true;
                spaceObjectPhysics.simulation.objectOrbitLineIsOn = true;
                spaceObjectPhysics.simulation.UpdateOrbitSimulation();
            }
            else {
                playerCamera.enabled = true;
                GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>().enabled = false;
                spaceObjectPhysics.simulation.objectOrbitLineIsOn = false;
                spaceObjectPhysics.simulation.UpdateOrbitSimulation();
            }
        }
    }

    private void FixedUpdate()
    {
        float xRot = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * mouseRotationSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        if (mouseControl) { 
          transform.Rotate(new Vector3(0, xRot, yRot));
          transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }


    }
    void OnGUI()
    {
        //GUI.Label(new Rect(0, 0, 100, 100), (1.0f / Time.smoothDeltaTime).ToString() );
    }
    private void OnCollisionEnter(Collision collision)
    {

        rb.isKinematic = true;
        spaceObjectPhysics.RemoveFromSimulation();
        hasLanded = true;

    }
}
