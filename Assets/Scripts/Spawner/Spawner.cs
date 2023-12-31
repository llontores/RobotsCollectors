using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Robot[] _robots;
    [SerializeField] private float _delay;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;
    [SerializeField] private int _minAmount;
    [SerializeField] private int _maxAmount;

    public event UnityAction<Ore> OreSpawned;

    private void OnEnable()
    {
        for (int i = 0; i < _robots.Length; i++)
        {
            _robots[i].OreBrought += ChangeActiveSelf;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _robots.Length; i++)
        {
            _robots[i].OreBrought -= ChangeActiveSelf;
        }
    }

    private void Start(){
        Initialize(_prefabs);
        StartCoroutine(SpawnOres());
    }

    private IEnumerator SpawnOres(){
        WaitForSeconds delay = new WaitForSeconds(_delay);
        GameObject resource;
        int amount = Random.Range(_minAmount,_maxAmount);
        float xPos;
        float zPos;

        while(true){

            for(int i = 0; i < amount; i++){

                if(TryGetObject(out resource)){

                    resource.SetActive(true);
                    xPos = Random.Range(_minX + transform.position.x,_maxX + transform.position.x);
                    zPos = Random.Range(_minZ + transform.position.z,_maxZ + transform.position.z);
                    resource.transform.position = new Vector3(xPos,transform.position.y,zPos);
                    OreSpawned?.Invoke(resource.GetComponent<Ore>());
                }
            }

            yield return delay;
        }
    }

    private void ChangeActiveSelf(Ore ore)
    {
        ore.gameObject.SetActive(false);
    }
}
