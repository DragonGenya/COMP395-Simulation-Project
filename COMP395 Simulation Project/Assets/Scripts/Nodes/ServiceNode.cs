using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ServiceNode : Node
{
    //Event here!
    public static event Action OnServiceStart;
    public static event Action OnServiceEnd;

    private float _serviceTime = -1.0f;
    private float _currentTimer = 0.0f;

    public BeverageTypes CurrentOrder;
    public bool IsServing = false;

    public float ServiceTime
    {
        private get => _serviceTime;
        set => _serviceTime = value;
    }
    public override void OccupyNode()
    {
        this.isReserved = false;
        this.isOccupied = true;
        OnServiceStart?.Invoke();
    }

    public override void ReleaseNode()
    {
        this.isOccupied = false;
        _currentTimer = 0;
        _serviceTime = -1.0f;
        CurrentOrder = BeverageTypes.Nothing;
    }

    void Update()
    {
        if (isOccupied && _serviceTime >= 0.0f)
        {
            IsServing = true;
            _currentTimer += Time.deltaTime;
            if (_currentTimer >= _serviceTime)
            {
                IsServing = false;
                OnServiceEnd?.Invoke();
            }
        }
    }
}
