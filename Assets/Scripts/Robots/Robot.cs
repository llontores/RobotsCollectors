using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotMover))]
public class Robot : MonoBehaviour
{
    [SerializeField] private RobotCollisionHandler _handler;
    [SerializeField] private Transform _storage;
    public bool IsUsing { get; private set; }

    private RobotMover _mover;
    private Ore _target;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private Coroutine _moveJob;
    private bool _haveGetDestination;

    private void OnEnable()
    {
        _handler.GetOre += GetBack;
        _handler.GetOre += CountGettingdestination;

    }

    private void OnDisable()
    {
        _handler.GetOre -= GetBack;
        _handler.GetOre -= CountGettingdestination;
    }

    private void Awake()
    {
        _mover = GetComponent<RobotMover>();
        _startPosition = transform.position;
        IsUsing = false;
    }

    public void BringOre(Ore target)
    {
        _target = target;
        _mover.SetParametres(_target.gameObject.transform,_storage.position,_startPosition);
        _handler.SetTarget(_target);
        _targetPosition = _target.gameObject.transform.position;
        IsUsing = true;
        StartCoroutine(Move(_targetPosition));
    }


    private void GetBack()
    {
        _targetPosition = _startPosition;
        _haveGetDestination = false;
        StartCoroutine(Move(_targetPosition));
        _mover.PickUpOre();
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        while (_haveGetDestination != true)
        {
            _mover.Move(targetPosition);

            yield return null;
        }

        EndMoveJob();
        print("я достиг цели");
    }

    private void EndMoveJob()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);

        _haveGetDestination = false;
    }

    private void CountGettingdestination()
    {
        _haveGetDestination = true;
    }

}
