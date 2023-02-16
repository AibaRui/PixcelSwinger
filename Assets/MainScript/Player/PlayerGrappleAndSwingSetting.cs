using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrappleAndSwingSetting : MonoBehaviour
{
    [Header("アンカーを発射する場所")]
    [Tooltip("アンカーを発射する場所")] [SerializeField] private Transform _gunTip;

    [Header("アンカーをさせる壁のレイヤー")]
    [Tooltip("アンカーをさせる壁のレイヤー")] [SerializeField] LayerMask _wallLayer;

    [Header("アンカー着地点のマーカー")]
    [Tooltip("アンカー着地点のマーカー")] [SerializeField] Transform _predictionPoint;

    [Header("ワイヤーの長さ")]
    [Tooltip("ワイヤーの長さ")] [SerializeField] private List<float> _wireLongs = new List<float>();

    private float _wireLong = 25;

    private int _wireLongsNum = 0;

    [SerializeField] Text _nowWireLongText;

    [SerializeField] Text _nowSetText;
    //Enum
    private SwingOrGrapple _swingOrGrappleEnum = SwingOrGrapple.Swing;

    //Enumのプロパティ
    public SwingOrGrapple SwingOrGrappleEnum { get => _swingOrGrappleEnum; }


    public float predictionSphereCastRadius;
    public Transform GunTip { get => _gunTip; }

    public Transform PredictionPoint { get => _predictionPoint; }


    /// <summary>アンカーの着地点マーカーの座標 </summary>
    RaycastHit _predictionHit;

    public RaycastHit PredictionHit { get => _predictionHit; }

    /// <summary>アンカーの刺さった位置</summary>
    private Vector3 _swingAndGrapplePoint;

    public Vector3 SwingAndGrapplePoint { get => _swingAndGrapplePoint; }

    private SpringJoint _joint;

    public SpringJoint Joint { set => _joint = value; }

    //  Vector3 currentGrapplePosition;


    [SerializeField] PlayerInput _playerInput;


    public LineRenderer lr;
    public enum SwingOrGrapple
    {
        Swing,
        Grapple,
    }

    private void Start()
    {
        _nowSetText.text = _swingOrGrappleEnum.ToString();
        lr = GetComponent<LineRenderer>();

        _wireLong = _wireLongs[0];
    }

    public void ChangeTypeSwingOrGrapple()
    {
        if (_playerInput.IsRightMouseClickDown)
        {
            if (_swingOrGrappleEnum == SwingOrGrapple.Swing)
            {
                _swingOrGrappleEnum = SwingOrGrapple.Grapple;
                _nowSetText.text = "Grapple";
            }
            else
            {
                _swingOrGrappleEnum = SwingOrGrapple.Swing;
                _nowSetText.text = "Swing";
            }
        }

        //上に
        if (_playerInput.IsMouseScrol > 0)
        {
            _wireLongsNum++;
            if(_wireLongsNum==_wireLongs.Count)
            {
                _wireLongsNum = _wireLongs.Count-1;
            }
            _wireLong = _wireLongs[_wireLongsNum];
            _nowWireLongText.text = _wireLong.ToString("00");
        }
        //下に
        else if (_playerInput.IsMouseScrol < 0)
        {
            _wireLongsNum--;
            if (_wireLongsNum <0)
            {
                _wireLongsNum = 0;
            }
            _wireLong = _wireLongs[_wireLongsNum];
            _nowWireLongText.text = _wireLong.ToString("00");
        }



    }

    /// <summary>アンカーの刺す位置を探す関数</summary>
    public void CheckForSwingPoints()
    {
        if (_joint != null)
        {
            return;
        }

        _predictionPoint.position = _predictionHit.point;

        //円形のCast
        RaycastHit spherCastHit;
        RaycastHit raycastHit;

        float maxDistance = 0;

        if (_swingOrGrappleEnum == SwingOrGrapple.Swing)
        {
            maxDistance = _wireLong;
        }
        else
        {
            maxDistance = _wireLong;
        }
        Physics.SphereCast(transform.position, predictionSphereCastRadius, Camera.main.transform.forward, out spherCastHit, maxDistance, _wallLayer);


        Physics.Raycast(transform.position, Camera.main.transform.forward, out raycastHit, maxDistance, _wallLayer);


        Vector3 realHitPoint;

        //真っ直ぐさせる場合
        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        //直線じゃないどこかの場合
        else if (spherCastHit.point != Vector3.zero)
        {
            realHitPoint = spherCastHit.point;
        }

        else
        {
            realHitPoint = Vector3.zero;
        }

        //マーカーの場所の設定
        if (realHitPoint != Vector3.zero)
        {
            _predictionPoint.gameObject.SetActive(true);
            _predictionPoint.position = realHitPoint;
        }
        else
        {
            _predictionPoint.gameObject.SetActive(false);
        }


        _predictionHit = raycastHit.point == Vector3.zero ? spherCastHit : raycastHit;
    }


    /// <summary>線を描く</summary>
    public void DrawRope()
    {
        if (!_joint)
        {
            return;
        }

        //   currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        //線を引く位置を決める
        //0は線を引く開始点
        //１は線を引く終了点
        lr.positionCount = 2;
        lr.SetPosition(0, _gunTip.position);
        lr.SetPosition(1, _predictionHit.point);
    }


}
