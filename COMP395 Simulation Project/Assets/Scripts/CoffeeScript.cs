using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private TimerUI timerUI;
    [Header("Settings")]
    [SerializeField]
    private bool testQueue = false;
    [Header("Debug Settings")]
    [SerializeField]
    private float[] testArrivalTimes = {
        0.0f,
        2.0f,
        3.0f,
        4.5f
    };

    [SerializeField]
    private bool repeatDebugQueueAtEnd = false;
    [SerializeField]
    private float testServiceTimes = 5.0f;
    private int debugTestIndex = 0;
    [SerializeField]
    private float timer = 0;


    void OnEnable()
    {
        ServiceNode.OnServiceStart += SetNextServiceTime;
    }
    void OnDisable()
    {
        ServiceNode.OnServiceStart -= SetNextServiceTime;
    }
    void Update()
    {
        timer += Time.deltaTime;
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
            }
        }
        if (testQueue)
        {
            if (timer >= testArrivalTimes[debugTestIndex])
            {
                debugTestIndex++;
                customerFactory.SpawnCustomer(startingNode);
            }
        }
        timerUI.SetTimerText(timer);
    }
    private void SetNextServiceTime()
    {
        if (testQueue)
        {
            foreach (ServiceNode serviceNode in serviceNodes)
            {

                serviceNode.ServiceTime = testServiceTimes;
                if (timerUI != null)
                {
                    timerUI.SetOrderText(serviceNode.CurrentOrder.ToString());
                }

            }
        }
    }

}
