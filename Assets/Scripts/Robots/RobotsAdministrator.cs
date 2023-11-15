using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RobotsAdministrator : MonoBehaviour
{
    [SerializeField] private Robot[] _robots;

    private Queue<Ore> _ores = new Queue<Ore>();

    private void OnEnable()
    {
        for (int i = 0; i < _robots.Length; i++)
        {
            _robots[i].StateChanged += TryAskRobot;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _robots.Length; i++)
        {
            _robots[i].StateChanged -= TryAskRobot;
        }
    }

    public void AddOre(Ore ore)
    {
        _ores.Enqueue(ore);
        TryAskRobot();
    }

    private void TryAskRobot()
    {

        Robot result = _robots.FirstOrDefault(robot => robot.IsUsing == false);

        if (result != null && _ores.Count > 0)
        {
            Ore currentOre = _ores.Dequeue();
            result.BringOre(currentOre);
        }
    }
}
