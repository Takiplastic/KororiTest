using UnityEngine;

public class StageStateRolling : StateBase
{
   protected new StageState _state => StageState.Rolling;
    private StateContext _context = null;
    private ClassB classB = null;
    private ClassC classC = null;

    public StageStateRolling(StageStateServiceDefault service, StateContext context)
    {
        _context = context;
        classB = service.ClassB;
        classC = service.ClassC;
    }

    public override void Enter()
    {
        Debug.Log("Entered : Rolling");
        classB.Show();
        classC.Show();
    }

    public override void Exit()
    {
        Debug.Log("Exit : Rolling");
    }
}
