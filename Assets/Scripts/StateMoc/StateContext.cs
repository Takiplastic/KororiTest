using UnityEngine;
using System.Collections.Generic;

public class StateContext
{
    private StateFactoryBase _stateFactory = null;
    private StateServiceBase _stateService = null;

    public Dictionary<StageState, StateBase> _stateMap = null;

    public StateBase _currentState = null;

    public StateContext(StateFactoryBase stateFactory, StateServiceBase stateService)
    {
        _stateFactory = stateFactory;
        _stateService = stateService;

        _stateMap = _stateFactory.CreateState(_stateService, this);
    }

    public void ChangeState(StageState state)
    {
        StateBase newState = _stateMap[state];
        if (newState == null) return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();

    }
}
