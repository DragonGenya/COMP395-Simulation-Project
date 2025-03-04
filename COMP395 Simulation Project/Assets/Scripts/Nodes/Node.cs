using UnityEngine;

public abstract class Node : MonoBehaviour
{
    protected bool isReserved = false;
    public bool IsReserved
    {
        private get
        {
            return isReserved;
        }
        set { isReserved = value; }
    }
    protected bool isOccupied = false;
    public bool IsOccupied
    {
        get
        {
            if (!isOccupied)
            {
                return isReserved;
            }
            else
            {
                return isOccupied;
            }
        }
        private set { isOccupied = value; }
    }
    public abstract void OccupyNode();
    public abstract void ReleaseNode();
    [SerializeField]
    private Node nextNode;
    public Node NextNode { get => nextNode; private set => nextNode = value; }
}
