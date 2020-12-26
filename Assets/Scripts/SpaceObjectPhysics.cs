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
    public SpaceOrbitLinePhysics orbitLinePhysics;

    public bool AffectsOther = true;

    //this is to turn off the physics for special cases such as when landed on a planet
    public bool shouldChangeRigidBodyValues = true;

    //used for aligning the players camera when landed
    public Vector3 strongestGravitationalForce;
    private void OnEnable()
    {
        AddToSimulation();
        simulation = FindObjectOfType<OrbitSimulation>();
        orbitLinePhysics = GetComponent<SpaceOrbitLinePhysics>();
        rigidBody = GetComponent<Rigidbody>();
        strongestGravitationalForce = Vector3.zero;
    }

    private void OnDisable()
    {
        RemoveFromSimulation();
    }

    void Start()
    {
    }
         
    void Update()
    {
    }
    private void FixedUpdate()
    {

    }

    public void UpdateAcceleration() {
        Vector3 netForce = new Vector3(0,0,0);
        strongestGravitationalForce = Vector3.zero;
        foreach(SpaceObjectPhysics spaceObject in spaceObjects) {
             if(spaceObject == this || !spaceObject.AffectsOther) {
                  continue;
             } 
             
            Vector3 directionVector = (spaceObject.transform.position - this.transform.position).normalized;
            float distance = Mathf.Abs(Vector3.Distance(spaceObject.transform.position, this.transform.position));
            distance *= simulation.scaleFactor;

            Vector3 force = directionVector * (float)(OrbitSimulation.GravityConstant * this.mass * spaceObject.mass / (distance * distance));
            netForce += force;
            if(force.magnitude > strongestGravitationalForce.magnitude) {
                strongestGravitationalForce = force; 
            }
        }
        acceleration = netForce / mass;
    }

    public void UpdatePosition() {
        velocity += acceleration * Time.deltaTime * simulation.timeScale;
        if (shouldChangeRigidBodyValues) { 
          rigidBody.velocity = velocity * simulation.timeScale;
        }
       // rigidBody.position += velocity * Time.deltaTime * simulation.timeScale; 
        
    }
    public void OnValidate()
    {
        if (!Application.isPlaying && orbitLinePhysics) { 
          simulation.UpdateOrbitSimulation();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       // Destroy(gameObject);
    }
    private void OnDestroy()
    {
        RemoveFromSimulation(); 
    }

    public void RemoveFromSimulation() { 
         
        SpaceObjectPhysics.spaceObjects.Remove(this);
        if (orbitLinePhysics) { 
          SpaceOrbitLinePhysics.spaceObjects.Remove(orbitLinePhysics);
        }

    }
    public void AddToSimulation() {
        if (!SpaceObjectPhysics.spaceObjects.Contains(this)) { 
          SpaceObjectPhysics.spaceObjects.Add(this);
        }
        if (!SpaceOrbitLinePhysics.spaceObjects.Contains(orbitLinePhysics) && orbitLinePhysics && orbitLinePhysics) {
            SpaceOrbitLinePhysics.spaceObjects.Add(orbitLinePhysics);
        }
    }
        

}
