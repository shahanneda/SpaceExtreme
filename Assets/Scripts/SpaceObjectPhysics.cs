using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceObjectPhysics : MonoBehaviour
{
    static List<SpaceObjectPhysics> spaceObjects = new List<SpaceObjectPhysics>();

    [SerializeField]
    public double universialGravitationalConstant = 0.0001;

    [SerializeField]
    Vector3 acceleration = new Vector3(0, 0, 0);

    [SerializeField]
    Vector3 velocity = new Vector3(0, 0, 0);

    private Rigidbody rigidBody;

    [SerializeField]
    public int mass = 100;

    public float timeScale = 1;

    private void OnEnable()
    {
        spaceObjects.Add(this);
    }

    private void OnDisable()
    {
        spaceObjects.Remove(this); 
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        UpdateAcceleration();
        UpdateVelocity();
    }

    void UpdateAcceleration() {
        Vector3 netForce = new Vector3(0,0,0);
        foreach(SpaceObjectPhysics spaceObject in spaceObjects) {
           if(spaceObject == this) {
                continue;
           } 
             
            Vector3 directionVector = (spaceObject.transform.position - this.transform.position).normalized;

            float distance = Mathf.Abs(Vector3.Distance(spaceObject.transform.position, this.transform.position));

            Vector3 force = directionVector * (float)(universialGravitationalConstant * this.mass * spaceObject.mass / (distance * distance));
            netForce += force;
        }
        acceleration += netForce / mass;
    }

    void UpdateVelocity() {
        velocity += acceleration * Time.deltaTime * timeScale;
        rigidBody.position += velocity*Time.deltaTime * timeScale; 
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("tes");
        Destroy(gameObject);
    }

}
