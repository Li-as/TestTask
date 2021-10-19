using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _targetSprite;
    [SerializeField] private float _targetOffset;
    [SerializeField] private LayerMask _layersToConsider;
    [SerializeField] private Cannonball _cannonballTemplate;
    [SerializeField] private float _fireForce;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layersToConsider))
            {
                _targetSprite.gameObject.SetActive(true);
                PlaceTargetAt(hitInfo.point, hitInfo.normal);
            }
            else
            {
                _targetSprite.gameObject.SetActive(false);
            }
        }    

        if (Input.GetMouseButtonUp(0))
        {
            _targetSprite.gameObject.SetActive(false);
            Fire(_targetSprite.transform.position);
        }
    }

    private void PlaceTargetAt(Vector3 position, Vector3 lookAtNormal)
    {
        _targetSprite.transform.rotation = Quaternion.LookRotation(lookAtNormal);
        Vector3 targetRotation = _targetSprite.transform.eulerAngles;
        targetRotation.y = 0;
        _targetSprite.transform.eulerAngles = targetRotation;

        Vector3 targetPosition = position;
        targetPosition = Vector3.MoveTowards(targetPosition, targetPosition + lookAtNormal, _targetOffset);
        _targetSprite.transform.position = targetPosition;
    }

    private void Fire(Vector3 targetPosition)
    {
        Cannonball ball = Instantiate(_cannonballTemplate, Camera.main.transform.position, Quaternion.identity);
        Vector3 fireForce = (targetPosition - ball.transform.position).normalized * _fireForce;
        ball.Rigidbody.AddForce(fireForce, ForceMode.Impulse);
    }
}
