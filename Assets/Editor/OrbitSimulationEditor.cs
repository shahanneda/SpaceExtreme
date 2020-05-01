using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(OrbitSimulation))]
public class OrbitSimulationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Run Orbit Simulation")) {
            ((OrbitSimulation)target).UpdateOrbitSimulation();
        }
    }
}
