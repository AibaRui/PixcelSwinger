using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //[Header("移動速度")]
    //[Tooltip("移動速度")] [SerializeField] float _moveSpeed = 3;

    [Header("攻撃のクールタイム")]
    [Tooltip("攻撃のクールタイム")] [SerializeField] float _attackCoolTime = 4;




    [Header("武器")]
    [Tooltip("武器")] [SerializeField] Transform _weaponMazzle;

    [Header("弾")]
    [Tooltip("弾")] [SerializeField] Transform _ammo;

    [Header("デス時に出すオブジェクト")]
    [Tooltip("デス時に出すオブジェクト")] [SerializeField] GameObject _deathBody;

    // [Header("近距離攻撃の音")]
    // [Tooltip("近距離攻撃の音")] [SerializeField] AudioClip _attackCloseAudioClip;

    //  [Header("遠距離攻撃の音")]
    //  [Tooltip("遠距離攻撃の音")] [SerializeField] AudioClip _attackFarAudioClip;

    //  [Header("被ダメ時の音")]
    //   [Tooltip("被ダメ時の音")] [SerializeField] AudioClip _damage;


    float _countTime = 0;


    AudioSource _aud;

    /// <summary>trueだったら思考</summary>
    bool _thinkNow = false;

    /// <summary>特定の行動をしているときは行動しない。攻撃等は多数呼ばれるから</summary>
    bool _isActionNow = false;

    /// <summary>ターゲット攻撃を喰らってるかどうか</summary>
    public bool _isDamagedTargetAttack = false;

    int _countDamagedTargetAttack = 0;


    Vector3 _angularVelocity;
    Vector3 _velocity;

    public bool _isJustKaihi = false;



    Rigidbody _rb;
    Animator _anim;
    GameObject _player;
    public EnemyAction _enemyAction = EnemyAction.Wait;
    Animator _weaponAnim;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        _thinkNow = true;
        _rb = gameObject.GetComponent<Rigidbody>();
        // _anim = gameObject.GetComponent<Animator>();
        //  _weaponAnim = _weapon.GetComponent<Animator>();
        // _aud = gameObject.GetComponent<AudioSource>();
    }






    void Update()
    {
        SetAi();


        //現在の状況に応じた行動
        switch (_enemyAction)
        {
            case EnemyAction.Wait:
                Wait();
                break;
            //case EnemyAction.Move:
            //    Move();
            //    break;

            //case EnemyAction.Follow:
            //    Follow();
            //    break;

            case EnemyAction.Attack:
                Attack();
                break;

        }
    }

    /// <summary>行動決定</summary>
    void SetAi()
    {
        //タイマーを回してる間は思考させない
        if (!_thinkNow || _isActionNow)
        {
            return;
        }
        MainRoutine();

        StartCoroutine(AiTimer());
    }

    void MainRoutine()
    {

        float dir = Vector3.Distance(_player.transform.position, transform.position);

        if (dir < 20)
        {
            _enemyAction = EnemyAction.Attack;

            return;
        }

        if (dir < 10)
        {
            _enemyAction = EnemyAction.Follow;
            return;
        }
        if (dir < 15)
        {
            _enemyAction = EnemyAction.Move;
            return;
        }


        if (dir < 20)
        {
            _enemyAction = EnemyAction.Wait;
            return;
        }

    }

    IEnumerator AiTimer()
    {
        _thinkNow = false;
        yield return new WaitForSeconds(0.1f);
        _thinkNow = true;
    }

    void TimeCount(float limit)
    {
        _countTime += Time.deltaTime;


    }

    void Wait()
    {
        //  Debug.Log("wait");
    }

    void Damaged()
    {
        Debug.Log("DAMAGE");
        _isActionNow = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var g = Instantiate(_deathBody);
        g.transform.position = transform.position;
        Destroy(gameObject);
    }

    /// <summary>近距離攻撃の行動 </summary>
    void Attack()
    {
        Dirction();
        if (_isActionNow)
        {
            return;
        }
        Debug.Log("Attack");

        _isActionNow = true;
        StartCoroutine(AttackC());
    }

    IEnumerator AttackC()
    {



        var go = Instantiate(_ammo);
        go.transform.position = _weaponMazzle.position;


        // _aud.PlayOneShot(_attackCloseAudioClip);
        yield return new WaitForSeconds(0.5f);

        if (_enemyAction == EnemyAction.Damaged)
        {
            yield break;
        }
        //_weaponAnim.Play("ArrmerEnemyWeaponAttackClose");
        yield return new WaitForSeconds(1.1f + _attackCoolTime);
        _isActionNow = false;
    }

    //void LongAttck()
    //{
    //    if (_isActionNow)
    //    {
    //        return;
    //    }
    //    _isActionNow = true;
    //    Dirction();
    //    StartCoroutine(LongAttackC());

    //}

    //IEnumerator LongAttackC()
    //{
    //    _weaponAnim.Play("ArmerEnemyWeaponFar1");
    //    _aud.PlayOneShot(_attackFarAudioClip);

    //    yield return new WaitForSeconds(0.5f);

    //    if (_enemyAction == EnemyAction.Damaged)
    //    {
    //        yield break;
    //    }
    //    var player = GameObject.FindGameObjectWithTag("Player");
    //    if (player)
    //    {
    //        var go = Instantiate(_longAttackEffect);
    //        go.transform.position = player.transform.position + new Vector3(0, 1, -1.5f);
    //    }

    //    yield return new WaitForSeconds(1.1f);
    //    if (_enemyAction == EnemyAction.Damaged)
    //    {
    //        yield break;
    //    }
    //    yield return new WaitForSeconds(_attackCoolTime);
    //    _isActionNow = false;
    //}


    void Dirction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 v = player.transform.position - transform.position;
        v.y = 0;
        transform.forward = v;
    }

    public enum EnemyAction
    {
        Next,
        Wait,
        Move,
        Follow,
        Attack,
        Evation,
        Damaged,
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KatanaAttack")
        {
            _enemyAction = EnemyAction.Damaged;
            Damaged();
        }
    }
}

