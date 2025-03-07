using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SimulationParameters", menuName = "ScriptableObjects/SimulationParameters")]
public class SimulationParameters : ScriptableObject
{
    public string simulationName = "";
    public UnitTime timeUnit = UnitTime.Seconds;
    public float MeanServiceTime = 0;
    public float MeanInterarrivalTime = 0;
    [Tooltip("Simulation Start in Seconds (Inclusive)")]
    public float SimulationStart = 0;
    [Tooltip("Simulation End in Seconds (Inclusive)")]
    public float SimulationEnd = 6000;

}
