using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAvirity : MonoBehaviour
{
    [SerializeField] GameObject _katana;


    [SerializeField] float _timeSpeed;
    [SerializeField] float _cooltimeCount = 5f;
    [SerializeField] float _cooltimeLimit = 5f;

    Vector3 _endPos;
    Vector3 _slashStartPos;
    Vector3 _slashEndPos;

    [SerializeField] Text _t;

    Vector3 _saveVelo;

    Vector3 _angularVelocity;
    Vector3 _velocity;

    bool _isSlash;
    bool _isSlow;


    bool _isCount;

    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cooltimeCount = _cooltimeLimit;
        _t.text = _cooltimeCount.ToString();
    }

    void Update()
    {
        CountTimeTimeAvirity();
        Slow();


    }

    void Slow()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSlow();
            _isCount = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSlow();
            _isCount = false;
        }
    }



    void CountTimeTimeAvirity()
    {
        if (_isCount)
        {
            //時間を遅くしている間、能力のタイム減らす
            if (_cooltimeCount >= 0)
            {
                _cooltimeCount -= Time.deltaTime;
            }
            else if(_cooltimeCount<=0)
            {
                StopSlow();
            }
        }
        else
        {
            //能力のタイムを回復  
            if (_cooltimeCount <= _cooltimeLimit)
            {
                _cooltimeCount += Time.deltaTime * 0.5f;
            }
        }

        _t.text = _cooltimeCount.ToString();
    }

    void StartSlow()
    {
        //能力のタイムが0だったら使えない
        if (_cooltimeCount <= 0)
        {
            return;
        }
        Time.timeScale = 0.2f;
        Debug.Log("s");
    }

    void StopSlow()
    {
        _isCount = false;

        Time.timeScale = 1f;
    }

}
