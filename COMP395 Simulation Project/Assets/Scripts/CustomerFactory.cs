using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory : MonoBehaviour
{
    private readonly List<Customer> customerPool = new();
    [SerializeField]
    private Transform spawnLocation;
    [SerializeField]
    private List<GameObject> customerPrefabs = new();
    [SerializeField]
    private int customerPoolSize = 40;

    void Start()
    {
        PopulateList();
        if (spawnLocation == null)
        {
            spawnLocation = transform;
        }
    }
    private void PopulateList()
    {
        for (int i = 0; i < customerPoolSize; i++)
        {
            GameObject newCustomer = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform);
            if (newCustomer.TryGetComponent(out Customer customer))
            {
                customer.IsOnQueue = false;
                customerPool.Add(customer);
            }
        }
    }

    public void SpawnCustomer(Node initialNode)
    {
        Customer newCustomer = CheckAvailableCustomer();
        if (newCustomer != null && !initialNode.IsOccupied)
        {
            newCustomer.SetLocation(spawnLocation.position);
            newCustomer.CurrentNode = initialNode;
            newCustomer.IsOnQueue = true;
        }
        else
        {
            Debug.LogWarning("The Initial Node is Occupied!. There's no room in Queue.");
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
