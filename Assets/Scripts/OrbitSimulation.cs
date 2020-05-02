using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitSimulation : MonoBehaviour
{
    public float timeStep = 0.5f;
    private float lastUpdateTime = 0;
    
    public float orbitCalculationTimeStep = 0.5f;
    public float simLength = 1000;

    public static float GravityConstant = 0.01f;
    public float timeScale = 1f;


    public SpaceOrbitLinePhysics relativeObject;

    public float autoUpdateOrbitLineSimulationTime = 1f;
    private float lastSimulationUpdateTime = 0;

    public float loopCheckTolerance = 5f;

    public bool objectOrbitLineIsOn = true;

    public void Start()
    {
        UpdateOrbitSimulation();
    }
    private void FixedUpdate()
    {
        if (Time.time - lastUpdateTime >= timeStep)
        {

            //print("Time passed " + (Time.time - lastUpdateTime));
            lastUpdateTime = Time.time;

            foreach (SpaceObjectPhysics spaceObjectPhysics in SpaceObjectPhysics.spaceObjects)
            {
                spaceObjectPhysics.UpdateAcceleration();
            }
        }

        if(objectOrbitLineIsOn  && Application.isPlaying && Time.time - lastSimulationUpdateTime > autoUpdateOrbitLineSimulationTime ) {
            UpdateOrbitSimulation();        
            lastSimulationUpdateTime = Time.time;
        }

        foreach (SpaceObjectPhysics spaceObjectPhysics in SpaceObjectPhysics.spaceObjects)
        {
            spaceObjectPhysics.UpdatePosition();
        }
    }

    public void UpdateOrbitSimulation()
    {
        foreach (SpaceOrbitLinePhysics spaceObject in SpaceOrbitLinePhysics.spaceObjects)
        {
            spaceObject.orbitLineOldPositions.Clear();
            spaceObject.UpdateOrbitLineValuesFromReal();
        }

        for (int i = 0; i < simLength; i++)
        {
            foreach (SpaceOrbitLinePhysics spaceObject in SpaceOrbitLinePhysics.spaceObjects)
            {
                spaceObject.OrbitLineUpdateAcceleration();
            }

            foreach (SpaceOrbitLinePhysics spaceObject in SpaceOrbitLinePhysics.spaceObjects)
            {
                spaceObject.OrbitLineUpdatePosition(orbitCalculationTimeStep, 1);

                relativeObject = spaceObject.mostInfluentialBody;
                Vector3 howMuchRelMoved = relativeObject.orbitLinePosition - relativeObject.transform.position;// how much the thinng we are orbiting has moved since this sim
                Vector3 positionWithRelAdjusted = spaceObject.orbitLinePosition - howMuchRelMoved;// the real positinon adjusted for the movement of the orbiitng

                float distanceBetweenStartAndNow = Vector3.Distance(positionWithRelAdjusted, spaceObject.transform.position);

                //if (i > simLength/2 && distanceBetweenStartAndNow < loopCheckTolerance && spaceObject.indexToStopLineAt == int.MaxValue) {
                 //   spaceObject.indexToStopLineAt = i;
                //}
                //else { 
                  spaceObject.orbitLineOldPositions.Add(positionWithRelAdjusted);
                //}

            }

        }

        if(Application.isPlaying && !objectOrbitLineIsOn) {
            foreach (SpaceOrbitLinePhysics spaceObject in SpaceOrbitLinePhysics.spaceObjects)
            {
                spaceObject.lineRenderer.positionCount = 0;
                spaceObject.lineRenderer.SetPositions(new Vector3[0]);
            }
            return; 
        }
        foreach (SpaceOrbitLinePhysics spaceObject in SpaceOrbitLinePhysics.spaceObjects)
        {
            Vector3[] positions = spaceObject.orbitLineOldPositions.ToArray();
            LineRenderer lineRenderer = spaceObject.lineRenderer;
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
            lineRenderer.numCornerVertices = 5;
            lineRenderer.numCapVertices = 5;
        }
    }
}
