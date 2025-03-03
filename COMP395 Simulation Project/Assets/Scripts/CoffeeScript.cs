using System.Collections;
using UnityEngine;

public class CoffeeScript : MonoBehaviour
{
    Customer currentCustomer;
    CustomerMovement currentCustomerMovement;
    (int milk, int sugar) currentOrder;

    Transform[] targets = new Transform[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCustomerMovement = gameObject.GetComponent<CustomerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TakeOrder()
    {
        currentOrder = currentCustomer.GetOrder();
        yield return new WaitForSeconds(currentCustomer.cashierExitTime - currentCustomer.arrivalTime + 5);
        currentCustomerMovement.SetNewDestination(targets[0]);
        StartCoroutine(PrepOrder());
    }

    IEnumerator PrepOrder()
    {
        yield return new WaitForSeconds(currentCustomer.queueExitTime - currentCustomer.cashierExitTime);
        currentCustomerMovement.SetNewDestination(targets[1]);
    }

    public void newCustomer(Customer customer, Transform[] transforms)
    {
        currentCustomer = customer;
        targets = transforms;
        StartCoroutine(TakeOrder());
    }

    void updateDisplay()
    {

    }
}
