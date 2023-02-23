using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossHamster : Boss
{
    [SerializeField] private Transform _shotPoint1;
    [SerializeField] private Transform _shotPoint2;
    [SerializeField] private GameObject _bullet;

    private RotateToTarget _rotateToTarget;
    private BossHamsterAnimationController _animationController;
    private StateMachine _stateMachine;
    private State _idleState;
    private State _runState;
    private State _shootState;

    protected override void Start()
    {
        base.Start();

        SetMoveBehaviour(new MoveToTarget(transform, _speed, transform.position));
        _rotateToTarget = new RotateToTarget(_mainScript.MainCamera);
        _animationController = new BossHamsterAnimationController(_animator);
        _idleState = new IdleStateHamster(_animationController);
        _runState = new RunStateHamster(this,_player, _animationController);
        _shootState = new ShootStateHamster(_animationController,_shotPoint1, _shotPoint2, _bullet);
        _stateMachine = new StateMachine();
        _stateMachine.Initialize(_shootState);
        StartCoroutine(AIMenu());
    }

    protected override void Update()
    {
        base.Update();

        _stateMachine.CurrentState.Update();
    }

    private IEnumerator AIMenu()
    {
        float waitTime = 1f;
        int i = Random.Range(0, 11);

        if (_player.activeInHierarchy)
        {
            if (i == 0)
            {
                _stateMachine.ChangeState(_idleState);
                waitTime = Random.Range(0.5f, 1f);
            }
            else if (i > 0 && i < 6)
            {
                _stateMachine.ChangeState(_shootState);
                waitTime = Random.Range(2f, 5f);
            }
            else
            {
                _stateMachine.ChangeState(_runState);
                waitTime = Random.Range(1f, 3f);
            }
        }
        else
        {
            _stateMachine.ChangeState(_idleState);
        }

        yield return new WaitForSeconds(waitTime);
        StartCoroutine(AIMenu());
    }
}
