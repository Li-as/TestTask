using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenBlocksTransporter : MonoBehaviour
{
    [SerializeField] private Transform _transportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Block block))
        {
            block.transform.position = _transportPoint.position;
        }
    }
}
