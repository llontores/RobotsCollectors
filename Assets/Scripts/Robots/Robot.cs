using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RobotMover))]
public class Robot : MonoBehaviour
{
    [SerializeField] private RobotCollisionHandler _handler;
    [SerializeField] private Transform _storage;
    [SerializeField] private float _getTargetDistance;
    public bool IsUsing { get; private set; }
    public event UnityAction OreBrought;
    public event UnityAction StateChanged;

    private RobotMover _mover;
    [SerializeField] private Transform _startPosition;
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
        IsUsing = false;
    }

    private void EndMoveJob()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);
    }

    public void BringOre(Ore target)
    {
        _mover.SetParametres(target.gameObject.transform, _storage.position, _startPosition.position);
        _moveJob = StartCoroutine(Move(target.gameObject.transform.position));
        IsUsing = true;
        _handler.SetTarget(target);
        print(target.gameObject.name);
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
        _moveJob = StartCoroutine(Move(_startPosition.position));
    }

    private void GetBase()
    {
        print("я на базе");
        if(IsUsing == true)
            EndMoveJob();
        OreBrought?.Invoke();
        IsUsing = false;
        StateChanged?.Invoke();
    }
}
