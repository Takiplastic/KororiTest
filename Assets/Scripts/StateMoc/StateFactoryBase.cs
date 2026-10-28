using System.Collections.Generic;
using UnityEngine;

public abstract class StateFactoryBase : MonoBehaviour
{
    public abstract Dictionary<StageState, StateBase> CreateState(StateServiceBase service, StateContext context); 
}
