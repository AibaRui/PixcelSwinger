using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{

    [SerializeField] float _swingMoveSpeedH = 5;
    [SerializeField] float _swingMoveSpeedV = 7;
    [SerializeField] float _swinwgSpeedLimit = 15;
    public LineRenderer lr;

    //  Vector3 currentGrapplePosition;

    SpringJoint joint;
    Rigidbody _rb;

    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInput _playerInput;
    private void Awake()
    {

    }
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    //void Update()
    //{
    //    if (_waapon.activeSelf)
    //    {

    //    }
    //    else
    //    {
    //        StopSwing();
    //        _control._isWapon = false;
    //    }

    //}



    //private void LateUpdate()
    //{
    //    if (_waapon.activeSelf)
    //    {
    //        DrawRope();
    //    }
    //    else
    //    {
    //        StopSwing();
    //    }
    //}

    /// <summary>スウィング中の動き</summary>
    public void GrappleMove()
    {
        if (joint != null)
        {
            Vector3 dir = Vector3.forward * _playerInput.VerticalInput + Vector3.right * _playerInput.HorizontalInput;
            Vector3 swingPoint = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;

            dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0;

            if (dir == Vector3.zero)
            {
                // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
                //_airVelo = Vector3.zero;
                //_animKatana.SetBool("Move", false);
            }
            else
            {

                _rb.AddForce(dir * _swingMoveSpeedH);
            }


            Vector3 directionToPoint = swingPoint - transform.position;
            _rb.AddForce(directionToPoint.normalized * _swingMoveSpeedV);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
    }



    public void StartGrapple()
    {
        RaycastHit predictionHit = _playerController.PlayerSwingAndGrappleSetting.PredictionHit;

        //空ぶったら何もしない
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }


        //アンカーの着地点。
        Vector3 swingPoint = predictionHit.point;

        joint = gameObject.AddComponent<SpringJoint>();

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




        //current(意味:現在)
        // currentGrapplePosition = gunTip.position;
    }


    /// <summary>スウィング中止</summary>
    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
