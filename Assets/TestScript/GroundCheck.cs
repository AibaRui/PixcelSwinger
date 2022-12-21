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


    /// <summary>設置判定</summary>
    public bool IsGround { get; private set; }

    private RaycastHit _hitGround;

    private void Update()
    {
        IsGround = CheckFowardWall();
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
        Gizmos.DrawWireCube(transform.position + transform.forward + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ));
    }
}
