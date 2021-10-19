using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _afterStopPosition;
    private Quaternion _afterStopRotation;
    private Rigidbody _rigidbody;
    private bool _isStateChecking;

    public Vector3 StartPosition => _startPosition;
    public Quaternion StartRotation => _startRotation;
    public Vector3 AfterStopPosition => _afterStopPosition;
    public Quaternion AfterStopRotation => _afterStopRotation;
    public Rigidbody Rigidbody => _rigidbody;
    public event UnityAction BallHit;
    public event UnityAction BlockStopped;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cannonball ball))
        {
            BallHit?.Invoke();
        }
    }

    private void Update()
    {
        if (_isStateChecking)
        {
            if (_rigidbody.velocity == Vector3.zero && _rigidbody.angularVelocity == Vector3.zero)
            {
                _isStateChecking = false;
                _afterStopPosition = transform.position;
                _afterStopRotation = transform.rotation;
                BlockStopped?.Invoke();
            }
        }
    }

    public void StartStateChecking()
    {
        _isStateChecking = true;
    }
}
