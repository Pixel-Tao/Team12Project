using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }
    public Animator Animator => Player?.Animator;
    public AnimationData AnimationData => Player?.AnimationData;
    public Condition Condition => Player?.Condition;
    public InputController Input => Player?.Input;
    
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerRunForwardState RunForwardState { get; private set; }
    public PlayerRunBackwardState RunBackwordState { get; private set; }
    public PlayerRunLeftStepState RunLeftStepState { get; private set; }
    public PlayerRunRightStepState RunRightStepState { get; private set; }
    public PlayerDodgeState DodgeState { get; private set; }
    

    public Vector3 LookDirection { get; set; }
    public Vector3 MoveDirection { get; set; }

    public float MaxMoveSpeed { get; private set; }
    public Defines.CharacterMovementType MovementType { get; set; }
    
    public PlayerStateMachine(Player player)
    {
        Player = player;
        IdleState = new PlayerIdleState(this);
        DodgeState = new PlayerDodgeState(this);
        
        RunState = new PlayerRunState(this);
        RunForwardState = new PlayerRunForwardState(this);
        RunBackwordState = new PlayerRunBackwardState(this);
        RunLeftStepState = new PlayerRunLeftStepState(this);
        RunRightStepState = new PlayerRunRightStepState(this);
    }
}