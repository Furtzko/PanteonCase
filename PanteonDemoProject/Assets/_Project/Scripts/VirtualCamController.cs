using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamController : BaseSingleton<VirtualCamController>
{
    private CinemachineVirtualCamera cmVirtualCam;
    private float shakeTimer;

    private void Awake()
    {
        cmVirtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmBasicMultiChannelPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cmBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cmBasicMultiChannelPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cmBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
