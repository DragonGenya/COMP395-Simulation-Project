public class Customer
{
    public int Id;
    public float arrivalTime;
    public float cashierExitTime;
    public float queueExitTime;
    public int counterNumber;
    (int milk, int sugar) order;

    // Constructor
    public Customer(int id, float arrivalTime, float cashierExitTime, float queueExitTime, int counterNumber, int milk, int sugar)
    {
        this.Id = id;
        this.arrivalTime = arrivalTime;
        this.cashierExitTime = cashierExitTime;
        this.queueExitTime = queueExitTime;
        this.counterNumber = counterNumber;
        this.order = (milk, sugar);
    }
    
    public (int, int) GetOrder()
    {
        return (order);
    }
}