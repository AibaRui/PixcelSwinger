using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallRunTest : MonoBehaviour
{
    [Header("���H")]
    [SerializeField] private bool _isOnKakou = false;

    [Header("Ray�̒���")]
    [SerializeField] private float m_checkWallRayDistace = 2f;

    [Header("���ʂ̕ǂ̔���̒���")]
    [SerializeField] private float _layLongOfCheckForwardWallOfEndWallRun = 5;

    [Header("�ǂ̃��C���[")]
    [SerializeField] private LayerMask _wallLayer;


    [Header("�ǂ̃��C���[")]
    [SerializeField] private Text _normal;

    [Header("����-�O��")]
    [SerializeField] private Text _syoumenHikuGaiseki;

    [Header("����-(-�O��)")]
    [SerializeField] private Text _syoumenHikuMainasuGaiseki;

    [Header("����-�O�ρE����")]
    [SerializeField] private Text _syoumenHikuGaisekiLong;

    [Header("����-(-�O��)�E����")]
    [SerializeField] private Text _syoumenHikuMainasuGaiseki_Long;

    bool _isHit = false;

    /// <summary>��RUN�̐i�ޕ���</summary>
    private Vector3 _wallForward;

    Vector3 _housen;

    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    private bool _rightWallHit = false;
    /// <summary>�E�̕ǂɓ������Ă��邩�ǂ���</summary>
    private bool _leftWallHit = false;

    /// <summary>�v���C���[�̎��_�̕����ɕǂ����邩�ǂ���</summary>
    private bool _fowardWallHit = false;


    /// <summary>WallRun�̎n�܂�́A�ǂ̕����Btrue:�E/false:��</summary>
    private bool _startHitRight;

    /// <summary>���g�̉E���̕ǂ���������Raycast</summary>
    private RaycastHit _rightWall;
    /// <summary>���g�̍����̕ǂ���������Raycast</summary>
    private RaycastHit _leftWall;
    /// <summary>���g�̐��ʂ̕ǂ���������Raycast</summary>
    private RaycastHit _fowardWall;


    public Vector3 WallForward => _wallForward;



    private void Update()
    {
        WallRuning();

    }

    /// <summary>WallRun���s������֐�
    /// 1:�O�ς�p���āA�i�s���������߂�
    /// 2:�J�������X������
    /// 3:���x��������֐����Ă�</summary>
    public void WallRuning()
    {
        //�E�̕ǂɓ����葱���Ă�����
        if (CheckWallRight())
        {
            _isHit = true;

            //�@�������
            _housen = _rightWall.normal;
            //�O�ς��g���A�i�s���������
            _wallForward = Vector3.Cross(_housen, transform.up);

           // Debug.Log("�O��:" + _wallForward);

            if (_isOnKakou)
            {

                Debug.Log("�v���C���[�̐��ʂ̒���"+transform.forward.magnitude.ToString("F5"));
                Debug.Log("�O�ς̒���:" + _wallForward.magnitude.ToString("F5"));

                //�O�σx�N�g�����}�C�i�X�������ꍇ�A������ς���B
                if ((transform.forward - _wallForward).magnitude > (transform.forward - -_wallForward).magnitude)
                {
                    Debug.Log("�v�Z:" + (transform.forward - _wallForward).magnitude + ">" + (transform.forward - -_wallForward).magnitude);
                    _wallForward = -_wallForward;
                }


                    _syoumenHikuGaiseki.text = (transform.forward - _wallForward).ToString();
                    _syoumenHikuGaisekiLong.text = (transform.forward - _wallForward).magnitude.ToString();

                    _syoumenHikuMainasuGaiseki.text = (transform.forward - -_wallForward).ToString();
                    _syoumenHikuMainasuGaiseki_Long.text = (transform.forward - -_wallForward).magnitude.ToString();



            }
        }
        //�E�̕ǂɓ����葱���Ă�����
        else if (CheckWallLeft())
        {
            _isHit = true;

            //�@�������
            _housen = _leftWall.normal;
            //�O�ς��g���A�i�s���������
            _wallForward = Vector3.Cross(_housen, transform.up);

            if (_isOnKakou)
            {
                if ((transform.forward - _wallForward).magnitude > (transform.forward - -_wallForward).magnitude)
                {
                    _wallForward = -_wallForward;
                }


            }


            _syoumenHikuGaiseki.text = (transform.forward - _wallForward).ToString();
            _syoumenHikuGaisekiLong.text = (transform.forward - _wallForward).magnitude.ToString();

            _syoumenHikuMainasuGaiseki.text = (transform.forward - -_wallForward).ToString();
            _syoumenHikuMainasuGaiseki_Long.text = (transform.forward - -_wallForward).magnitude.ToString();

        }
        else
        {
            _isHit = false;
        }
    }


    private void OnDrawGizmos()
    {
        //�����
        Debug.DrawLine(transform.position, transform.position + transform.up * 4, Color.green);

        if (_isHit)
        {
            //������
            Debug.DrawLine(transform.position, transform.position + (_housen * 4), Color.red);
            Debug.DrawLine(transform.position, transform.position + (_housen * -4), Color.red);

            Debug.DrawLine(transform.position, transform.position + _wallForward * 10, Color.blue);


            Debug.DrawLine(transform.position, (transform.forward - _wallForward) * 2, Color.white);


            Debug.DrawLine(transform.position, (transform.forward - -_wallForward) * 2, Color.gray);


            Debug.DrawLine(transform.position, transform.position + transform.forward * 4,Color.black);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + (transform.right * 4), Color.yellow);
            Debug.DrawLine(transform.position, transform.position + (transform.right * -4), Color.yellow);

        }
    }



    public bool CheckWallLeft()
    {
        _leftWallHit = Physics.Raycast(transform.position, -transform.right, out _leftWall, m_checkWallRayDistace, _wallLayer);

        if(_leftWall.collider!=null)
        {
 
        }

        return _leftWallHit;
    }


    /// <summary>�ǂɋ߂����ǂ����𔻒f����</summary>
    public bool CheckWallRight()
    {
        _rightWallHit = Physics.Raycast(transform.position, transform.right, out _rightWall, m_checkWallRayDistace, _wallLayer);

        if(_rightWall.collider!=null)
        {
        Debug.Log("�E�̕�;" + _rightWall.collider.transform.position);
        }

        return _rightWallHit;
    }
}
