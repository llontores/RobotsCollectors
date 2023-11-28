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
    [SerializeField] private GameObject _flagPrefab;

    private int _oresAmount;
    private Transform _newBaseFlag;
    private int _clicksCounter;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            CollectorsBase collectorsBase = null;

            if (_clicksCounter == 0)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    collectorsBase = hit.collider.GetComponent<CollectorsBase>();

                    if (collectorsBase != null)
                    {
                        _clicksCounter++;
                        print("я кликнул на базу" + collectorsBase.gameObject.name);
                    }             
                    else
                        _clicksCounter = 0;
                }
            }
            else if (_clicksCounter == 1)
            {
                Vector3 mousePosition = Input.mousePosition;

                _newBaseFlag.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x,
                mousePosition.y, Camera.main.nearClipPlane));
                collectorsBase.SetNewBaseFlag(_newBaseFlag);
                _clicksCounter = 0;
            }

        }
    }

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
        TryShowButton(_buyRobotButton);
    }

    private void TryBuyRobot()
    {
        if (_oresAmount >= _robotPrice)
        {
            _administrator.TryAddRobot();
            _conter.SpendOres(_robotPrice);
            _oresAmount -= _robotPrice;
            TryShowButton(_buyRobotButton);
        }
    }

    private void TryShowButton(Button shownButton)
    {
        if (_oresAmount >= _robotPrice)
            _buyRobotButton.gameObject.SetActive(true);
        else
            _buyRobotButton.gameObject.SetActive(false);
    }
}
