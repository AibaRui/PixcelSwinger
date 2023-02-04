using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //[Header("�ړ����x")]
    //[Tooltip("�ړ����x")] [SerializeField] float _moveSpeed = 3;

    [Header("�U���̃N�[���^�C��")]
    [Tooltip("�U���̃N�[���^�C��")] [SerializeField] float _attackCoolTime = 4;




    [Header("����")]
    [Tooltip("����")] [SerializeField] Transform _weaponMazzle;

    [Header("�e")]
    [Tooltip("�e")] [SerializeField] Transform _ammo;

    [Header("�f�X���ɏo���I�u�W�F�N�g")]
    [Tooltip("�f�X���ɏo���I�u�W�F�N�g")] [SerializeField] GameObject _deathBody;

    // [Header("�ߋ����U���̉�")]
    // [Tooltip("�ߋ����U���̉�")] [SerializeField] AudioClip _attackCloseAudioClip;

    //  [Header("�������U���̉�")]
    //  [Tooltip("�������U���̉�")] [SerializeField] AudioClip _attackFarAudioClip;

    //  [Header("��_�����̉�")]
    //   [Tooltip("��_�����̉�")] [SerializeField] AudioClip _damage;


    float _countTime = 0;


    AudioSource _aud;

    /// <summary>true��������v�l</summary>
    bool _thinkNow = false;

    /// <summary>����̍s�������Ă���Ƃ��͍s�����Ȃ��B�U�����͑����Ă΂�邩��</summary>
    bool _isActionNow = false;

    /// <summary>�^�[�Q�b�g�U���������Ă邩�ǂ���</summary>
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


        //���݂̏󋵂ɉ������s��
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

    /// <summary>�s������</summary>
    void SetAi()
    {
        //�^�C�}�[���񂵂Ă�Ԃ͎v�l�����Ȃ�
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

    /// <summary>�ߋ����U���̍s�� </summary>
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

