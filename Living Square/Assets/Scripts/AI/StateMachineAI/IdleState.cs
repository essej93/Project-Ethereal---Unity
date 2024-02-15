using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// idle state for when enemy is not doing anything (can be used between states or when it's not in any other state)
public class IdleState : EnemyAiState
{

    float time, idleTime;

    public void Enter(AiAgent agent)
    {
        agent.playerSpotted = false;
        agent.anim.SetBool("walking", false);
        time = 0;
        idleTime = agent.config.idleTime; // idle time can be changed in config file

        agent.agentAudio.Stop();

        // sets destination to current position of nav agent so it doesn't move
        agent.navAgent.SetDestination(agent.navAgent.transform.position);
    }

    public void Exit(AiAgent agent)
    {
      
    }

    public StateId GetId()
    {
        return StateId.Idle;
    }

    public void Update(AiAgent agent)
    {
        time += Time.deltaTime;
        // waits til time is higher than idle time
        if (time > idleTime)
        {
            // checks if enemy is non-patrolling or patrolling enemy
            if (!agent.config.isStaticEnemy) {
                agent.statemachine.updateCurrentState(StateId.Patrol);
            } 
            else{
                agent.statemachine.updateCurrentState(StateId.GoToStartingPoint);
            }

        }
    }


}
