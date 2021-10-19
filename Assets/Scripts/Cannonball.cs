using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    
    private Rigidbody _rigidbody;
    private float _passedTime;
    private LevelResetter _levelResetter;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(LevelResetter levelResetter)
    {
        _levelResetter = levelResetter;
        _levelResetter.ResetStarted += OnResetStarted;
    }

    private void OnDisable()
    {
        _levelResetter.ResetStarted -= OnResetStarted;
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;
        if (_passedTime >= _lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnResetStarted()
    {
        Destroy(gameObject);
    }
}
