using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    //State we want to play
    public State currentState;

    void Update()
    {
        //Run Logic
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        //Set nextState to the the state that gets returned
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            //Switch to next state
            SwitchState(nextState);
        }
    }

    private void SwitchState(State nextState)
    {
        currentState = nextState;
    }
}
