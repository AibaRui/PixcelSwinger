using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateEventStop : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.TalkManager.DoTalk();
        _stateMachine.PlayerController.AudioManager.StartTalk();

        //�b���Ă���Ԃ́A�A�V�X�g�p�l�����o���v�Z���ԂɊ܂߂Ȃ�
        _stateMachine.PlayerController.IsMove = false;
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.IsMove = true;
    }

    public override void FixedUpdate()
    {

    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        if(!_stateMachine.PlayerController.EventController.IsEventNow)
        {
            _stateMachine.PlayerController.EventEriaCheck.IsEnterEventEria = false;
            _stateMachine.TransitionTo(_stateMachine.StateIdle);
            Debug.Log("Event=>Idle");
            return;
        }
    }
}
