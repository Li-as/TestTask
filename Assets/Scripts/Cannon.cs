using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cannon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _targetSprite;
    [SerializeField] private float _targetOffset;
    [SerializeField] private LayerMask _layersToConsider;
    [SerializeField] private Cannonball _cannonballTemplate;
    [SerializeField] private float _fireForce;
    [SerializeField] private LevelResetter _levelResetter;
    
    private void Update()
    {
        if (_levelResetter.IsResetting == false)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            {
                if (Input.touchCount > 0)
                {
                    TouchPhase phase = Input.GetTouch(0).phase;

                    if (phase == TouchPhase.Began || phase == TouchPhase.Moved)
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

                    if (phase == TouchPhase.Ended)
                    {
                        _targetSprite.gameObject.SetActive(false);
                        Fire(_targetSprite.transform.position);
                    }
                }
            }
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
        ball.Init(_levelResetter);
        Vector3 fireForce = (targetPosition - ball.transform.position).normalized * _fireForce;
        ball.Rigidbody.AddForce(fireForce, ForceMode.Impulse);
    }
}
