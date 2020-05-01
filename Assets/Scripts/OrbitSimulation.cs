﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitSimulation : MonoBehaviour
{
    public float timeStep = 0.5f;
    public float simLength = 1000;

    public static float GravityConstant = 0.001f;
    //public float timeScale = 0.01f;

    public float lastUpdateTime = 0;
    private void FixedUpdate()
    {
        if (Time.time - lastUpdateTime >= timeStep)
        {

            print("Time passed " + (Time.time - lastUpdateTime));
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
                spaceObject.OrbitLineUpdatePosition(timeStep, 1);

                spaceObject.orbitLineOldPositions.Add(spaceObject.orbitLinePosition);
            }
        }


        foreach (SpaceOrbitLinePhysics spaceObject in SpaceOrbitLinePhysics.spaceObjects)
        {
            Vector3[] positions = spaceObject.orbitLineOldPositions.ToArray();
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
            lineRenderer.numCornerVertices = 5;
            lineRenderer.numCapVertices = 5;
        }
    }
}
