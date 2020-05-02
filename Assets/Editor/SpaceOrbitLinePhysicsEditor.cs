using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SpaceOrbitLinePhysics))]
[CanEditMultipleObjects]
public class SpaceOrbitLinePhysicsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Run Orbit Simulation")) {
            ((SpaceOrbitLinePhysics)target).simulation.UpdateOrbitSimulation();
        }
    }
}
