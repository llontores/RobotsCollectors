using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _inventoryPoint;

    private Vector3 _destination;
    private Transform _target;
    private Coroutine _moveOreJob;
    private Vector3 _storage;
    private Vector3 _startPosition;

    public void SetParametres(Transform target,Vector3 storage,Vector3 startPosition){
        _target = target;
        _storage = storage;
        _startPosition = startPosition;
    }

    public void Move(Vector3 target){
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    public void PickUpOre(){
        _moveOreJob = StartCoroutine(MoveOre(_startPosition));
    }

    public void PutOre(){
        StopCoroutine(_moveOreJob);
        _target.position = _storage;
    }

    private IEnumerator MoveOre(Vector3 destination){

        while(transform.position.x != destination.x && transform.position.z != destination.z)
        {
            _target.position = _inventoryPoint.position;

            yield return null;
        }

        PutOre();
    }
}
