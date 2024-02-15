using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// script to make a non-patrolling enemy go back to where it started/spawned
public class GoToStartingPosState : EnemyAiState
{
    public void Enter(AiAgent agent)
    {
        agent.anim.SetBool("walking", true);
        agent.agentAudio.Walk();
        agent.navAgent.SetDestination(agent.spawnPos); // has agent go back to it's start/spawn destination

    }

    public void Exit(AiAgent agent)
    {
        agent.anim.SetBool("walking", false);

        agent.agentAudio.Stop();
    }

    public StateId GetId()
    {
        return StateId.GoToStartingPoint;
    }

    public void Update(AiAgent agent)
    {
        if (Vector3.Distance(agent.navAgent.transform.position, agent.spawnPos) <= 1) // stops when it's less than or = 1 distance
        {
            // sets navagent to it's own position to have it stop moving then rotates the enemy to face it's spawning position
            agent.navAgent.SetDestination(agent.navAgent.transform.position);
            agent.anim.SetBool("walking", false);
            agent.agentAudio.Stop();
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, agent.spawnRot, Time.deltaTime * 5);
        }
    }

}
