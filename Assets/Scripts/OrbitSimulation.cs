using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitSimulation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        foreach(SpaceObjectPhysics spaceObjectPhysics in SpaceObjectPhysics.spaceObjects) {
            spaceObjectPhysics.UpdateAcceleration();
            spaceObjectPhysics.UpdatePosition();
        
        }
    }
}
