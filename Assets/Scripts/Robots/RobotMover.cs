using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _inventoryPoint;
    
    private Transform _destination;
    private Transform _target;
    private Coroutine _moveOreJob;

    public void SetTarget(Transform target){
        _target = target;
    }

    public void Move(Transform target,Vector3 startPosition){
        //transform.position = Vector3.Lerp(startPosition, target.position, Time.deltaTime / _speed);
        //transform.position = Vector3.MoveTowards(transform.position,transform.position, _speed);
        // Направление к цели
        Vector3 direction = (target.position - transform.position).normalized;

        // Движение к цели
        transform.Translate(direction * _speed * Time.deltaTime);

        // Поворот объекта, чтобы он всегда смотрел на цель
        transform.LookAt(target);
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
