using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("地面として認識するレイヤー")]
    [SerializeField] LayerMask _layer;

    [Header("BoxCastの範囲")]
    [SerializeField] private float _boxCastX = 1;
    [SerializeField] private float _boxCastY = 1;
    [SerializeField] private float _boxCastZ = 1;

    [SerializeField] private Vector3 posAdd;

    [SerializeField] float _isGroundedLength = 1;

    [SerializeField] Animator _gunAnim;
    /// <summary></summary>
    public bool IsGround { get; private set; }

    private RaycastHit _hitGround;


    Collider[] _col;

    private void Update()
    {
        IsGround = C();
    }



    public bool C()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center + new Vector3(0, 0, -0.3f);   // start: 体の中心
        Vector3 end = start + Vector3.down * _isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end, Color.green); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする


        _col = Physics.OverlapBox(transform.position + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ), Quaternion.identity, _layer);
        _gunAnim.SetBool("IsGround", _col.Length > 0 ? true : false);
        return _col.Length > 0 ? true : false;
    }
    public bool CheckFowardWall()
    {
        bool isGround = Physics.BoxCast(transform.position + transform.forward +
            posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ),
            transform.forward, out _hitGround, Quaternion.identity, 1.0f, _layer);
        return isGround;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ));
    }
}
