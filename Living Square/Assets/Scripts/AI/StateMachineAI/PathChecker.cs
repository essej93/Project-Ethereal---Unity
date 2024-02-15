using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

// PathChecker script which is used in the AiAgent script so it can be called from different states.
// PathChecker uses the checkPath(Vector3) function to check if the NavAgent is able to get to its destination i.e. the player
// the function will return true/false respectively to whatever script it's called from
public class PathChecker
{
    AiAgent agent;
    NavMeshAgent navAgent;

    // constructor which uses the agent as an argument
    public PathChecker(AiAgent agent)
    {
        this.agent = agent;
        navAgent = agent.navAgent;
    }

    // checkPath uses the target and uses the navAgent of the enemy it's being called from to calculate the path
    // then checks if it has a complete path and returns true/false
    public bool checkPath(Vector3 target)
    {
        NavMeshPath path = new NavMeshPath();

        navAgent.CalculatePath(target, path);

        if (path.status != NavMeshPathStatus.PathComplete)
        {
            agent.hasPath = false;
            return false;
        }

        else
        {
            agent.hasPath = true;
            return true;
        }


    }

}
