using System.Collections.Generic;
using UnityEngine;

public class StageStateFactoryDefault : StateFactoryBase
{
    public override Dictionary<StageState, StateBase> CreateState(StateServiceBase service, StateContext context)
    {
        StageStateServiceDefault defaultService = service as StageStateServiceDefault;
        Dictionary<StageState, StateBase> defaultStateTable = new Dictionary<StageState, StateBase>
        {
            {StageState.Idle, new StageStateIdle(defaultService, context)},
            {StageState.Rolling, new StageStateRolling(defaultService, context)},
        };

        return defaultStateTable;
    }
}
