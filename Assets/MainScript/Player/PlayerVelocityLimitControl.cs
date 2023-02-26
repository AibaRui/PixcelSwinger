using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityLimitControl : MonoBehaviour
{
    [Header("初心者_移動速度の制限")]
    [SerializeField] private Vector2 _limitSppedOfEazy = new Vector2(30, 40);

    [Header("初心者_移動速度の制限。ワイヤーが短いとき")]
    [SerializeField] private Vector2 _limitSppedOfEazyOfWireShort = new Vector2(20, 30);

    [Header("中級_移動速度の制限")]
    [SerializeField] private Vector2 _limitSppedOfNomal = new Vector2(60, 70);

    [Header("中級_移動速度の制限。ワイヤーが短いとき")]
    [SerializeField] private Vector2 _limitSppedOfNomalOfWireShort = new Vector2(40, 50);

    [Header("上級_移動速度の制限")]
    [SerializeField] private Vector2 _limitSppedOfHard = new Vector2(100, 80);

    [Header("上級_移動速度の制限。ワイヤーが短いとき")]
    [SerializeField] private Vector2 _limitSppedHardOfWireShort = new Vector2(50, 60);




    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerGrappleAndSwingSetting _playerGrappleAndSwingSetting;

    Rigidbody _rb;




    void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
    }


    public void VelocityLimit()
    {

        float limitSpeed = 0;
        float limitSpeedY = 0;

        if (_playerController.OperationLevel == PlayerController.OperationsLevel.Eazy)
        {
            if (_playerGrappleAndSwingSetting.WireLongEnum == PlayerGrappleAndSwingSetting.WireLong.Short)
            {
                limitSpeed = _limitSppedOfEazyOfWireShort.x;
                limitSpeedY = _limitSppedOfEazyOfWireShort.y;
            }
            else
            {
                limitSpeed = _limitSppedOfEazy.x;
                limitSpeedY = _limitSppedOfEazy.y;
            }
        }
        else if (_playerController.OperationLevel == PlayerController.OperationsLevel.Nomal)
        {
            if (_playerGrappleAndSwingSetting.WireLongEnum == PlayerGrappleAndSwingSetting.WireLong.Short)
            {
                limitSpeed = _limitSppedOfNomalOfWireShort.x;
                limitSpeedY = _limitSppedOfNomalOfWireShort.y;
            }
            else
            {
                limitSpeed = _limitSppedOfNomal.x;
                limitSpeedY = _limitSppedOfNomal.y;
            }
        }
        else
        {
            if (_playerGrappleAndSwingSetting.WireLongEnum == PlayerGrappleAndSwingSetting.WireLong.Short)
            {
                limitSpeed = _limitSppedHardOfWireShort.x;
                limitSpeedY = _limitSppedHardOfWireShort.y;

            }
            else
            {
                limitSpeed = _limitSppedOfHard.x;
                limitSpeedY = _limitSppedOfHard.y;
            }
        }



        if (_rb.velocity.x > limitSpeed)
        {
            _rb.velocity = new Vector3(limitSpeed, _rb.velocity.y, _rb.velocity.z);
        }
        else if (_rb.velocity.x < -limitSpeed)
        {
            _rb.velocity = new Vector3(-limitSpeed, _rb.velocity.y, _rb.velocity.z);
        }

        if (_rb.velocity.z > limitSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, limitSpeed);
        }
        else if (_rb.velocity.z < -limitSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, -limitSpeed);
        }

        if (_rb.velocity.y > limitSpeedY)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, limitSpeedY, _rb.velocity.z);
        }

        if (_rb.velocity.y < -limitSpeedY)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, -limitSpeedY, _rb.velocity.z);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
