using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stateId enum (add new Id for every state)
public enum StateId
{
    Idle,
    Patrol,
    Investigate,
    ChasePlayer,
    GoToStartingPoint
}

// interface that is inherited by the different states
public interface EnemyAiState
{
    StateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
