using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    void Update()
    {
        LookAtTarget();
    }
    private void LookAtTarget()
    {
        if (target != null)
        {
            transform.LookAt(Target);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
