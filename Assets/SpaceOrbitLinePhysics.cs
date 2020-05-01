using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpaceOrbitLinePhysics : MonoBehaviour
{
    public static List<SpaceOrbitLinePhysics> spaceObjects = new List<SpaceOrbitLinePhysics>();

    [HideInInspector]
    public Vector3 orbitLinePosition = new Vector3(0,0,0);
    [HideInInspector]
    public Vector3 orbitLineVelocity = new Vector3(0,0,0);
    [HideInInspector]
    public Vector3 orbitLineAcceleration = new Vector3(0,0,0);

    public List<Vector3> orbitLineOldPositions = new List<Vector3>();

    public LineRenderer lineRenderer;
    [HideInInspector]
    public SpaceObjectPhysics objectPhysics;

    public SpaceOrbitLinePhysics mostInfluentialBody;
    private void OnEnable()
    {
        spaceObjects.Add(this);
        objectPhysics = GetComponent<SpaceObjectPhysics>();

        UpdateOrbitLineValuesFromReal();
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void OnDisable()
    {
        spaceObjects.Remove(this);
    }

    void Start()
    {
        UpdateOrbitLineValuesFromReal();
    }

    public void UpdateOrbitLineValuesFromReal()
    {
        orbitLineVelocity = objectPhysics.velocity;
        orbitLinePosition = transform.position;
        orbitLineAcceleration = new Vector3(0, 0, 0);

    }


    private void FixedUpdate()
    {

    }

    public void OrbitLineUpdateAcceleration()
    {
        Vector3 netForce = new Vector3(0, 0, 0);
        Vector3 biggestForce = new Vector3(0, 0, 0);
        foreach (SpaceOrbitLinePhysics spaceObject in spaceObjects)
        {
            if (spaceObject == this)
            {
                continue;
            }

            Vector3 directionVector = (spaceObject.orbitLinePosition - this.orbitLinePosition).normalized;

            float distance = Mathf.Abs(Vector3.Distance(spaceObject.orbitLinePosition, this.orbitLinePosition));

            Vector3 force = directionVector * (float)(OrbitSimulation.GravityConstant * this.objectPhysics.mass * spaceObject.objectPhysics.mass / (distance * distance));
            netForce += force;
            if(force.magnitude > biggestForce.magnitude) {
                biggestForce = force;
                mostInfluentialBody = spaceObject; 
            }
        }
        this.orbitLineAcceleration = netForce / objectPhysics.mass;
    }

    public void OrbitLineUpdatePosition(float timeStep, float timeScale)
    {
        this.orbitLineVelocity += orbitLineAcceleration * timeStep  * timeScale;
        this.orbitLinePosition += orbitLineVelocity * timeStep * timeScale;
    }

    public Vector3 getRelativePosition() { 
         
    
    }
}
