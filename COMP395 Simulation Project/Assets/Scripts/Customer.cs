using System;
using UnityEngine;

[RequireComponent(typeof(CustomerMovement))]
public class Customer : MonoBehaviour
{
    private Node currentNode;
    public Node CurrentNode
    {
        private get { return currentNode; }
        set { currentNode = value; }
    }
    private CustomerMovement customerMovement;
    public bool IsOnQueue
    {
        get { return gameObject.activeInHierarchy; }
        set { gameObject.SetActive(value); }
    }
    private BeverageTypes beverageOrder;

    void Awake()
    {
        customerMovement = GetComponent<CustomerMovement>();
    }

    void OnEnable()
    {
        beverageOrder = (BeverageTypes)UnityEngine.Random.Range(1, Enum.GetValues(typeof(BeverageTypes)).Length);
        ServiceNode.OnServiceEnd += CheckNextNode;
        CheckNodeAvailability();

    }
    private void OnDisable()
    {
        ServiceNode.OnServiceEnd -= CheckNextNode;
    }
    public void SetLocation(Vector3 location)
    {
        this.transform.position = location;
    }
    public BeverageTypes GetOrder()
    {
        return beverageOrder;
    }
    private void CheckNodeAvailability()
    {
        if (currentNode != null)
        {
            if (!currentNode.IsOccupied)
            {
                currentNode.IsReserved = true;
                customerMovement.SetNewDestination(currentNode.transform, ArrivedToNode);
            }
            else
            {
                ArrivedToNode();
            }
        }
        else
        {
            EndService();
        }
    }
    private void ArrivedToNode()
    {
        currentNode.OccupyNode();
        if (currentNode.GetType() == typeof(ServiceNode))
        {
            ServiceNode serviceNode = (ServiceNode)currentNode;
            serviceNode.CurrentOrder = beverageOrder;
        }
        else
        {
            CheckNextNode();
        }
    }
    private void CheckNextNode()
    {
        if (currentNode.NextNode != null)
        {
            if (!currentNode.NextNode.IsOccupied)
            {
                currentNode.ReleaseNode();
                currentNode = currentNode.NextNode;
                currentNode.IsReserved = true;
                customerMovement.SetNewDestination(currentNode.transform, ArrivedToNode);
            }
        }
        else
        {
            EndService();
        }

    }
    private void EndService()
    {
        if (currentNode != null)
        {
            currentNode.ReleaseNode();
            currentNode = null;
        }
        gameObject.SetActive(false);
    }
}
