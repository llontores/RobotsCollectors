using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RobotMover))]
public class Robot : MonoBehaviour
{
    [SerializeField] private RobotCollisionHandler _handler;
    public bool IsUsing { get; private set; }
    public event UnityAction<Ore> OreBrought;
    public event UnityAction<bool,Vector3> MovingStateChanged;
    public event UnityAction StateChanged;

    private Transform _storage;
    private Transform _oresReceiver;
    private RobotMover _mover;
    private Ore _target;
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
    public void BringOre(Ore target)
    {
        _target = target;
        _mover.SetParametres(_target.gameObject.transform, _storage.position, _oresReceiver.position);
        MovingStateChanged?.Invoke(true,_target.gameObject.transform.position);
        //_moveJob = StartCoroutine(Move(_target.gameObject.transform));
        IsUsing = true;
        _handler.SetTarget(target);
        _handler.SetTarget(_target);
    }

    public void SetBase(Transform receiver,Transform storage)
    {
        _oresReceiver = receiver;
        _storage = storage;
    }

    private void EndMoveJob()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);
    }


    //private IEnumerator Move(Transform targetPosition)
    //{
    //    while(Vector3.Distance(transform.position,targetPosition.position) > _getTargetDistance)
    //    {
    //        _mover.Move(targetPosition.position);

    //        yield return null;
    //    }
    //}

    private void GetBack()
    {
        //EndMoveJob();
        MovingStateChanged?.Invoke(false, _oresReceiver.position);
        _mover.PickUpOre();
        MovingStateChanged?.Invoke(true, _oresReceiver.position);
        //_moveJob = StartCoroutine(Move(_oresReceiver));
    }

    private void GetBase()
    {
        if(IsUsing == true)
            MovingStateChanged?.Invoke(false, _target.gameObject.transform.position);
        OreBrought?.Invoke(_target);
        IsUsing = false;
        StateChanged?.Invoke();
    }
}
