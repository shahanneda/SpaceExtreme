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

        if(Application.isPlaying && Time.time - lastSimulationUpdateTime > autoUpdateOrbitLineSimulationTime) {
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
                Vector3 howMuchRelMoved = relativeObject.orbitLinePosition - relativeObject.transform.position;
                spaceObject.orbitLineOldPositions.Add(spaceObject.orbitLinePosition -howMuchRelMoved);
            }

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
