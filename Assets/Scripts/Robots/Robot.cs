using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotMover))]
public class Robot : MonoBehaviour
{
    [SerializeField] private RobotCollisionHandler _handler;
    [SerializeField] private Transform _storage;
    [SerializeField] private float _getTargetDistance;
    public bool IsUsing { get; private set; }

    private RobotMover _mover;
    private Vector3 _startPosition;
    private Coroutine _moveJob;

    private void OnEnable()
    {
        _handler.GetOre += GetBack;
        _handler.GetBaseBack += GetBase;
    }
     
    private void OnDisable()
    {
        _handler.GetOre -= GetBack;
        _handler.GetBaseBack -= GetBase;
    }

    private void Awake()
    {
        _mover = GetComponent<RobotMover>();
        _startPosition = transform.position;
        IsUsing = false;
    }

    private void EndMoveJob()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);
    }

    public void BringOre(Ore target)
    {
        _mover.SetParametres(target.gameObject.transform, _storage.position, _startPosition);
        IsUsing = true;
        _moveJob = StartCoroutine(Move(target.gameObject.transform.position));
        _handler.SetTarget(target);
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        while(Vector3.Distance(transform.position,targetPosition) > _getTargetDistance)
        {
            _mover.Move(targetPosition);

            yield return null;
        }
    }

    private void GetBack()
    {
        EndMoveJob();
        _mover.PickUpOre(_getTargetDistance);
        _moveJob = StartCoroutine(Move(_startPosition));
    }

    private void GetBase()
    {
        EndMoveJob();
        IsUsing = false;
    }
}
