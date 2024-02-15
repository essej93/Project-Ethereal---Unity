using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyAiState
{

    Transform[] waypoints; // to hold all waypoints
    Transform wayPointParentTransf; // to hold waypoint parent
    public int currentPoint = 0;
    private Transform target;

    public void Enter(AiAgent agent)
    {
        agent.anim.SetBool("walking", true); // sets walking animation
        agent.agentAudio.Walk(); // starts walk audio
        wayPointParentTransf = agent.config.waypointParent.transform; // gets waypoint parent from config

        // instantiates tranform array with number of children of waypoint parent
        waypoints = new Transform[wayPointParentTransf.childCount];

        // adds all the child waypoints to waypoint list
        for (int i = 0; i < wayPointParentTransf.childCount; i++)
        {
            waypoints[i] = wayPointParentTransf.GetChild(i).transform;
        }

        agent.anim.speed = 1.0f;

    }

    public void Exit(AiAgent agent)
    {
        agent.anim.SetBool("walking", false);
        agent.agentAudio.Stop();
    }

    public StateId GetId()
    {
        return StateId.Patrol;
    }

    public void Update(AiAgent agent)
    {
        // checks if current point is the not at the last point
        if (currentPoint < waypoints.Length)
        {
            // gets next waypoint
            target = waypoints[currentPoint];
            agent.navAgent.SetDestination(target.position); // sets nav agent destination
            if (Vector3.Distance(agent.navAgent.transform.position, target.position) <= 2) // stops when it's less than or = 2 distance
            {
                currentPoint++;
                agent.statemachine.updateCurrentState(StateId.Idle); // sets back to idle state once it gets to destination
            }
        }
        else currentPoint = 0;

    }

}
