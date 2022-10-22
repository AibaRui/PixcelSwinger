using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbWall : MonoBehaviour
{
    [SerializeField] LayerMask _layer;

    [SerializeField] float _boxCastX = 1;
    [SerializeField] float _boxCastY = 1;
    [SerializeField] float _boxCastZ = 1;

    [SerializeField] Vector3 posAdd;
    /// <summary>前方に壁があるかどうか</summary>
    bool _fowardWall;
    /// <summary>登る壁の上に壁があるかどうか</summary>
    bool _UpWall;
    RaycastHit hitFowardWal;

   public bool _isClimb;

    P_Control _control;
    Rigidbody _rb;


    void Start()
    {
        _control = GetComponent<P_Control>();
        _rb = GetComponent<Rigidbody>();
    }



    private void Update()
    {
        CheckFowardWall();
        Climb();


        //if (Physics.BoxCast(transform.position, new Vector3(_boxCastX, _boxCastY, _boxCastZ), transform.forward, out hitFowardWal, Quaternion.identity, 1.0f, LayerMask.GetMask("Default")))
        //{
        //    Debug.Log(hitFowardWal.collider.name);
        //}

    }

    void Climb()
    {
        float v = Input.GetAxisRaw("Vertical");

        if (!_UpWall && _fowardWall && !_isClimb)
        {
            if(v>0)
            {
                _rb.velocity = Vector3.zero;
                _isClimb = true;
            }
        }

        if(_isClimb)
        {
            Vector3 velo = new Vector3(0, 20, 0);
            _rb.velocity = velo;

            if(!_fowardWall)
            {
                _rb.velocity = Vector3.zero;
                _rb.AddForce(transform.forward * 3, ForceMode.Impulse);
                _isClimb = false;
            }
        }


    }

    public void CheckFowardWall()
    {
        Vector3 start = this.transform.position + transform.up * 2;
        Vector3 end = start + transform.forward;

        _UpWall = Physics.Linecast(start, end,_layer); // 引いたラインに何かがぶつかっていたら true とする
        _fowardWall = Physics.BoxCast(transform.position + transform.forward + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ), transform.forward, out hitFowardWal, Quaternion.identity, 1.0f,_layer);

        Debug.DrawLine(start, end, Color.green); // 動作確認用に Scene ウィンドウ上で線を表示する
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward+posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ));
    }
}
