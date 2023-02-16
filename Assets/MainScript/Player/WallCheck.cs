using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [Header("登れる壁のレイヤー")]
    [SerializeField] LayerMask _layerClimbWall;

    [Header("正面のRay")]
    [SerializeField] float _boxCastFowardMidleX = 1;
    [SerializeField] float _boxCastFowardMidleY = 1;
    [SerializeField] float _boxCastFowardMidleZ = 1;
    [SerializeField] Vector3 _posAddFowardMidle;

    [Header("正面上のRayの上の長さ")]
    [SerializeField] float _upWallCheckLayLongY = 4;

    [Header("正面上のRayの正面の長さ")]
    [SerializeField] float _upWallCheckLayLongZ = 3;

    [Header("正面下のRayの上の長さ")]
    [SerializeField] float _downWallCheckLayLongY = 4;

    [Header("正面下のRayの正面の長さ")]
    [SerializeField] float _downWallCheckLayLongZ = 3;


    /// <summary>前方に壁があるかどうか</summary>
    private bool _fowardWall;

    /// <summary>登る壁の上に壁があるかどうか</summary>
    private bool _UpWall;

    private bool _downWall;

    public bool FowardWall { get => _fowardWall; }

    public bool UpWall { get => _UpWall; }

    public bool DownWall { get => _downWall; }

    RaycastHit hitFowardWal;

    private bool _isClimb;

    public bool IsClimb { get => _isClimb; }


    //壁走りの設定

    [Header("横のRayの長さ(WalllRun用)")]
    [SerializeField] float _checkWallRayDistace = 2f;

    [Header("壁のレイヤー")]
    [SerializeField] LayerMask _wallLayer;

    /// <summary>右の壁に当たっているかどうか</summary>
    bool _rightWallHit = false;
    /// <summary>右の壁に当たっているかどうか</summary>
    bool _leftWallHit = false;
    RaycastHit m_rightWall;
    RaycastHit m_leftWall;

    /// <summary>壁RUN中</summary>
    bool _isWallRun;
    /// <summary>壁RUNができるかどうか</summary>
    bool _okWallRun = true;

    /// <summary>壁RUNの進む方向</summary>
    Vector3 wallForward;


    Vector3 jumpFowrd;

    void Start()
    {

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.forward + _posAddFowardMidle, 
            new Vector3(_boxCastFowardMidleX, _boxCastFowardMidleY, _boxCastFowardMidleZ));

        //正面上のRay
        Vector3 startUp1 = this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp1 = startUp1 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp2 = transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp2 = startUp2 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp3 = -transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp3 = startUp3 + transform.forward * _upWallCheckLayLongZ;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startUp1, endUp1);
        Gizmos.DrawLine(startUp2, endUp2);
        Gizmos.DrawLine(startUp3, endUp3);

        //正面下のRay
        Vector3 startDown = this.transform.position + -transform.up * _downWallCheckLayLongY;
        Vector3 endDown = startDown + transform.forward * _downWallCheckLayLongZ;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startDown, endDown);


        //WallRUn用横のRay
        Gizmos.color = Color.yellow;
        Vector3 rightEnd = transform.position + transform.right * _checkWallRayDistace;
        Vector3 leftEnd = transform.position + -transform.right * _checkWallRayDistace;
        Gizmos.DrawLine(transform.position, rightEnd);
        Gizmos.DrawLine(transform.position, leftEnd);
    }


    /// <summary>段差チェック</summary>
    public void CheckClimbWall()
    {
        //上のRay(正面の上に壁があるかどうか)
        Vector3 startUp1 = this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp1 = startUp1 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp2 = transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp2 = startUp2 + transform.forward * _upWallCheckLayLongZ;

        Vector3 startUp3 = -transform.right + this.transform.position + transform.up * _upWallCheckLayLongY;
        Vector3 endUp3 = startUp3 + transform.forward * _upWallCheckLayLongZ;

        bool upWallMidle = Physics.Linecast(startUp1, endUp1, _layerClimbWall);
        bool upWallRight = Physics.Linecast(startUp2, endUp2, _layerClimbWall);
        bool upWallLeft = Physics.Linecast(startUp3, endUp3, _layerClimbWall);

        //下のRay(正面の足元に壁があるかどうか)
        Vector3 startDown = this.transform.position + -transform.up * _downWallCheckLayLongY;
        Vector3 endDown = startDown + transform.forward * _downWallCheckLayLongZ;

        _downWall = Physics.Linecast(startDown, endDown, _layerClimbWall);

        //正面の壁
        _fowardWall = Physics.BoxCast(transform.position + _posAddFowardMidle, new Vector3(_boxCastFowardMidleX, _boxCastFowardMidleY, _boxCastFowardMidleZ), transform.forward, out hitFowardWal, Quaternion.identity, 1.0f, _layerClimbWall);



        if ((!upWallMidle && !upWallLeft && ! upWallRight) &&( _fowardWall || _downWall))
        {
            _isClimb = true;
        }
        else
        {
            _isClimb = false;
        }
    }




    /// <summary>壁に近いかどうかを判断する</summary>
    public bool CheckWallLeft()
    {
        _leftWallHit = Physics.Raycast(transform.position, -transform.right, out m_leftWall, _checkWallRayDistace, _wallLayer);
        return _leftWallHit;
    }


    /// <summary>壁に近いかどうかを判断する</summary>
    public bool CheckWallRight()
    {
        _rightWallHit = Physics.Raycast(transform.position, transform.right, out m_rightWall, _checkWallRayDistace, _wallLayer);
        return _rightWallHit;
    }

}
