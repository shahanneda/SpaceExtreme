using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitSimulation : MonoBehaviour
{
    public float timeStep = 0.5f;
    public float orbitCalculationTimeStep = 0.5f;
    public float simLength = 1000;

    public static float GravityConstant = 0.01f;
    public float timeScale = 1f;

    public float lastUpdateTime = 0;

    public SpaceOrbitLinePhysics relativeObject;
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
