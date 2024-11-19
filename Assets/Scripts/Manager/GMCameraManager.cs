using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMCameraManager : GMManagerBase<GMCameraManager>
{
    private CinemachineBrain _mainCamera;
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineConfiner2D _confiner2D;
    public override IEnumerator GMAwake()
    {
        _mainCamera = GetComponentInChildren<CinemachineBrain>();
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _confiner2D = GetComponentInChildren<CinemachineConfiner2D>();
        return base.GMAwake();
    }
    public override IEnumerator GMStart()
    {
        return base.GMStart();
    }
    public void SetConfinerBoundingShape(Collider2D collider)
    {
        if (_confiner2D == null)
            return;
        _confiner2D.m_BoundingShape2D = collider;
    }
    public void SetCinemachineTarget(Transform transform)
    {
        if (_virtualCamera == null)
            return;
        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;
    }
    public void SetCinemachineFollow(Transform transform)
    {
        if (_virtualCamera == null)
            return;
        _virtualCamera.Follow = transform;
    }
}
