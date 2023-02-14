using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimingWall : MonoBehaviour
{

    [SerializeField] WallCheck _wallCheck;

    private bool _isClimbing = true;

    public bool IsClimb { get => _isClimbing; set => _isClimbing = value; }

    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

   public void Climb()
    {
        if (_wallCheck.DownWall)
        {
            Vector3 velo = new Vector3(0, 20, 0);
            _rb.velocity = velo;
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(transform.up * 3, ForceMode.Impulse);
            _rb.AddForce(transform.forward * 5, ForceMode.Impulse);
            Debug.Log("End");
            _isClimbing = false;
        }
    }


}
