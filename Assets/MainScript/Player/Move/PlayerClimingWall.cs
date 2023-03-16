using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimingWall : MonoBehaviour
{
    [SerializeField] private WallCheck _wallCheck;

    /// <summary>���ݐi�s�`�ŁA�ǂ�o���Ă���Œ����ǂ���</summary>
    private bool _isClimbing = true;

    private Rigidbody _rb;

    public bool IsClimb { get => _isClimbing; set => _isClimbing = value; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>�ǂ̂ڂ�����s����֐�</summary>
    public void Climb()
    {
        //�ǂ̂ڂ肪�\�ȏ�Ԃ�������
        if (_wallCheck.DownWall)
        {
            //Y���̂ݑ��x�����āA�����ɏグ��
            Vector3 velo = new Vector3(0, 20, 0);
            _rb.velocity = velo;
        }
        else
        {
            //�ǂ�o��؂�����A�O���ɉ����o���ĕǂ̂ڂ���I��
            _rb.velocity = Vector3.zero;
            _rb.AddForce(transform.up * 3, ForceMode.Impulse);
            _rb.AddForce(transform.forward * 5, ForceMode.Impulse);

            _isClimbing = false;
        }
    }
}
