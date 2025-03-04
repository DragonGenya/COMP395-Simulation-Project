using System;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName = "SimulationParameters", menuName = "ScriptableObjects/SimulationParameters")]
public class SimulationParameters : ScriptableObject
{
    public string simulationName = "";
    public UnitTime timeUnit = UnitTime.Seconds;
    public float MeanServiceTime = 0;
    public float MeanInterarrivalTime = 0;

}
