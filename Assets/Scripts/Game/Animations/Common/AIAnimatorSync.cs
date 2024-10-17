using UnityEngine;
using UnityEngine.AI;
using Zenject;
public sealed class AIAnimatorSync : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    [Inject]
    public void Construct(NavMeshAgent agent, Animator animator)
    {
        _agent = agent;
        _animator = animator;

        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    private void OnAnimatorMove()
    {
        var rootPosition = _animator.rootPosition;
        rootPosition.y = _agent.nextPosition.y;
        transform.position = rootPosition;
        _agent.nextPosition = rootPosition;
    }
}