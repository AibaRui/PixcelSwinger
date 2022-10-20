using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] GameObject _weapon;
    public Transform gunTip;
    //  public Transform cam;
    public Transform player;
    public LayerMask _wall;
    [SerializeField] LineRenderer lr;

    [SerializeField] float maxGrappleDistance;
    [SerializeField] float grappleDelayTime;
    public float overshootYAxis;

    Vector3 grapplePoint;

    float grapplingCoolDown;
    float grapplingCoolDownCount;


    [SerializeField] float grapplingTimer = 2;
    bool _grappling = true;

    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    GameObject _player;

    P_Control _control;
    Swing _swing;

    PlayerMove _playerMove;
    Rigidbody _rb;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        _control = _player.GetComponent<P_Control>();
        _swing = _player.GetComponent<Swing>();
        _rb = _player.GetComponent<Rigidbody>();
        _playerMove = _player.GetComponent<PlayerMove>();
        lr = _player.gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (_weapon.activeSelf)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _playerMove._isSwing = true;
                _control._isGrapple = true;
                StartGrapple();
            }

            if (grapplingCoolDownCount > 0)
            {
                grapplingCoolDownCount -= Time.deltaTime;
            }
        }
        else
        {
            StopGrapple();
        }

    }

    private void LateUpdate()
    {
        if (_grappling)
        {

        }
    }


    void StartGrapple()
    {
        if (grapplingCoolDownCount > 0)
        {
            return;
        }
        if (_player.GetComponent<WallRun>() != null)
        {
            _player.GetComponent<WallRun>().StopWallRun();
        }
        if (_player.GetComponent<Swing>() != null)
        {
            _player.GetComponent<Swing>().StopSwing();
        }
        lr.positionCount = 2;
        _grappling = true;

        _rb.velocity = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position, Camera.main.transform.forward, out hit, maxGrappleDistance, _wall))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = _player.transform.position + _player.transform.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        //  lr.enabled = true;    
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public void ResetRestrictions()
    {
        _playerMove._isSwing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopGrapple();
        _playerMove._isSwing = false;
    }

    void ExecuteGrapple()
    {
        Vector3 lowestPoint = new Vector3(_player.transform.position.x, _player.transform.position.y - 1f, _player.transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0)
        {
            highestPointOnArc = overshootYAxis;
        }

        Jump(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    void Jump(Vector3 targetPosition, float trajectoryHeight)
    {
        Vector3 velo = SetVelo(_player.transform.position, targetPosition, trajectoryHeight);
        _rb.velocity = velo;
    }


    Vector3 SetVelo(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    public void StopGrapple()
    {
        _control._isGrapple = false;
        _grappling = false;
        _playerMove._isSwing = false;
        grapplingCoolDownCount = grapplingCoolDown;

        // lr.enabled = false;
        lr.positionCount = 0;
    }

}
