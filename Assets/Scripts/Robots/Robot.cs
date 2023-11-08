using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotMover))]
public class Robot : MonoBehaviour
{
    [SerializeField] private Transform _handlePoint;
    [SerializeField] private float _handleRadius;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private RobotCollisionHandler _handler;
    private RobotMover _mover;
    private Ore _target;
    private Transform _startPosition;
    public bool IsUsing { get; private set; }

    private void OnEnable()
    {
        _handler.TouchedOre += GetBack;
    }

    private void OnDisable()
    {
        _handler.TouchedOre -= GetBack;
    }

    private void Awake()
    {
        _mover = GetComponent<RobotMover>();
        _startPosition = transform;
    }

    public void BringOre(Ore target)
    {
        _target = target;
        _mover.SetTarget(_target.gameObject.transform);
        _handler.SetTarget(_target);
        IsUsing = true;
    }

    private void Update()
    {
        if(transform.position != _target.gameObject.transform.position && _target != null)
        {
            _mover.Move(_target.gameObject.transform);
        }
    }

    private void GetBack()
    {
        _mover.PickUpOre();
        _mover.Move(_startPosition);
        _target = null;
        IsUsing = false;
    }

}
