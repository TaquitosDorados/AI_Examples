using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : EnemyState
{
    public override void OnEnter(EnemyStateMachine machine)
    {
        Debug.Log("Entrando a Walk");
        machine.m_myself.IdleStarter();
    }

    public override void OnExit(EnemyStateMachine machine)
    {
        Debug.Log("Saliendo de Walk");
    }

    public override void OnUpdate(EnemyStateMachine machine)
    {
        machine.m_myself.Move();

        if(machine.m_myself.foundPlayer)
        {
            machine.SetState(machine.m_followState);
        }

        if (machine.m_myself.startIdle)
        {
            machine.SetState(machine.m_idleState);
        }
    }
}
