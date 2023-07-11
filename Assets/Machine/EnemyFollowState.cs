using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFollowState : EnemyState
{
    public override void OnEnter(EnemyStateMachine machine)
    {
        Debug.Log("Entrando a Follow");
        machine.m_myself.speed = 10;
    }

    public override void OnExit(EnemyStateMachine machine)
    {
        Debug.Log("Saliendo de Follow");
        machine.m_myself.speed = 5;
    }

    public override void OnUpdate(EnemyStateMachine machine)
    {
        machine.m_myself.FollowPlayer();

        if(Mathf.Abs(machine.m_myself.transform.position.x - machine.m_myself.player.transform.position.x) <= 1.2f)
        {
            
            machine.SetState(machine.m_attackState);
            machine.m_myself.Attack();
        }

        if (!machine.m_myself.foundPlayer)
        {
            machine.SetState(machine.m_walkState);
        }
    }
}
