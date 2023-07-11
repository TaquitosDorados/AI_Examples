using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState currentState;

    private EnemyIdleState idleState = new EnemyIdleState();
    private EnemyWalkState walkState = new EnemyWalkState();
    private EnemyAttackState attackState = new EnemyAttackState();
    private EnemyFollowState followState = new EnemyFollowState();

    [SerializeField] private Enemy myself;

    public Enemy m_myself => myself;

    public EnemyIdleState m_idleState => idleState;
    public EnemyWalkState m_walkState => walkState;
    public EnemyAttackState m_attackState => attackState;
    public EnemyFollowState m_followState => followState;

    private void Awake()
    {
        SetState(walkState);
    }

    public void SetState(EnemyState state)
    {

        currentState?.OnExit(this);
        currentState = state;
        currentState?.OnEnter(this);
    }

    private void Update()
    {
        currentState?.OnUpdate(this);
    }
}
