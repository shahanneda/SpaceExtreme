using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera playerCamera;

    private Rigidbody rb;
    private SpaceObjectPhysics spaceObjectPhysics;

    public float MoveSpeed = 0.5f;

    public bool mouseControl = true;
    public float mouseRotationSpeed = 0.5f;
    public float surfaceGravityScale = 0.5f;

    void OnEnable()
    {
        playerCamera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        playerCamera.enabled = true;
        rb.isKinematic = false;
        spaceObjectPhysics = GetComponent<SpaceObjectPhysics>();

    }


    public void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            rb.MovePosition(transform.position + transform.forward * MoveSpeed*Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.MovePosition(transform.position + -transform.forward * MoveSpeed*Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.MovePosition(transform.position + transform.right * MoveSpeed*Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.MovePosition(transform.position + -transform.right * MoveSpeed*Time.deltaTime); 
        }


        float xRot = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * mouseRotationSpeed;
        if (mouseControl)
        {
            playerCamera.transform.Rotate(new Vector3(yRot, xRot, 0));
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
    public void FixedUpdate()
    {


        Vector3 gravityUp = -spaceObjectPhysics.strongestGravitationalForce.normalized;
        //this aligns our up to the strongest gravtiation force(should be planet center)
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, float.PositiveInfinity)) {

            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, float.PositiveInfinity);

            transform.rotation = finalRotation;
            transform.position = hit.point + hit.normal * .5f;


        }

        


        //rb.AddForce(-gravityUp * surfaceGravityScale, ForceMode.Force);
        //playerCamera.transform.rotation = Quaternion.FromToRotation(playerCamera.transform.up, gravityUp) * playerCamera.transform.rotation;

    }

    void OnDisable()
    {
        playerCamera.enabled = false;
        rb.isKinematic = true;     
    }

}
