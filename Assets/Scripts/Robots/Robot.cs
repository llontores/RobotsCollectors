using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RobotMover))]
public class Robot : MonoBehaviour
{
    [SerializeField] private RobotCollisionHandler _handler;
    [SerializeField] private float _getTargetDistance;
    public bool IsUsing { get; private set; }
    public event UnityAction<Ore> OreBrought;
    public event UnityAction StateChanged;

    private Transform _storage;
    private Transform _startPosition;
    private RobotMover _mover;
    private Ore _target;
    private Coroutine _moveJob;
    private Vector3 _targetPosition;

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
        IsUsing = false;
    }
    public void BringOre(Ore target)
    {
        _target = target;
        _mover.SetParametres(_target.gameObject.transform, _storage.position, _startPosition.position);
        _moveJob = StartCoroutine(Move(_target.gameObject.transform));
        IsUsing = true;
        _handler.SetTarget(target);
        _handler.SetTarget(_target);
    }

    public void SetBase(Transform robotsBase,Transform storage)
    {
        _startPosition = robotsBase;
        _storage = storage;
    }

    private void EndMoveJob()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);
    }


    private IEnumerator Move(Transform targetPosition)
    {
        while(Vector3.Distance(transform.position,targetPosition.position) > _getTargetDistance)
        {
            _mover.Move(targetPosition.position);

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
        if(IsUsing == true)
            EndMoveJob();
        OreBrought?.Invoke(_target);
        IsUsing = false;
        StateChanged?.Invoke();
    }
}
