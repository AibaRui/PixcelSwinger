using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateOpenInventory : PlayerStateBase
{
    public override void Enter()
    {
        //プレイヤーの動きを止める
        _stateMachine.PlayerController.Rb.velocity = Vector3.zero;

        //インベントリを開いている間は、アシストパネルを出す計算時間に含めない
        _stateMachine.PlayerController.IsMove = false;

        //インベントリのUIをActiveにする
        _stateMachine.PlayerController.InventoryManager.InventoryOpen();

        //インベントリを開く音
        _stateMachine.PlayerController.AudioManager.OpenInvrntory();
    }

    public override void Exit()
    {
        //動いている
        _stateMachine.PlayerController.IsMove = false;

        //インベントリのUIをActiveFalseにする
        _stateMachine.PlayerController.InventoryManager.InventoryClose();
    }

    public override void FixedUpdate()
    {

    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        if (_stateMachine.PlayerController.PlayerInput.IsTabDown)
        {
            //ゲーム設定の変更内容を適用したか
            _stateMachine.PlayerController.GameSetting.CheckChange();
            //音量設定の変更内容を適用したか
            _stateMachine.PlayerController.AudioSetting.CheckDoSetting();

            if (_stateMachine.PlayerController.GameSetting.IsChangeSetting || _stateMachine.PlayerController.AudioSetting.IsChange)
            {
                Debug.Log("-------変更あり");
            }
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Inventory=>Idle");
            }
        }

    }


}
