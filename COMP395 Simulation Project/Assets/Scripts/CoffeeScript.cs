using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// This script is equivalent to a Simulation Manager. 
/// It controls the customer arrival and service times.
/// </summary>
public class CoffeeScript : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField]
    private List<ServiceNode> serviceNodes = new();
    [SerializeField]
    private Node startingNode;
    [SerializeField]
    private CustomerFactory customerFactory;
    [SerializeField]
    private SimulationParameters simulationParameters; //follows a M/M/1 queueing model.
    [SerializeField]
    private TimerUI timerUI;
    [Header("Settings")]
    [SerializeField]
    private bool testQueue = false;
    [SerializeField]
    private bool spawnConsumerAtStart = true;

    [Header("Debug Settings")]
    [SerializeField]
    private float[] testArrivalTimes = {
        0.0f,
        2.0f,
        3.0f,
        4.5f,
        12.0f,
        15.0f,
    };

    [SerializeField]
    private bool repeatDebugQueueAtEnd = false;
    [SerializeField]
    private float testServiceTimes = 5.0f;
    private int debugTestIndex = 0;
    [SerializeField]
    private float timer = 0;

    private bool spawnableInitialCustomer = false;
    private float randomInterarrivalTime = 0;
    private float startTime, endTime;


    void OnEnable()
    {
        ServiceNode.OnServiceStart += SetNextServiceTime;
        ServiceNode.OnServiceEnd += ResetCurrentOrder;
        spawnableInitialCustomer = spawnConsumerAtStart;
        timer = 0;
        if (simulationParameters != null)
        {
            startTime = simulationParameters.SimulationStart * (int)simulationParameters.timeUnit;
            endTime = simulationParameters.SimulationEnd * (int)simulationParameters.timeUnit;
            randomInterarrivalTime += GenerateNextValue(simulationParameters.MeanInterarrivalTime);
            Debug.Log("Next Arrival Time: " + randomInterarrivalTime);
            if (timerUI != null)
            {
                timerUI.SetSimulationTitle(simulationParameters.simulationName);
                timerUI.SetNextArrivalTime(randomInterarrivalTime);
            }
        }
    }
    void OnDisable()
    {
        ServiceNode.OnServiceStart -= SetNextServiceTime;
        ServiceNode.OnServiceEnd -= ResetCurrentOrder;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (!testQueue && simulationParameters != null)
        {
            if (timer >= startTime && timer <= endTime)
            {

                if (spawnableInitialCustomer)
                {
                    customerFactory.SpawnCustomer(startingNode);
                    spawnableInitialCustomer = false;
                }
                if (timer >= randomInterarrivalTime)
                {
                    randomInterarrivalTime += GenerateNextValue(this.simulationParameters.MeanInterarrivalTime);
                    customerFactory.SpawnCustomer(startingNode);
                    Debug.Log("Next Arrival Time: " + randomInterarrivalTime);
                    if (timerUI != null)
                    {
                        timerUI.SetNextArrivalTime(randomInterarrivalTime);
                    }
                }
            }
        }
        else
        {
            if (debugTestIndex >= testArrivalTimes.Length)
            {
                if (repeatDebugQueueAtEnd)
                {
                    timer = 0;
                    debugTestIndex = 0;
                }
                else
                {
                    testQueue = false;
                    return;
                }
            }
            if (timer >= testArrivalTimes[debugTestIndex])
            {
                if (timerUI != null)
                {
                    timerUI.SetNextArrivalTime(testArrivalTimes[debugTestIndex]);
                }

                debugTestIndex++;
                customerFactory.SpawnCustomer(startingNode);
            }
        }
        timerUI.SetTimerText(timer);
    }
    private void SetNextServiceTime()
    {
        float nextServiceTime;
        if (!testQueue)
        {
            nextServiceTime = GenerateNextValue(simulationParameters.MeanServiceTime);
        }
        else
        {
            nextServiceTime = testServiceTimes;
        }
        foreach (ServiceNode serviceNode in serviceNodes)
        {
            serviceNode.ServiceTime = nextServiceTime;
            Debug.Log("Service Time: " + nextServiceTime);
            if (timerUI != null)
            {
                timerUI.SetCurrentServiceTime(nextServiceTime);
                timerUI.SetOrderText(serviceNode.CurrentOrder.ToString());
            }

        }

    }
    /// <summary>
    /// Generates a random number based on a mean Arrival Time or mean Service Time. (1/Lambda or 1/Mu).
    /// Uses exponential distribution to generate the random number. (hence the logarithm)
    /// </summary>
    /// <param name="mean">The mean to generate the random number.</param>
    /// <returns></returns>
    private float GenerateNextValue(float mean)
    {
        float nextTime = -Mathf.Log(1 - UnityEngine.Random.value) * mean;
        nextTime *= (int)simulationParameters.timeUnit;
        return nextTime;
    }
    private void ResetCurrentOrder()
    {
        if (timerUI != null)
        {
            timerUI.SetOrderText("Nothing");
        }
    }

}
