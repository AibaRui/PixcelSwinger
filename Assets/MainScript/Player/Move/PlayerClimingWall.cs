using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimingWall : MonoBehaviour
{
    [SerializeField] private WallCheck _wallCheck;

    /// <summary>現在進行形で、壁を登っている最中かどうか</summary>
    private bool _isClimbing = true;

    private Rigidbody _rb;

    public bool IsClimb { get => _isClimbing; set => _isClimbing = value; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>壁のぼりを実行する関数</summary>
    public void Climb()
    {
        //壁のぼりが可能な状態だったら
        if (_wallCheck.DownWall)
        {
            //Y軸のみ速度をつけて、垂直に上げる
            Vector3 velo = new Vector3(0, 20, 0);
            _rb.velocity = velo;
        }
        else
        {
            //壁を登り切ったら、前方に押し出して壁のぼりを終了
            _rb.velocity = Vector3.zero;
            _rb.AddForce(transform.up * 3, ForceMode.Impulse);
            _rb.AddForce(transform.forward * 5, ForceMode.Impulse);

            _isClimbing = false;
        }
    }
}
