using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] GameObject _waapon;


    [SerializeField] float _swingMoveSpeedH = 5;
    [SerializeField] float _swingMoveSpeedV = 7;
    [SerializeField] float _swinwgSpeedLimit = 15;
    public LineRenderer lr;


    [Header("アンカーを発射する場所")]
    [Tooltip("アンカーを発射する場所")] [SerializeField] Transform gunTip;

    [Header("アンカーをさせる壁のレイヤー")]
    [Tooltip("アンカーをさせる壁のレイヤー")] [SerializeField] LayerMask _wallLayer;

    public float predictionSphereCastRadius;


    [Header("アンカー着地点のマーカー")]
    [Tooltip("アンカー着地点のマーカー")] [SerializeField] Transform predictionPoint;

    [Header("アンカーを刺せる最大の長さ")]
    [Tooltip("アンカーを刺せる最大の長さ")] [SerializeField] float _maxSwingDistance = 25f;


    /// <summary>アンカーの着地点マーカーの座標 </summary>
    RaycastHit predictionHit;

    /// <summary>アンカーの刺さった位置</summary>
    Vector3 swingPoint;

    GameObject _player;



    //  Vector3 currentGrapplePosition;
    PlayerMove _playerMove;
    P_Control _control;

    SpringJoint joint;
    Rigidbody m_rb;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        _control = _player.GetComponent<P_Control>();
        lr = _player.gameObject.GetComponent<LineRenderer>();
        _playerMove = _player.gameObject.GetComponent<PlayerMove>();
        m_rb = _player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_waapon.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _control._isSwing = true;
                StartSwing();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _control._isSwing = false;
                StopSwing();
            }

            CheckForSwingPoints();

            if (joint != null)
            {
                SwingingMove();
            }
            _control._isWapon = true;
        }
        else
        {
            StopSwing();
            _control._isWapon = false;
        }

    }

    private void LateUpdate()
    {
        if (_waapon.activeSelf)
        {
            DrawRope();
        }
        else
        {
            StopSwing();
        }
    }


    /// <summary>アンカーの刺す位置を探す関数</summary>
    void CheckForSwingPoints()
    {
        if (joint != null)
        {
            return;
        }

        RaycastHit spherCastHit;
        Physics.SphereCast(_player.transform.position, predictionSphereCastRadius, Camera.main.transform.forward, out spherCastHit, _maxSwingDistance, _wallLayer);

        RaycastHit raycastHit;
        Physics.Raycast(_player.transform.position, Camera.main.transform.forward, out raycastHit, _maxSwingDistance, _wallLayer);


        Vector3 realHitPoint;

        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }

        else if (spherCastHit.point != Vector3.zero)
        {
            realHitPoint = spherCastHit.point;
        }

        else
        {
            realHitPoint = Vector3.zero;
        }

        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }

        else
        {
            predictionPoint.gameObject.SetActive(false);
        }


        predictionHit = raycastHit.point == Vector3.zero ? spherCastHit : raycastHit;
    }






    /// <summary>スウィング中の動き</summary>
    void SwingingMove()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");


        if (h > 0) m_rb.AddForce(transform.right * _swingMoveSpeedH);

        if (h < 0) m_rb.AddForce(-transform.right * _swingMoveSpeedH);

        if (v > 0) m_rb.AddForce(transform.forward * _swingMoveSpeedV);

        //Spaceを押してる間アンカーが縮んで引っ張られる
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            m_rb.AddForce(directionToPoint.normalized * _swingMoveSpeedV);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }

        //下に押してる間アンカーが伸びて下に下がれる
        if (v < 0)
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + 5;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }

    /// <summary>線を描く</summary>
    void DrawRope()
    {
        if (!joint)
        {
            return;
        }

        //   currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        //線を引く位置を決める
        //0は線を引く開始点
        //１は線を引く終了点
        lr.positionCount = 2;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }

    void StartSwing()
    {
        //空ぶったら何もしない
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        //スウィングをしたらグラップルはしない
        if (GetComponent<Grapple>() != null)
        {
            GetComponent<Grapple>().StopGrapple();
        }
        if (_player.GetComponent<WallRun>() != null)
        {
            _player.GetComponent<WallRun>().StopWallRun();
        }

        //アンカーの着地点を赤点の位置にする
        swingPoint = predictionHit.point;
        joint = _player.gameObject.AddComponent<SpringJoint>();

        //Anchor(jointをつけているオブジェクトのローカル座標  ////例)自分についてる命綱の位置)
        //connectedAnchor(アンカーのついてる点のワールド座標　////例)アンカーの先。バンジージャンプの橋の、支えているところ)

        //autoConfigureConnectedAnchorはジョイントをつけた時はOnになっており、AnchorとconnectedAnchor(アンカーの接地点)が同じ
        //つまり自分の居る位置にアンカーを刺してそこでぶら下がっている状態。なのでオフにする

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;



        float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);


        //ジョイントの長さを変更(*1だとアンカーを指しても長さが縮まらないため、すぐに浮かない)
        //強制的に短くする事で引っ張られる事になる
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        //ばねの強さ
        joint.spring = 4.5f;

        //(springの減る量)バネがビヨーンと伸びるのを繰り返してから動かなくなるまでの時間。値が多いほど短くなる
        joint.damper = 7f;

        joint.massScale = 4.5f;



        lr.positionCount = 2;


        //current(意味:現在)
        // currentGrapplePosition = gunTip.position;
    }


    /// <summary>スウィング中止</summary>
    public void StopSwing()
    {
        _control._isSwing = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
}
