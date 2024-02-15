using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : EnemyAiState
{
    private GameObject player;

    public void Enter(AiAgent agent)
    {
        // if Ai enters this state then we have it start running and set chasing player to true
        agent.anim.SetBool("running", true);
        //if(!agent.agentAudio.audioSource.isPlaying) agent.agentAudio.Run();
        agent.agentAudio.Run();


        agent.chasingPlayer = true;

        // because ai is now aware of player we increase the fov angle of the Ai and the spotted/detection radius
        agent.fov.viewAngle = 180.0f;
        agent.fov.spottedDistance = agent.fov.radius;

        player = GameObject.FindWithTag("Player");
    }

    public void Exit(AiAgent agent)
    {
        if (!agent.chasingPlayer)
        {
            agent.anim.SetBool("running", false);
        }

        // if Ai can no longer see the player and exits state then its
        // fov and detection/spotted radius goes back to its default values
        agent.fov.radius = 30.0f;
        agent.fov.viewAngle = 70.0f;
        agent.fov.spottedDistance = 7.0f;
        agent.agentAudio.Stop();

    }

    public StateId GetId()
    {
        return StateId.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {

        // checks if player can be seen and that we are still chasing the player
        // if so then the ai will continue to update its destination to the players location
        if (agent.playerSeen && agent.chasingPlayer)
        {

            agent.navAgent.SetDestination(player.transform.position);
            
            
        }
        // if the Ai can no longer see the player but is still "chasing" the player then it will go to investigate state
        // to check players last known location
        else if(!agent.playerSeen && agent.chasingPlayer)
        {
            agent.statemachine.updateCurrentState(StateId.Investigate);
        }
        
    }

}
