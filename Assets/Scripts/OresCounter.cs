using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OresCounter : MonoBehaviour
{
    [SerializeField] private string _staticText;
    [SerializeField] private TMP_Text _text;

    private int _counter;
    private List<Robot> _robots = new List<Robot>();
    public event UnityAction<int> OreCollected;

    private void OnDisable()
    {
        for (int i = 0; i < _robots.Count; i++)
        {
            _robots[i].OreBrought -= IncreaseOres;
        }
    }

    private void IncreaseOres(Ore ore)
    {
        _counter++;
        _text.text = _staticText + _counter.ToString();
        OreCollected?.Invoke(_counter);
    }

    public void SpendOres(int ores)
    {
        _counter -= ores;
        _text.text = _staticText + _counter.ToString();
    }

    public void AddRobot(Robot newRobot)
    {
        _robots.Add(newRobot);
        newRobot.OreBrought += IncreaseOres;
    }
}
