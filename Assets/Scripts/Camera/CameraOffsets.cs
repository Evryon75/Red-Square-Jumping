using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOffsets : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    private CinemachineFramingTransposer transposer;
    public static Vector2 offsets;

    private void Start()
    {
        offsets = new Vector2(0.5f, 0.5f);
        transposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void LateUpdate()
    {
        transposer.m_ScreenX = offsets.x;
        transposer.m_ScreenY = offsets.y;
    }
}
