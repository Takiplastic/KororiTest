using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StateFactoryBase _stateFactory;
    [SerializeField] private StateServiceBase _stateService;

    private StateContext _context;

    private void Awake()
    {
        _context = new StateContext(_stateFactory, _stateService);
    }

    private void Start()
    {
        _context.ChangeState(StageState.Idle);
    }
}
