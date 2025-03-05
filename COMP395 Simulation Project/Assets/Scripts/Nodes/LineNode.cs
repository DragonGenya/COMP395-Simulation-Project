using UnityEngine;

public class LineNode : Node
{
    public override void OccupyNode()
    {
        this.isOccupied = true;
        this.isReserved = false;
    }

    public override void ReleaseNode()
    {
        this.isOccupied = false;
    }
}
