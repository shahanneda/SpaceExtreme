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

    private void OnEnable()
    {
        AddToSimulation();
        simulation = FindObjectOfType<OrbitSimulation>();
        orbitLinePhysics = GetComponent<SpaceOrbitLinePhysics>();
        rigidBody = GetComponent<Rigidbody>();
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
        foreach(SpaceObjectPhysics spaceObject in spaceObjects) {
           if(spaceObject == this) {
                continue;
           } 
             
            Vector3 directionVector = (spaceObject.transform.position - this.transform.position).normalized;
            float distance = Mathf.Abs(Vector3.Distance(spaceObject.transform.position, this.transform.position));
            distance *= simulation.scaleFactor;

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
        RemoveFromSimulation(); 
    }

    public void RemoveFromSimulation() { 
         
        SpaceObjectPhysics.spaceObjects.Remove(this);
        SpaceOrbitLinePhysics.spaceObjects.Remove(orbitLinePhysics);
    }
    public void AddToSimulation() {
        if (!SpaceObjectPhysics.spaceObjects.Contains(this)) { 
          SpaceObjectPhysics.spaceObjects.Add(this);
        }
        if (!SpaceOrbitLinePhysics.spaceObjects.Contains(orbitLinePhysics) && orbitLinePhysics) {
            SpaceOrbitLinePhysics.spaceObjects.Add(orbitLinePhysics);
        }
    }
        

}
