using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory : MonoBehaviour
{
    [SerializeField]
    private List<Customer> customerPool = new();
    [SerializeField]
    private Transform spawnLocation;

    void Start()
    {
        foreach (Customer c in customerPool)
        {
            c.IsOnQueue = false;
        }
        if (spawnLocation == null)
        {
            spawnLocation = transform;
        }
    }
    public void SpawnCustomer(Node initialNode)
    {
        Customer newCustomer = CheckAvailableCustomer();
        if (newCustomer != null)
        {
            newCustomer.SetLocation(spawnLocation.position);
            newCustomer.CurrentNode = initialNode;
            newCustomer.IsOnQueue = true;
        }
    }

    private Customer CheckAvailableCustomer()
    {
        foreach (Customer c in customerPool)
        {
            if (!c.IsOnQueue)
            {
                return c;
            }
        }
        Debug.LogWarning("No available customers in pool. ");
        return null;
    }
}
