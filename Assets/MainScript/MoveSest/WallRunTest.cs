using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallRunTest : MonoBehaviour
{
    [Header("加工")]
    [SerializeField] private bool _isOnKakou = false;

    [Header("Rayの長さ")]
    [SerializeField] private float m_checkWallRayDistace = 2f;

    [Header("正面の壁の判定の長さ")]
    [SerializeField] private float _layLongOfCheckForwardWallOfEndWallRun = 5;

    [Header("壁のレイヤー")]
    [SerializeField] private LayerMask _wallLayer;


    [Header("壁のレイヤー")]
    [SerializeField] private Text _normal;

    [Header("正面-外積")]
    [SerializeField] private Text _syoumenHikuGaiseki;

    [Header("正面-(-外積)")]
    [SerializeField] private Text _syoumenHikuMainasuGaiseki;

    [Header("正面-外積・長さ")]
    [SerializeField] private Text _syoumenHikuGaisekiLong;

    [Header("正面-(-外積)・長さ")]
    [SerializeField] private Text _syoumenHikuMainasuGaiseki_Long;

    bool _isHit = false;

    /// <summary>壁RUNの進む方向</summary>
    private Vector3 _wallForward;

    Vector3 _housen;

    /// <summary>右の壁に当たっているかどうか</summary>
    private bool _rightWallHit = false;
    /// <summary>右の壁に当たっているかどうか</summary>
    private bool _leftWallHit = false;

    /// <summary>プレイヤーの視点の方向に壁があるかどうか</summary>
    private bool _fowardWallHit = false;


    /// <summary>WallRunの始まりの、壁の方向。true:右/false:左</summary>
    private bool _startHitRight;

    /// <summary>自身の右側の壁を検索するRaycast</summary>
    private RaycastHit _rightWall;
    /// <summary>自身の左側の壁を検索するRaycast</summary>
    private RaycastHit _leftWall;
    /// <summary>自身の正面の壁を検索するRaycast</summary>
    private RaycastHit _fowardWall;


    public Vector3 WallForward => _wallForward;



    private void Update()
    {
        WallRuning();

    }

    /// <summary>WallRun実行させる関数
    /// 1:外積を用いて、進行方向を決める
    /// 2:カメラが傾かせる
    /// 3:速度を加える関数を呼ぶ</summary>
    public void WallRuning()
    {
        //右の壁に当たり続けていたら
        if (CheckWallRight())
        {
            _isHit = true;

            //法線を取る
            _housen = _rightWall.normal;
            //外積を使い、進行方向を取る
            _wallForward = Vector3.Cross(_housen, transform.up);

           // Debug.Log("外積:" + _wallForward);

            if (_isOnKakou)
            {

                Debug.Log("プレイヤーの正面の長さ"+transform.forward.magnitude.ToString("F5"));
                Debug.Log("外積の長さ:" + _wallForward.magnitude.ToString("F5"));

                //外積ベクトルがマイナスだった場合、向きを変える。
                if ((transform.forward - _wallForward).magnitude > (transform.forward - -_wallForward).magnitude)
                {
                    Debug.Log("計算:" + (transform.forward - _wallForward).magnitude + ">" + (transform.forward - -_wallForward).magnitude);
                    _wallForward = -_wallForward;
                }


                    _syoumenHikuGaiseki.text = (transform.forward - _wallForward).ToString();
                    _syoumenHikuGaisekiLong.text = (transform.forward - _wallForward).magnitude.ToString();

                    _syoumenHikuMainasuGaiseki.text = (transform.forward - -_wallForward).ToString();
                    _syoumenHikuMainasuGaiseki_Long.text = (transform.forward - -_wallForward).magnitude.ToString();



            }
        }
        //右の壁に当たり続けていたら
        else if (CheckWallLeft())
        {
            _isHit = true;

            //法線を取る
            _housen = _leftWall.normal;
            //外積を使い、進行方向を取る
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
        //上方向
        Debug.DrawLine(transform.position, transform.position + transform.up * 4, Color.green);

        if (_isHit)
        {
            //横方向
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


    /// <summary>壁に近いかどうかを判断する</summary>
    public bool CheckWallRight()
    {
        _rightWallHit = Physics.Raycast(transform.position, transform.right, out _rightWall, m_checkWallRayDistace, _wallLayer);

        if(_rightWall.collider!=null)
        {
        Debug.Log("右の壁;" + _rightWall.collider.transform.position);
        }

        return _rightWallHit;
    }
}
