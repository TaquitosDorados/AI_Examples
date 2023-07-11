using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void OnEnter(EnemyStateMachine machine);
    public abstract void OnUpdate(EnemyStateMachine machine);
    public abstract void OnExit(EnemyStateMachine machine);

}
