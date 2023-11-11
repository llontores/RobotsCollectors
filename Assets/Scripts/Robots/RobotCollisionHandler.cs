using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotCollisionHandler : MonoBehaviour
{
    public event UnityAction GetOre;
    public event UnityAction GetDestination;
    private Ore _target;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ore ore))
        {
            if (ore == _target)
            {
                GetOre?.Invoke();
            }
        }
    }

    public void SetTarget(Ore target)
    {
        _target = target;
    }
}
