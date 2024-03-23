using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private int _robotPrice;

    public int RobotPrice => _robotPrice;

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
                    print(collectorsBase == null);

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
                //collectorsBase.SetNewBaseFlag(_newBaseFlag);
                _clicksCounter = 0;
            }

        }
    }
}
