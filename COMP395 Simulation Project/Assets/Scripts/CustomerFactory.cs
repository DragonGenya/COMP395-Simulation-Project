using System.Collections;
using UnityEngine;

public class CustomerFactory : MonoBehaviour
{
    
    public Customer[] customers ={ 
            new Customer(1, 11, 26, 72, 0, 1, 1),
            new Customer(2, 70, 80, 182, 0, 1, 1),
            new Customer(3, 180, 207, 237, 0, 1, 1),
            new Customer(4, 185, 239, 255, 0, 1, 1),
            new Customer(5, 210, 282, 350, 0, 1, 1),
            new Customer(6, 260, 352, 392, 0, 1, 1),
            new Customer(7, 290, 398, 481, 0, 1, 1),
            new Customer(8, 439, 500, 574, 0, 1, 1),
            new Customer(9, 483, 576, 627, 0, 1, 1),
            new Customer(10, 485, 629, 695, 0, 1, 1),
            new Customer(11, 543, 696, 820, 0, 1, 1),
            new Customer(12, 766, 770, 800, 0, 1, 1),
            new Customer(13, 946, 980, 1027, 0, 1, 1),
            new Customer(14, 998, 1032, 1046, 0, 1, 1),
            new Customer(15, 1005, 1070, 1083, 0, 1, 1),
            new Customer(16, 1160, 1190, 1238, 0, 1, 1),
            new Customer(17, 1185, 1239, 1340, 0, 1, 1),
            new Customer(18, 1348, 1450, 1532, 0, 1, 1),
            new Customer(19, 1530, 1555, 1620, 0, 1, 1),
            new Customer(20, 1560, 1630, 1670, 0, 1, 1),
            new Customer(21, 1562, 1677, 1690, 0, 1, 1),
            new Customer(22, 1722, 1732, 1778, 0, 1, 1),
            new Customer(23, 1910, 1913, 2080, 0, 1, 1),
            new Customer(24, 1912, 2075, 2215, 0, 1, 1),
            new Customer(25, 1912, 1990, 2054, 0, 1, 1),
            new Customer(26, 1932, 2166, 2238, 0, 1, 1),
            new Customer(27, 1960, 2270, 2344, 0, 1, 1),
            new Customer(28, 2025, 2305, 2342, 0, 1, 1),
            new Customer(29, 2130, 2390, 2414, 0, 1, 1),
            new Customer(30, 2230, 2416, 2458, 0, 1, 1),
            new Customer(31, 2300, 2460, 2488, 0, 1, 1),
            new Customer(32, 2431, 2515, 2660, 0, 1, 1)};
            
    public Transform[] targets = new Transform[4];
    public GameObject CustomerObject;
    Customer currentCustomer;

    void Start()
    {
        currentCustomer = customers[0];
        StartCoroutine(Spawn(currentCustomer.arrivalTime - 5));
    }
    
    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject currentCustomerObject = Instantiate(CustomerObject, targets[0].position, transform.rotation);
        currentCustomerObject.GetComponent<CustomerMovement>().SetNewDestination(targets[1]);
        currentCustomerObject.GetComponent<CoffeeScript>().newCustomer(currentCustomer, new Transform[]{targets[2], targets[3]});
        float arrivalDelay = currentCustomer.arrivalTime;
        currentCustomer = customers[currentCustomer.Id];
        StartCoroutine(Spawn(currentCustomer.arrivalTime - arrivalDelay));
    }
}
