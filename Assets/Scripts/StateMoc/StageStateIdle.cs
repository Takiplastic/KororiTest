using System.Runtime.InteropServices;
using UnityEngine;

public class StageStateIdle : StateBase
{
    protected new StageState _state => StageState.Idle;
    private StateContext _context = null;
    private ClassA classA = null;

    public StageStateIdle(StageStateServiceDefault service, StateContext context)
    {
        _context = context;
        classA = service.ClassA;
    }

    public override void Enter()
    {
        Debug.Log("Entered : Idle");
        classA.Show();
        _context.ChangeState(StageState.Rolling);
    }

    public override void Exit()
    {
        Debug.Log("Exit : Idle");
    }
}
