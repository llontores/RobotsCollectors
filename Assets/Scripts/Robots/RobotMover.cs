using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMover : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Transform _inventoryPoint;
    
    private Transform _destination;
    private Transform _target;
    private Coroutine _moveOreJob;

    public void SetTarget(Transform target){
        _target = target;
    }

    public void SetDestination(Transform destination){
        _destination = destination;
    }

    public void Move(){
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime / _delay);
    }

    public void PickUpOre(){
        _moveOreJob = StartCoroutine(MoveOre(_destination));
    }

    public void PutOre(){
        StopCoroutine(_moveOreJob);
        _target.position = _destination.position;
    }

    private IEnumerator MoveOre(Transform destination){
        while(_target.position != _destination.position){
            _target.position = _inventoryPoint.position;

            yield return null;
        }
    }
}
