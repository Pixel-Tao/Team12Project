using UnityEngine;

public class PlayerDodgeState : PlayerRunState
{
    public PlayerDodgeState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Input.OnMoveEvent += MoveHandle;
        stateMachine.Input.OnMoveCancelEvent += MoveCancelHandle;
        stateMachine.Input.OnLookEvent += LookHandle;
        StartAnimation(stateMachine.AnimationData.GroundHash);
        StartAnimation(stateMachine.AnimationData.RunHash);
        StartAnimation(stateMachine.AnimationData.DodgeHash);
        // TODO : 재사용 대기시간 설정
        TryApplyForce();
    }

    public override void Exit()
    {
        stateMachine.Input.OnMoveEvent -= MoveHandle;
        stateMachine.Input.OnMoveCancelEvent -= MoveCancelHandle;
        stateMachine.Input.OnLookEvent -= LookHandle;
        stateMachine.IsDodging = false;
        StopAnimation(stateMachine.AnimationData.GroundHash);
        StopAnimation(stateMachine.AnimationData.RunHash);
        StopAnimation(stateMachine.AnimationData.DodgeHash);
    }

    private void TryApplyForce()
    {
        if (stateMachine.IsDodging) return;
        stateMachine.IsDodging = true;

        stateMachine.Player.ForceReceiver.Reset();
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.MoveDirection * stateMachine.Player.DodgeForce);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Dodge");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.RunState);
        }
    }
}