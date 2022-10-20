using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sliding : MonoBehaviour
{
    [Header("スライディングの速さ")]
    [SerializeField] float _slidingSpeed = 5;

    [Header("メインのカメラ")]
    [SerializeField] CinemachineVirtualCamera _camera;
    CinemachineTransposer _cameraTransposer;

    /// <summary>スライディングできるかどうか</summary>
    bool _okSliding = true;


    P_Control _control;
    PlayerMove m_playerMove;

    //   [SerializeField] AudioSource m_aud;
    Animator m_anim;
    Rigidbody m_rb;
    void Start()
    {
        m_playerMove = FindObjectOfType<PlayerMove>();
        _cameraTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();

        _control = GetComponent<P_Control>();

        m_rb = gameObject.GetComponent<Rigidbody>();
        m_anim = gameObject.GetComponent<Animator>();
        //   m_aud = m_aud.GetComponent<AudioSource>();
    }


    void Update()
    {
        if (!_control._isGrapple && !_control._isSwing && !_control._isWallRun)
        {
            SlidingGo();
        }
    }

    void SlidingGo()
    {
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("1");
            //移動しながらしゃがんだらスライディング（クールタイムあり）
            if (v > 0 && _okSliding)
            {
                Debug.Log("2");
                _okSliding = false;
                _control._isSliding = true;
                m_playerMove._isSliding = true;

                m_rb.AddForce(transform.forward * _slidingSpeed, ForceMode.Impulse);
                StartCoroutine(EndSlid());
            }
        }
        //ctrlを押している間、カメラの高さを下げる
        if (Input.GetKeyDown(KeyCode.LeftControl) && _control._isGround)
        {
            //_cameraTransposer.m_FollowOffset.y = 0.6f;
            transform.localScale = new Vector3(1, 0.3f, 1);
            _control._isSquat = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)&& _control._isGround)
        {
            m_rb.velocity = Vector3.zero;
            _control._isSquat = false;

            transform.localScale = new Vector3(1, 1f, 1);
            _cameraTransposer.m_FollowOffset.y = 1.5f;
            _control._isSliding = false;
            if (!_okSliding)
            {
                StartCoroutine(CoolTime());
            }
        }
    }

    public void StopSquat()
    {
        _control._isSquat = false;
        _control._isSliding = false;
        _cameraTransposer.m_FollowOffset.y = 1.5f;
        _okSliding = true;
    }

    IEnumerator EndSlid()
    {
        yield return new WaitForSeconds(2);
        m_playerMove._isSliding = false;
        _control._isSliding = false;
    }

    /// <summary>スライディングのクールタイム</summary>
    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(1);
        _okSliding = true;
    }
}
