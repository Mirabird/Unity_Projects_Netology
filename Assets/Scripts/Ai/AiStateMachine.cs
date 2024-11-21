﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{
    public AiState[] states;
    public AiAgent agent;
    public AiStateId currentState;

    public AiStateMachine(AiAgent agent) {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        states = new AiState[numStates];
    }

    public void RegisterState(AiState state) {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public AiState GetState(AiStateId stateId) {
        int index = (int)stateId;
        return states[index];
    }

    public void Update() {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(AiStateId newState) {
        var state = GetState(newState);
        if (state == null) {
            Debug.LogError($"{newState} State has not been registered");
        }

        if (newState != currentState) {
            GetState(currentState)?.Exit(agent);
            currentState = newState;
            GetState(currentState)?.Enter(agent);
        }
    }
}
