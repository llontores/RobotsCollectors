using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RobotsAdministrator : MonoBehaviour
{
    [SerializeField] private Robot[] _robots;

    private List<Ore> _ores = new List<Ore>();

    public void TryAddOre(Ore ore)
    {
        if(_ores.Contains(ore) == false)
        {
            _ores.Add(ore);
        }
    }

    private void Update()
    {
        if(_ores != null)
        {
            Robot result  = _robots.FirstOrDefault(robot => robot.IsUsing == false);
            result.BringOre(_ores[0]);
        }
    }
}
