using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceObjectPhysics : MonoBehaviour
{
    public static List<SpaceObjectPhysics> spaceObjects = new List<SpaceObjectPhysics>();


    [HideInInspector]
    private Vector3 acceleration = new Vector3(0, 0, 0);

    [SerializeField]
    public Vector3 velocity = new Vector3(0, 0, 0);

    private Rigidbody rigidBody;

    [SerializeField]
    public int mass = 100;


    public OrbitSimulation simulation;

    private void OnEnable()
    {
        spaceObjects.Add(this);
        simulation = FindObjectOfType<OrbitSimulation>();
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

    }

    public void UpdateAcceleration() {
        Vector3 netForce = new Vector3(0,0,0);
        foreach(SpaceObjectPhysics spaceObject in spaceObjects) {
           if(spaceObject == this) {
                continue;
           } 
             
            Vector3 directionVector = (spaceObject.transform.position - this.transform.position).normalized;

            float distance = Mathf.Abs(Vector3.Distance(spaceObject.transform.position, this.transform.position));

            Vector3 force = directionVector * (float)(OrbitSimulation.GravityConstant * this.mass * spaceObject.mass / (distance * distance));
            netForce += force;
        }
        acceleration = netForce / mass;
    }

    public void UpdatePosition() {
        velocity += acceleration * Time.deltaTime * simulation.timeScale;
        rigidBody.velocity = velocity * simulation.timeScale;
       // rigidBody.position += velocity * Time.deltaTime * simulation.timeScale; 
        
    }
    public void OnValidate()
    {
        if (!Application.isPlaying) { 
          simulation.UpdateOrbitSimulation();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       // Destroy(gameObject);
    }
    private void OnDestroy()
    {
        SpaceObjectPhysics.spaceObjects.Remove(this);
    }

}
