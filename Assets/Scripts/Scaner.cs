using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private RobotsAdministrator _administrator;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.OreSpawned += TransferOre; 
    }

    private void OnDisable()
    {
        _spawner.OreSpawned -= TransferOre;
    }

    private void TransferOre(Ore ore)
    {
        _administrator.AddOre(ore);
    }

}
