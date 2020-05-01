using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitSimulation : MonoBehaviour
{
    public float timeStep = 0.5f;
    public float simLength = 1000;

    public static float gravityConstant = 0.001f;

    private void FixedUpdate()
    {
        foreach(SpaceObjectPhysics spaceObjectPhysics in SpaceObjectPhysics.spaceObjects) {
            spaceObjectPhysics.UpdateAcceleration();
            spaceObjectPhysics.UpdatePosition();
        }
    }

   public void UpdateOrbitSimulation() {
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
                spaceObject.OrbitLineUpdatePosition(timeStep);

                spaceObject.orbitLineOldPositions.Add(spaceObject.orbitLinePosition);
            }
        }


        foreach (SpaceOrbitLinePhysics spaceObject in SpaceOrbitLinePhysics.spaceObjects)
        {
            Vector3[] positions = spaceObject.orbitLineOldPositions.ToArray();
            GetComponent<LineRenderer>().positionCount = positions.Length;
            GetComponent<LineRenderer>().SetPositions(positions);
        }
   } 
}
