using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenDoorAndWalkSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private NavMeshAgent _actorNavMeshAgent;
    [SerializeField] private Animator _actorAnimator;
    [SerializeField] private Door _doorToOpen;

    [Header("Animation Parameters")]
    [SerializeField] private bool _isHoldingObject;
    [SerializeField] private float _waitForObjectPlaceDuration;
    [SerializeField] private float _placementAngle = 30.0f;
    [SerializeField] private Transform _objectSlot;
    [SerializeField] private Transform _placedObjectSlot;
    [SerializeField] private GameObject _objectPrefab;

    [Header("Navigation Parameters")]
    [SerializeField] private Transform[] _path;

    [Header("Rotation Parameters")]
    [SerializeField] private Transform _lookAtWhenDone;
    [SerializeField] private float _rotationSpeed = 2.0f;

    private bool _actorIsMoving;
    private int _currentPathNodeIndex;
    private GameObject _carriedObject;

    private void OnEnable()
    {
        _doorToOpen.AnimationComplete += WalkThroughDoor;
    }
    private void OnDisable()
    {
        _doorToOpen.AnimationComplete -= WalkThroughDoor;
    }

    private void Start()
    {
        Begin();
    }

    void Update()
    {
        if (HasReachedDestination())
        {
            if (_currentPathNodeIndex + 1 < _path.Length)
            {
                _currentPathNodeIndex++;
                _actorNavMeshAgent.destination =
                    _path[_currentPathNodeIndex].position;
            }
            else
            {
                if (_isHoldingObject)
                {
                    _actorAnimator.SetTrigger("StopWalkingWithObject");
                    _actorAnimator.SetTrigger("PlaceObject");
                    StartCoroutine(WaitForObjectPlaceRoutine());
                }
                else
                {
                    _actorAnimator.SetTrigger("StopWalking");
                }
                _actorIsMoving = false;
                StartCoroutine(RotateTowardsTarget());
            }
        }
    }

    public void Begin()
    {
        if (_isHoldingObject)
        {
            _actorAnimator.SetTrigger("CarryObject");
            _carriedObject = Instantiate(_objectPrefab, _objectSlot);
        }
        _doorToOpen.Open();
    }

    public void WalkThroughDoor()
    {
        _actorIsMoving = true;
        if (_isHoldingObject)
        {
            _actorAnimator.SetTrigger("StartWalkingWithObject");
        }
        else
        {
            _actorAnimator.SetTrigger("StartWalking");
        }
        _actorNavMeshAgent.destination = _path[_currentPathNodeIndex].position;
    }

    private bool HasReachedDestination()
    {
        return !_actorNavMeshAgent.pathPending &&
            _actorIsMoving &&
            _actorNavMeshAgent.remainingDistance <=
            _actorNavMeshAgent.stoppingDistance + 0.1f;
    }

    private IEnumerator RotateTowardsTarget()
    {
        Vector3 direction =
            (_lookAtWhenDone.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(transform.rotation, lookRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                lookRotation, Time.deltaTime * _rotationSpeed);
            yield return null;
        }
    }

    private IEnumerator WaitForObjectPlaceRoutine()
    {
        yield return new WaitForSeconds(_waitForObjectPlaceDuration);
        _actorAnimator.SetTrigger("StartIdle");
        _carriedObject.transform.parent = _placedObjectSlot;
        _carriedObject.transform.localRotation = 
            Quaternion.Euler(0.0f, _placementAngle, 0.0f);
        _carriedObject.transform.localPosition = Vector3.zero;
    }
}