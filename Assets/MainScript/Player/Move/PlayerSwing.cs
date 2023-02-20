using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    [Header("Swing中の移動の速さ")]
    [SerializeField] float _swingMoveSpeedH = 5;

    [Header("アンカーの刺さった位置に引き寄せる速さ")]
    [SerializeField] float _swingBurst = 7;

    public LineRenderer lr;

    //  Vector3 currentGrapplePosition;

    SpringJoint joint;

    Rigidbody _rb;


    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInput _playerInput;

    [SerializeField] Animator _legAnim;

    private void Awake()
    {

    }
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>スウィング中の動き</summary>
    public void SwingingMove()
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


            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);


            if(Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 dirs = Camera.main.transform.forward;
                _rb.AddForce(dirs.normalized * 10);
                Debug.Log("加速");
            }

            if (Input.GetKey(KeyCode.Space))
            {
                joint.maxDistance = distanceFromPoint * 0.5f;
                joint.minDistance = distanceFromPoint * 0.1f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst * 2);
            }
            else
            {
                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance = distanceFromPoint * 0.25f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst);
            }

        }
    }

    public void StartSwing()
    {
        RaycastHit predictionHit = _playerController.PlayerSwingAndGrappleSetting.PredictionHit;
        //swingJointPoint = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;
        //空ぶったら何もしない
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        _playerController.PlayerSwingAndGrappleSetting.Joint = joint;

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



        lr.positionCount = 2;

        //_tipPos = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;
        //current(意味:現在)
        //currentGrapplePosition = gunTip.position;
    }


    /// <summary>スウィング中止</summary>
    public void StopSwing()
    {
        lr.positionCount = 0;
        _playerController.PlayerSwingAndGrappleSetting.Joint = null;

        _legAnim.SetBool("Swing", false);
        Destroy(joint);
    }

    public void LegAnimation()
    {
        _legAnim.SetFloat("Speed", _rb.velocity.y);

        _legAnim.SetBool("Swing", true);

        if (transform.position.x > _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point.x)
        {
            _legAnim.SetBool("IsLeft", true);
            _legAnim.SetBool("IsRight", false);
        }
        else
        {
            _legAnim.SetBool("IsRight", true);
            _legAnim.SetBool("IsLeft", false);
        }

    }


}
