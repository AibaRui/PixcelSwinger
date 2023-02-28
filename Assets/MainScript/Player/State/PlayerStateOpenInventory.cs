using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateOpenInventory : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.Rb.velocity = Vector3.zero;
        _stateMachine.PlayerController.InventoryManager.InventoryOpen();
    }

    public override void Exit()
    {
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
            _stateMachine.PlayerController.GameSetting.CheckChange();

            if (_stateMachine.PlayerController.GameSetting.IsChangeSetting)
            {
                Debug.Log("a");
            }
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                Debug.Log("Inventory=>Idle");
            }
        }

    }


}
