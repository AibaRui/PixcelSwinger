using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateGrapple : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PlayerGrapple.StartGrapple();
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.Joint = _stateMachine.PlayerController.Player.GetComponent<SpringJoint>();

        //�e�̃A�j���[�V����
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingOrGrappleAinm();

        //���C���[���k�߂鉹�𗬂�
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.WireSound();

        //�󒆃W�����v�̉񐔒���
        _stateMachine.PlayerController.PlayerJumping.ReSetAirJump();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.PlayerGrapple.StopGrapple();

        if (_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckPointIsHit())
        {
            _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.EndGrappleOrSwing();
        }


        //���[�v���Ă��鉹(Wire�̉����~�߂�)
        _stateMachine.PlayerController.AudioManager.StopLoopPlayerSE();
    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.PlayerGrapple.GrappleMove();

        //���x�����̃X�N���v�g
        _stateMachine.PlayerController.PlayerVelocityLimitControl.VelocityLimit();
        //Hit! ��UI���o��
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.ActivePointer();
    }

    public override void LateUpdate()
    {
        //���`��
        _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.DrawRope();
    }

    public override void Update()
    {

        //��������Swing�𒆎~
        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickUp || !_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckPointIsHit())
        {
            //�㏸
            if (_stateMachine.PlayerController.Rb.velocity.y > 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                //Grapple�I���̉���炷
                _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingAndGrappleEndSound();

                _stateMachine.TransitionTo(_stateMachine.StateUpAir);

                Debug.Log("Grapple=>UpAir");
                return;
            }
            //���~
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0 && !_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                //Grapple�I���̉���炷
                _stateMachine.PlayerController.PlayerSwingAndGrappleSetting.SwingAndGrappleEndSound();

                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                Debug.Log("Grapple=>DownAir");
                return;
            }
        }

        var h = _stateMachine.PlayerController.PlayerInput.HorizontalInput;
        var v = _stateMachine.PlayerController.PlayerInput.VerticalInput;

        if (_stateMachine.PlayerController.PlayerInput.IsLeftMouseClickUp || !_stateMachine.PlayerController.PlayerSwingAndGrappleSetting.CheckPointIsHit())
        {
            if (_stateMachine.PlayerController.GroundCheck.IsGround)
            {
                if ((h != 0 && v != 0))
                {
                    _stateMachine.TransitionTo(_stateMachine.StateIdle);
                    Debug.Log("Grapple=>Idle");
                    return;
                }
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    Debug.Log("Grapple=>Move");
                }
            }
        }



    }

}

