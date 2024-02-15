using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// statemachine keeps track of which state an AI is in, it is used to change
// inbetween the different states for the AI
public class StateMachine
{
    public EnemyAiState[] states; // holds all the states in an array
    public AiAgent agent; // holds reference to the AiAgent
    public StateId currentState; // tracks current state

    // constructor for state machine
    // uses AiAgent as argument
    public StateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numState = System.Enum.GetNames(typeof(StateId)).Length; // gets number of states in the EnemyState script
        states = new EnemyAiState[numState]; // instantiates array with number of states
    }

    // function to register states to the states array
    public void RegisterState(EnemyAiState state)
    {
        int index = (int) state.GetId(); // gets a state by calling its GetId function then
        states[index] = state; // assigns a state using it's ID in the enum list
    }

    // gets the currentState, if it is not null it will invoke Update function which updates the agent
    // AiAgent calls the Update function for the statemachine
    public void Update()
    {
        GetState(currentState)?.Update(agent); // statemachine then calls the Update() function for the current state
    }

    // function to get a state by using an id as an arg;
    public EnemyAiState GetState(StateId id)
    {
        int index = (int) id;
        return states[index];
    }

    // function used to Exit current state then change to new state
    public void updateCurrentState(StateId updatedStateId)
    {

        GetState(currentState)?.Exit(agent); // calls the Exit function of the current state
        currentState = updatedStateId; // assigns the new current state
        GetState(currentState)?.Enter(agent); // calls the Enter function of the new current state
    }

}
