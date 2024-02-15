using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// AiAgent script is the main brain for the AI, it holds references to all the components required for the statemachine
// The state machine uses the agent as an argument so it can access the variables within the agent
public class AiAgent : MonoBehaviour
{

    // variables to manage state machine, nav mesh and animator
    [HideInInspector]
    public StateMachine statemachine; // variable to hold statemachine for AiAgent
    public StateId entryState = StateId.Idle; // sets entry state for statemachine use
    [HideInInspector]
    public NavMeshAgent navAgent; // variable to hold navAgent
    public AiAgentConfig config; // variable which holds config
    [HideInInspector]
    public Animator anim; // variable to hold the animator
    public StateId currentState;
    [HideInInspector]
    public FieldOfView fov; // variable to hold fov script
    [HideInInspector]
    public EnemyAudio agentAudio; // variable to hold EnemyAudio script
    [HideInInspector]
    public PathChecker pathChecker; // variable to hold PathChecker script

    // variables for AI to access player information
    public bool playerSeen = false;
    public bool playerSpotted = false;
    public bool chasingPlayer = false;
    public bool hasPath = false;

    // AI info
    [HideInInspector]
    public Vector3 spawnPos;
    [HideInInspector]
    public Quaternion spawnRot;

    private void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {

        // instantiates statemachine using agent and assigns components to variables for easy access
        statemachine = new StateMachine(this); // AIAgent sends itself as an arg to statemachine so statemachine can access variables
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
        agentAudio = GetComponent<EnemyAudio>();
        pathChecker = new PathChecker(this);

        // registers different states
        statemachine.RegisterState(new PatrolState());
        statemachine.RegisterState(new IdleState());
        statemachine.RegisterState(new InvestigateState());
        statemachine.RegisterState(new ChasePlayerState());
        statemachine.RegisterState(new GoToStartingPosState());
        spawnPos = transform.position;
        spawnRot = transform.rotation;

        statemachine.updateCurrentState(entryState); // sets the entry state for the statemachine

        //instantiates playerSeen default to false and gets player location for later use
        playerSeen = false;


    }

    // Update is called once per frame
    // As AiAgent is a monoscript the update function is called automatically by unity
    // and because the statemachine is no a monoscript, we call the update function within the AiScript for the statemachine
    void Update()
    {
        statemachine.Update(); // used to call the update method of the current state       
        currentState = statemachine.currentState; // for debugging purposes (to keep track of the current state when testing)
    }
}
