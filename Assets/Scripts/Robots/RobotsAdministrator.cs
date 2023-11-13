using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RobotsAdministrator : MonoBehaviour
{
    [SerializeField] private Robot[] _robots;

    private Queue<Ore> _ores = new Queue<Ore>();

    public void AddOre(Ore ore)
    {
        _ores.Enqueue(ore);
        print("пуки");
        TryAskRobot();
    }

    private void TryAskRobot()
    {
        Robot result = _robots.FirstOrDefault(robot => robot.IsUsing == false);


        if (result != null)
        {
            Ore currentOre = _ores.Dequeue();
            print(currentOre.gameObject.transform.position);
            result.BringOre(currentOre);
        }
    }
}
