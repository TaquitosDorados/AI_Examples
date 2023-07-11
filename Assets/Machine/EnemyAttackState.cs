using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public override void OnEnter(EnemyStateMachine machine)
    {
        Debug.Log("Entrando a Attack");
    }

    public override void OnExit(EnemyStateMachine machine)
    {
        Debug.Log("Saliendo de Attack");
    }

    public override void OnUpdate(EnemyStateMachine machine)
    {
        if(!machine.m_myself.attacking)
        {
            machine.SetState(machine.m_walkState);
        }
    }
}
