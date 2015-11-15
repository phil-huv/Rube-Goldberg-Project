using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class StateMachine : MonoBehaviour
{
    // The current state.
    protected State currentState;

    // List of states in the state machine.
    List<State> states = new List<State>();

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    
    // As of now, this function does nothing. But I may need it in the future when I reuse this class. So
    // I'll just keep it here for now as a reminder that I can use this if I need to.
    void SetupMachine()
    {
        
    }





    
    // This function adds a valid  transition from state1 to state2.
    public void AddTransition(State state1, State state2)
    {
        state1.addTransition(state2);
        if (!states.Contains(state1))
            states.Add(state1);
        if (!states.Contains(state2))
            states.Add(state2);
        
    }
    
    // checks to see if the current state needs to transition.
    public void checkTransition()
    {
        currentState = currentState.checkTransition();
    }

    // This function forces the machine to transition to the next state in the list
    // STORNGLY ADVISED THAT YOU DO NOT USE THIS. USE IT ONLY IF THE STATES ARE LINEAR AND IF YOU MUST.
    public void Transition()
    {
        int index = states.IndexOf(currentState);
        currentState = states[++index];
    }
    
    // This function sets the current state to startState.
    public void startMachine(State startState)
    {
        currentState = startState;
    }


}

public abstract class State
{
    // This is a list of transitions
    protected List<State> transitions;

    // This is a public constructor.
    public State()
    {
        transitions = new List<State>();
    }

    // This function checks to see if a state needs to transition, and returns the state the needs to be stored
    // in StateMachine.currentState
    public abstract State checkTransition();
    

    
    // This function adds the goal state to the list of transition states. Can be overrideen, but normally
    // doesn't need to be.
    public virtual void addTransition(State goalState)
    {
        transitions.Add(goalState);
    }

    

    // This function should be overridden should any inherting class need to do something
    // every frame
    public virtual void OnUpdate()
    {

    }
    

    

}


