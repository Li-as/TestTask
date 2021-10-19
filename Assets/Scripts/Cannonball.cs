using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    
    private Rigidbody _rigidbody;
    private float _passedTime;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;
        if (_passedTime >= _lifetime)
        {
            Destroy(gameObject);
        }
    }
}
