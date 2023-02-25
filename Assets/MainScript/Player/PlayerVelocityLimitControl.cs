using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityLimitControl : MonoBehaviour
{
    [Header("初心者_前後左右の移動速度の制限")]
    [SerializeField] private float _limitSppedOfEazy = 30;

    [Header("初心者_上下の移動速度の制限")]
    [Tooltip("初心者_上下の移動速度の制限")] [SerializeField] private float _limitSppedYOfEzay = 40;

    [Header("初心者_前後左右。ワイヤーのが短いとき")]
    [SerializeField] private float _limitSppedOfEazyOfWireShort = 15;

    [Header("初心者_上下。ワイヤーのが短いとき")]
    [SerializeField] private float _limitSppedOfEazyYOfWireShort = 15;

    [Header("中級_前後左右の移動速度の制限")]
    [SerializeField] private float _limitSppedOfNomal = 25;

    [Header("中級_上下の移動速度の制限")]
    [SerializeField] private float _limitSppedYOfNomal = 30;

    [Header("中上_前後左右。ワイヤーのが短いとき")]
    [SerializeField] private float _limitSppedOfNomalOfWireShort = 30;

    [Header("中上_上下。ワイヤーのが短いとき")]
    [SerializeField] private float _limitSppedOfNomalYOfWireShort = 30;

    [Header("上級_前後左右の移動速度の制限")]
    [SerializeField] private float _limitSppedOfHard = 35;

    [Header("上級_上下の移動速度の制限")]
    [SerializeField] private float _limitSppedYOfHard = 40;




    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerGrappleAndSwingSetting _playerGrappleAndSwingSetting;

    Rigidbody _rb;

    [SerializeField] OperationsLevel _firstOperationLevl = OperationsLevel.Eazy;

    public OperationsLevel FirstOperationLevel => _firstOperationLevl;

    private OperationsLevel _operationLevl = OperationsLevel.Eazy;

    public OperationsLevel OperationLevel { get => _operationLevl; set => _operationLevl = value; }

    public enum OperationsLevel
    {
        Eazy,
        Nomal,
        Hard,
    }


    void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
    }


    public void VelocityLimit()
    {

        float limitSpeed = 0;
        float limitSpeedY = 0;

        if (_operationLevl == OperationsLevel.Eazy)
        {
            if (_playerGrappleAndSwingSetting.WireLongEnum == PlayerGrappleAndSwingSetting.WireLong.Short)
            {
                limitSpeed = _limitSppedOfEazyOfWireShort;
                limitSpeedY = _limitSppedOfEazyYOfWireShort;
            }
            else
            {
                limitSpeed = _limitSppedOfEazy;
                limitSpeedY = _limitSppedYOfEzay;
            }
        }
        else if (_operationLevl == OperationsLevel.Nomal)
        {
            if (_playerGrappleAndSwingSetting.WireLongEnum == PlayerGrappleAndSwingSetting.WireLong.Short)
            {
                limitSpeed = _limitSppedOfNomalOfWireShort;
                limitSpeedY = _limitSppedOfNomalYOfWireShort;
            }
            else
            {
                limitSpeed = _limitSppedOfNomal;
                limitSpeedY = _limitSppedYOfNomal;
            }
        }
        else
        {
            if (_playerGrappleAndSwingSetting.WireLongEnum == PlayerGrappleAndSwingSetting.WireLong.Short)
            {
                limitSpeed = _limitSppedOfNomalOfWireShort;
                limitSpeedY = _limitSppedOfNomalYOfWireShort;
                Debug.Log("aaa");
            }
            else
            {
                limitSpeed = _limitSppedOfHard;
                limitSpeedY = _limitSppedYOfHard;
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
