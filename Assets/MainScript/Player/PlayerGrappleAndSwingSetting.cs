using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrappleAndSwingSetting : MonoBehaviour
{
    [Header("アンカーを発射する場所")]
    [Tooltip("アンカーを発射する場所")] [SerializeField] private Transform _gunTip;

    [Header("アンカーをさせる壁のレイヤー")]
    [Tooltip("アンカーをさせる壁のレイヤー")] [SerializeField] private LayerMask _wallLayer;

    [Header("アンカー着地点のマーカー")]
    [Tooltip("アンカー着地点のマーカー")] [SerializeField] private Transform _predictionPoint;

    [Header("ワイヤーの長さ")]
    [Tooltip("ワイヤーの長さ")] [SerializeField] private List<float> _wireLongs = new List<float>();

    [Header("球形のcastの半径")]
    [Tooltip("球形のcastの半径")] private float predictionSphereCastRadius;


    [Header("HitPointの最初のAnimationがおわる秒数")]
    [SerializeField] private float _endHitPoitAnimSecond = 0.5f;

    [Header("アンカーの着地点を示すUI")]
    [SerializeField] private GameObject _hitPointer;

    [Header("アンカーの着地点を示すUI")]
    [SerializeField] private RectTransform _hitPointerUI;


    [Header("Swingの選択バー")]
    [SerializeField] private GameObject _swingSelectBar;

    [Header("Grappleの選択バー")]
    [SerializeField] private GameObject _grappleSelectBar;

    [Header("ワイヤーの長さの選択バー")]
    [SerializeField] private List<GameObject> _wireLongsSelsectBar = new List<GameObject>();

    [Header("HitPointの初期の設定")]
    [SerializeField] private SwingHitUI _firstSwingHitUISetting;

    [Header("SwingとGrappleを切り替えた時の音")]
    [SerializeField] private AudioClip _changeAudio;

    [Header("SwingとGrappleの終わりの音")]
    [SerializeField] private AudioClip _swingEndClip;

    [Header("SwingとGrappleのワイヤーを引く音")]
    [SerializeField] private AudioClip _wireSound;

    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private LineRenderer _lr;

    /// <summary>現在のワイヤーの長さ</summary>
    private float _wireLong = 25;

    /// <summary>ワイヤーの長さを居れたListの要素を示す</summary>
    private int _wireLongsNum = 0;

    /// <summary>アンカーの刺さった位置</summary>
    private Vector3 _swingAndGrapplePoint;

    private float _countTime = 0;

    /// <summary>HitしたRayの情報</summary>
    RaycastHit _predictionHit;

    private bool _isEndHitPoinAnim = false;

    private SpringJoint _joint;

    private SwingHitUI _swingHitUISetting;


    private bool _isLongWire;

    public bool IsLongWire { get => _isLongWire; set => _isLongWire = value; }


    private WireLong _wireLongEnum;

    public WireLong WireLongEnum => _wireLongEnum;

    //Enum
    [SerializeField]
    private SwingOrGrapple _swingOrGrappleEnum = SwingOrGrapple.Swing;

    //Enumのプロパティ
    public SwingOrGrapple SwingOrGrappleEnum { get => _swingOrGrappleEnum; }

    public Transform GunTip { get => _gunTip; }

    public Transform PredictionPoint { get => _predictionPoint; }

    public RaycastHit PredictionHit { get => _predictionHit; }

    public Vector3 SwingAndGrapplePoint { get => _swingAndGrapplePoint; }

    public SpringJoint Joint { set => _joint = value; }

    public SwingHitUI SwingHitUISetting { get => _swingHitUISetting; set => _swingHitUISetting = value; }

    public SwingHitUI FirstSwingHitUISetting => _firstSwingHitUISetting;

    /// <summary>アンカー設置時のUIの表示の仕方</summary>
    public enum SwingHitUI
    {
        //UIを表示しない
        NoUI,
        //最初だけ表示
        First,
        //ずっと表示
        All
    }

    /// <summary>SwingかGrappleのどちらの状態かを示す</summary>
    public enum SwingOrGrapple
    {
        Swing,
        Grapple,
    }

    public enum WireLong
    {
        Short,
        Midiam,
        Long,
    }

    private void Start()
    {
        if (_swingOrGrappleEnum == SwingOrGrapple.Swing)
        {
            _swingSelectBar.SetActive(true);
            _grappleSelectBar.SetActive(false);
        }
        else
        {
            _swingSelectBar.SetActive(false);
            _grappleSelectBar.SetActive(true);
        }

        //ワイヤーの長さを設定
        _wireLong = _wireLongs[0];
        _wireLongsSelsectBar[0].SetActive(true);
    }

    /// <summary>SwingとGrappleの状態を入れ替える関数</summary>
    public void ChangeTypeSwingOrGrapple()
    {
        //マウス右クリックを押したら変更
        //Swing<=>Grapple　交互に変更する
        if (_playerInput.IsRightMouseClickDown)
        {
            if (_swingOrGrappleEnum == SwingOrGrapple.Swing)
            {
                _swingOrGrappleEnum = SwingOrGrapple.Grapple;
                _swingSelectBar.SetActive(false);
                _grappleSelectBar.SetActive(true);
            }
            else
            {
                _swingOrGrappleEnum = SwingOrGrapple.Swing;
                _swingSelectBar.SetActive(true);
                _grappleSelectBar.SetActive(false);
            }

            //変更時の音を流す
            _playerController.AudioManager.PlayeGameUISE(_changeAudio);
        }

        if (_isLongWire)
        {
            //上に
            if (_playerInput.IsMouseScrol > 0)
            {
                //変更時の音を流す
                _playerController.AudioManager.PlayeGameUISE(_changeAudio);

                _wireLongsNum++;
                if (_wireLongsNum == _wireLongs.Count)
                {
                    _wireLongsNum = _wireLongs.Count - 1;
                }
                _wireLong = _wireLongs[_wireLongsNum];


                SetEnumWireLong(_wireLongsNum);

                _wireLongsSelsectBar.ForEach(i => i.SetActive(false));
                _wireLongsSelsectBar[_wireLongsNum].SetActive(true);
            }
            //下に
            else if (_playerInput.IsMouseScrol < 0)
            {
                //変更時の音を流す
                _playerController.AudioManager.PlayeGameUISE(_changeAudio);

                _wireLongsNum--;
                if (_wireLongsNum < 0)
                {
                    _wireLongsNum = 0;
                }
                _wireLong = _wireLongs[_wireLongsNum];
                SetEnumWireLong(_wireLongsNum);

                _wireLongsSelsectBar.ForEach(i => i.SetActive(false));
                _wireLongsSelsectBar[_wireLongsNum].SetActive(true);
            }
        }
    }

    public void SetEnumWireLong(int num)
    {
        if (_wireLongsNum == 0)
        {
            _wireLongEnum = WireLong.Short;
        }
        else if (_wireLongsNum == 1)
        {
            _wireLongEnum = WireLong.Midiam;
        }
        else
        {
            _wireLongEnum = WireLong.Long;
        }
    }

    /// <summary>アンカーの刺す位置を探す関数</summary>
    public void CheckForSwingPoints()
    {
        //Joinがある=　現在Swing中ならアンカー着地点が決まっているので探さない
        if (_joint != null)
        {
            return;
        }

        //アンカー着地点を示す球体を、Rayの当たっているポイントに移動させる
        _predictionPoint.position = _predictionHit.point;

        //円形のCast
        RaycastHit spherCastHit;
        RaycastHit raycastHit;

        //アンカー着地点をRayを飛ばして探す。
        //Rayの長さは、現在設定しているワイヤーの長さ
        float maxDistance = _wireLong;

        //球形のCast
        Physics.SphereCast(transform.position, predictionSphereCastRadius, Camera.main.transform.forward, out spherCastHit, maxDistance, _wallLayer);
        //直線状のCast
        Physics.Raycast(transform.position, Camera.main.transform.forward, out raycastHit, maxDistance, _wallLayer);

        //Rayの当たった場所
        Vector3 realHitPoint;

        //真っ直ぐさせる場合(RayCastが当たる場合)
        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        //直線じゃないどこかの場合(RayCastが当たらないが、SphereCastが当たった場合)
        else if (spherCastHit.point != Vector3.zero)
        {
            realHitPoint = spherCastHit.point;
        }
        //どちらも当たらない場合
        else
        {
            realHitPoint = Vector3.zero;
        }

        //アンカー設置点のマーカーの場所を設定
        if (realHitPoint != Vector3.zero)
        {
            _predictionPoint.gameObject.SetActive(true);
            _predictionPoint.position = realHitPoint;
        }
        else
        {
            _predictionPoint.gameObject.SetActive(false);
        }

        //rayが当たらなかったらSpherCastの情報をいれる
        _predictionHit = raycastHit.point == Vector3.zero ? spherCastHit : raycastHit;
    }

    /// <summary>Swing中に[Hit!]のUIを表示する仕組み</summary>
    public void ActivePointer()
    {
        if (!_joint)
        {
            return;
        }

        if (_swingHitUISetting == SwingHitUI.NoUI)
        {
            return;
        }
        else if (_swingHitUISetting == SwingHitUI.First)
        {


            if (_countTime <= _endHitPoitAnimSecond)
            {
                PointerView();
                _countTime += Time.deltaTime;
            }
            else
            {
                if (!_isEndHitPoinAnim)
                {
                    _hitPointer.SetActive(false);
                    _isEndHitPoinAnim = true;
                }
            }

        }
        else
        {
            PointerView();
        }
    }

    private void PointerView()
    {
        //マーカーの位置をスクリーン画面に変換して表示する
        var targetWorldPos = PredictionPoint.position;
        var targetScreenPos = Camera.main.WorldToScreenPoint(targetWorldPos);

        _hitPointer.transform.position = targetScreenPos;

        var cameraDir = Camera.main.transform.forward;
        var targetDir = targetWorldPos - Camera.main.transform.position;

        var isFront = Vector3.Dot(targetDir, cameraDir) > 0;
        _hitPointer.SetActive(isFront);
    }


    /// <summary>Siwng終わりに呼ぶ</summary>
    public void EndGrappleOrSwing()
    {
        //[Hit!]のUIを非表示にする
        _isEndHitPoinAnim = false;
        _countTime = 0;
        _hitPointer.SetActive(false);

        //JointとLineRendrerを消す
        _joint = null;
        _lr.positionCount = 0;
    }

    /// <summary>Swing時のワイヤーを描く</summary>
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
        _lr.positionCount = 2;
        _lr.SetPosition(0, _gunTip.position);
        _lr.SetPosition(1, _predictionHit.point);
    }

    public void SwingAndGrappleEndSound()
    {
        _playerController.AudioManager.PlayerSE(_swingEndClip, false);
    }

    public void WireSound()
    {
        _playerController.AudioManager.PlayerSE(_wireSound,true);
    }

}
