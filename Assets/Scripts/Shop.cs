using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private RobotsAdministrator _administrator;
    [SerializeField] private OresCounter _conter;
    [SerializeField] private Button _buyRobotButton;
    [SerializeField] private int _robotPrice;

    private int _oresAmount;

    private void OnEnable()
    {
        _conter.OreCollected += CountOres;
        _buyRobotButton.onClick.AddListener(TryBuyRobot);
    }

    private void OnDisable()
    {
        _conter.OreCollected -= CountOres;
        _buyRobotButton.onClick.RemoveListener(TryBuyRobot);
    }

    private void CountOres(int oresAmount)
    {
        _oresAmount = oresAmount;
        TryShowButton();
    }

    private void TryBuyRobot()
    {
        if(_oresAmount >= _robotPrice)
        {
            _administrator.TryAddRobot();
            _conter.SpendOres(_robotPrice);
            TryShowButton();
        }
    }

    private void TryShowButton()
    {
        if(_oresAmount >= _robotPrice)
            _buyRobotButton.gameObject.SetActive(true);
        else
            _buyRobotButton.gameObject.SetActive(false);
    }
}
