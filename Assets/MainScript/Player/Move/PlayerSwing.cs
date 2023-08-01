using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>プレイヤーのスウィングの動きの実装</summary>
public class PlayerSwing : MonoBehaviour
{
    [Header("Swing中の移動の速さ")]
    [SerializeField] float _swingMoveSpeedH = 5;

    [Header("アンカーの刺さった位置に引き寄せる速さ")]
    [SerializeField] float _swingBurst = 7;

    [Header("バネの強さ")]
    [SerializeField] private float _springPower = 4.5f;

    [Header("ダンパ-の強さ")]
    [SerializeField] private float _damperPower = 7;

    [Header("ダンパ-の強さ")]
    [SerializeField] private float _massScale = 4.5f;

    [SerializeField] PlayerController _playerController;

    [SerializeField] PlayerInput _playerInput;

    /// <summary>プレイヤーのSpringJoint</summary>
    private SpringJoint _joint;

    /// <summary>プレイヤーのRigidBody</summary>
    private Rigidbody _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>スウィング中の動き</summary>
    public void SwingingMove()
    {
        if (_joint != null)
        {
            //移動方向を決める
            Vector3 dir = Vector3.forward * _playerInput.VerticalInput + Vector3.right * _playerInput.HorizontalInput;

            //Swingのアンカーの着地点
            Vector3 swingPoint = _playerController.PlayerSwingAndGrappleSetting.PredictionHit.point;

            // メインカメラを基準に入力方向のベクトルを変換する
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            if (dir == Vector3.zero)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            }   // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            else
            {
                _rb.AddForce(dir * _swingMoveSpeedH);
            }   //入力が合ったら速度を加える

            //アンカー着地点の方向
            Vector3 directionToPoint = swingPoint - transform.position;

            //アンカーとの距離
            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            if (Input.GetKey(KeyCode.Space))
            {
                _joint.maxDistance = distanceFromPoint * 0.5f;
                _joint.minDistance = distanceFromPoint * 0.1f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst * 2);
            }   //Spaceキーを押している間は加速する
            else
            {
                _joint.maxDistance = distanceFromPoint * 0.8f;
                _joint.minDistance = distanceFromPoint * 0.25f;
                _rb.AddForce(directionToPoint.normalized * _swingBurst);
            }   //通常の速さで加速

        }
    }

    /// <summary>スウィングを始めた時の処理</summary>
    public void StartSwing()
    {

        RaycastHit predictionHit = _playerController.PlayerSwingAndGrappleSetting.PredictionHit;

        //空ぶったら何もしない
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        //swing/grappleの設定クラスに自身のjointを渡す
        _joint = gameObject.AddComponent<SpringJoint>();

        //jointの情報を渡す
        _playerController.PlayerSwingAndGrappleSetting.Joint = _joint;

        //アンカーの着地点。
        Vector3 swingPoint = predictionHit.point;

        //Anchor(jointをつけているオブジェクトのローカル座標  ////例)自分についてる命綱の位置)
        //connectedAnchor(アンカーのついてる点のワールド座標　////例)アンカーの先。バンジージャンプの橋の、支えているところ)

        //autoConfigureConnectedAnchorはジョイントをつけた時はOnになっており、AnchorとconnectedAnchor(アンカーの接地点)が同じ
        //つまり自分の居る位置にアンカーを刺してそこでぶら下がっている状態。なのでオフにする

        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = swingPoint;

        //自分とアンカーの位置の間(ジョイント)の長さ。
        float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

        //ジョイントの長さを変更(*1だとアンカーを指しても長さが縮まらないため、すぐに浮かない)
        //強制的に短くする事で引っ張られる事になる
        _joint.maxDistance = distanceFromPoint * 0.8f;
        _joint.minDistance = distanceFromPoint * 0.25f;

        //ばねの強さ(のび縮みのしやすさ)
        _joint.spring = _springPower;

        //(springの減る量)バネがビヨーンと伸びるのを繰り返してから動かなくなるまでの時間。値が多いほど短くなる
        _joint.damper = _damperPower;

        //質量
        _joint.massScale = _massScale;
    }


    /// <summary>スウィング中止</summary>
    public void StopSwing()
    {
        _playerController.PlayerSwingAndGrappleSetting.Joint = null;
    
        //Jointを消す
        Destroy(_joint);
    }
}
