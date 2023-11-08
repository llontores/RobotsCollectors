using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private float _delay;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;
    [SerializeField] private int _minAmount;
    [SerializeField] private int _maxAmount;

    private void Awake(){
        Initialize(_prefabs);
        StartCoroutine(SpawnOres());
    }

    private IEnumerator SpawnOres(){
        WaitForSeconds delay = new WaitForSeconds(_delay);
        int amount = Random.Range(_minAmount,_maxAmount);
        GameObject ore;

        while(true){

            for(int i = 0; i < amount; i++){
                if(TryGetObject(out ore)){
                    ore.SetActive(true);
                    //float zPos = Random.Range(_minX + transform.position.x,_maxX + transform.position.x);
                    //float xPos = Random.Range(_minZ + transform.position.y,_maxZ + transform.position.y);
                    ore.transform.position = new Vector3(transform.position.x,3, transform.position.z);
                }
            }

            yield return delay;
        }
    }
}
