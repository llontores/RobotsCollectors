using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OresCounter : MonoBehaviour
{
    [SerializeField] private string _staticText;
    [SerializeField] private Robot[] _robots;
    [SerializeField] private TMP_Text _text;

    private int _counter;

    private void OnEnable()
    {
        for (int i = 0; i < _robots.Length; i++)
        {
            _robots[i].OreBrought += IncreaseOres;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _robots.Length; i++)
        {
            _robots[i].OreBrought -= IncreaseOres;
        }
    }

    private void IncreaseOres(Ore ore)
    {
        _counter++;
        _text.text = _staticText + _counter.ToString();
    }
}
