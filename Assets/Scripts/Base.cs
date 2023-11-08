using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private RobotsAdministrator _administrator;

    private void OnTriggerStay(Collider collider)
    {
        if(collider.TryGetComponent(out Ore ore))
        {
            _administrator.TryAddOre(ore);
        }
    }
}
