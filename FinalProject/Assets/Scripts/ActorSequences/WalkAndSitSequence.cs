using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkAndSitSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private NavMeshAgent _actorNavMeshAgent;
    [SerializeField] private Animator _actorAnimator;

    [Header("Navigation Parameters")]
    [SerializeField] private Transform[] _path;

    [Header("Rotation Parameters")]
    [SerializeField] private Transform _lookAtBeforeSit;
    [SerializeField] private float _rotationSpeed = 120.0f;

    private bool _actorIsMoving;
    private int _currentPathNodeIndex;

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
                _actorAnimator.SetTrigger("StopWalking");
                _actorIsMoving = false;
                StartCoroutine(RotateTowardsTarget());
            }
        }
    }

    public void Begin()
    {
        _actorIsMoving = true;
        _actorAnimator.SetTrigger("StartWalking");
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
            (_lookAtBeforeSit.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 1.5f)
        {
            Debug.Log(Quaternion.Angle(transform.rotation, targetRotation));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            yield return null;
        }
        _actorAnimator.SetTrigger("SitDown");
    }
}
