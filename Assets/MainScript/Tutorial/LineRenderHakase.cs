using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderHakase : MonoBehaviour
{
    [SerializeField] Transform _muzzulePos;
    [SerializeField] Transform _setPos;

    [SerializeField] private LineRenderer _lineRenderer;

    private void LateUpdate()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _muzzulePos.position);
        _lineRenderer.SetPosition(1, _setPos.position);
    }
}
