using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _inventoryPoint;
    [SerializeField] private RobotCollisionHandler _handler;

    private Vector3 _destination;
    private Transform _target;
    private Coroutine _moveOreJob;
    private Vector3 _storage;
    private Vector3 _startPosition;
    private Robot _robot;
    private void OnEnable()
    {
        _robot.OreBrought += PutOre; 
    }

    private void OnDisable()
    {
        _robot.OreBrought -= PutOre;
    }

    private void Awake()
    {
        _robot = GetComponent<Robot>();
    }

    public void SetParametres(Transform target,Vector3 storage,Vector3 startPosition){
        _target = target;
        _storage = storage;
        _startPosition = startPosition;
    }

    public void Move(Vector3 target){
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    public void PickUpOre(float getTargetDistance)
    {
        _moveOreJob = StartCoroutine(MoveOre(_startPosition,getTargetDistance));
    }

    public void PutOre(Ore ore){
        if (_moveOreJob != null)
            StopCoroutine(_moveOreJob);
    }

    private IEnumerator MoveOre(Vector3 destination,float getTargetDistance){

        while(Vector3.Distance(transform.position, destination) > getTargetDistance )
        {
            _target.position = _inventoryPoint.position;

            yield return null;
        }
    }
}
