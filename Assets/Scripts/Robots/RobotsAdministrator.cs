using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RobotsAdministrator : MonoBehaviour
{
    [SerializeField] private Transform _base;
    [SerializeField] private Robot _robotsPrefab;
    [SerializeField] private Robot[] _inputRobots;
    [SerializeField] private Transform _storage;

    private Queue<Ore> _ores = new Queue<Ore>();
    private List<Robot> _robots = new List<Robot>();

    private void Awake()
    {
        for (int i = 0; i < _inputRobots.Length; i++)
        {
            _robots.Add(_inputRobots[i]);
            _robots[i].SetBase(_base, _storage);
        }
    }

    private void OnEnable()
    {

        for (int i = 0; i < _robots.Count; i++)
        {
            _robots[i].StateChanged += TryAskRobot;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _robots.Count; i++)
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

    public void TryAddRobot()
    {
        Robot addedRobot = Instantiate(_robotsPrefab, _base.position,Quaternion.identity);
        addedRobot.SetBase(_base, _storage);
        _robots.Add(addedRobot);
        TryAskRobot();
    }
}
