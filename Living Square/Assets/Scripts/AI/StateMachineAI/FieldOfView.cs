using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfView : MonoBehaviour
{
    private AiAgent agent; // to access agent variables

    [Range(0,360)]
    public float viewAngle;

    public float radius, spottedDistance;
    public bool playerSeen; // used for editor script
    public GameObject player; // used for editor script
    Collider[] checks; // holds all the player tagged objects

    private LayerMask targetMask; // holds "Player" layer 
    private LayerMask objectMask; // holds "Objects" layer


    private void Awake()
    {
        agent = this.GetComponent<AiAgent>();
        player = GameObject.FindWithTag("Player");
        targetMask = LayerMask.GetMask("Player");
        objectMask = LayerMask.GetMask("Objects");

    }

    // Start is called before the first frame update
    void Start()
    {


        // default variables (can be changed within the inspector)
        radius = 30.0f; // change this to increase FOV distance/radius
        viewAngle = 70.0f; // change this to change FOV angle
        spottedDistance = 7.0f; // if player within this distance, enemy will be alerted
        playerSeen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // stores the colliders of all objects in the targetMask layer into the checks array in a radius around the enemy
        checks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // checks if the array is not empty
        if (checks.Length != 0 )
        {
            Transform target = checks[0].transform; // just index 0 for now as we only have player in target layer
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // if statement gets the angle from the enemy to the targer(player) and checks it against the viewAngle (FOV)
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                // gets distance from enemy to target(player)
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // casts a ray from the enemy to the target, if it hits anything in the object mask then playerSeen will be set false
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, objectMask))
                {

                    //Debug.Log("Drawing ray to player");


                    // checks if the nav agent can get to the player by using the path checker script
                    // if no path can be made then agent will go back to idle state
                    if (agent.pathChecker.checkPath(player.transform.position))
                    {
                        // checks to see if target/player is greater than the spottedDistance
                        // if greater than spotted distance than AI will investigate, this also checks if playerseen is equal to false
                        // if it's less than the spotted distance then AI will chase player as player is close
                        if (distanceToTarget > spottedDistance && !agent.playerSeen && !agent.chasingPlayer)
                        {
                            if (!agent.navAgent.pathPending && agent.statemachine.currentState != StateId.Investigate)
                            {
                                agent.statemachine.updateCurrentState(StateId.Investigate);
                            }
                            

                        }
                        // player is close:
                        else
                        {
                            if(agent.statemachine.currentState != StateId.ChasePlayer)
                            {
                                agent.playerSeen = true;
                                agent.statemachine.updateCurrentState(StateId.ChasePlayer);

                            }

                        }

                    }
                    else agent.playerSeen = false;
                    
                    
                }
                else agent.playerSeen = false;
            }
            else agent.playerSeen = false;
        }
        else if (agent.playerSeen) agent.playerSeen = false;


        playerSeen = agent.playerSeen;

    }
}
