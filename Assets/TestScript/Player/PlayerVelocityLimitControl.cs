using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityLimitControl : MonoBehaviour
{
    public ControlLevel _moveControlLevel = ControlLevel.Eazy; 

    [Header("初心者_前後左右の移動速度の制限")]
    [SerializeField] private float _limitSppedOfEazy = 15;

    [Header("初心者_上下の移動速度の制限")]
    [Tooltip("初心者_上下の移動速度の制限")] [SerializeField] private float _limitSppedYOfEzay = 20;


    [Header("普通_前後左右の移動速度の制限")]
    [SerializeField] private float _limitSppedOfNomal;


    [SerializeField] PlayerController _playerController;

    Rigidbody _rb;

    public enum ControlLevel
    {
        Eazy,
        Nomal,
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

        if (_moveControlLevel == ControlLevel.Eazy)
        {
            limitSpeed = _limitSppedOfEazy;
            limitSpeedY = _limitSppedYOfEzay;
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
