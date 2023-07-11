using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override void OnEnter(EnemyStateMachine machine)
    {
        Debug.Log("Entrando a Idle");
        machine.m_myself.timeOnIdle(3f);
    }

    public override void OnExit(EnemyStateMachine machine)
    {
        Debug.Log("Saliendo de Idle");
    }

    public override void OnUpdate(EnemyStateMachine machine)
    {
        if (machine.m_myself.foundPlayer)
        {
            machine.SetState(machine.m_followState);
        }

        if(!machine.m_myself.startIdle)
        {
            machine.SetState(machine.m_walkState);
        }
    }
}
