using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    private bool _inGameCam = true;

    [SerializeField] private CinemachineVirtualCamera inGameCamera;
    [SerializeField] private CinemachineVirtualCamera drawingCamera;


    private void SwitchPriority()
    {
        if (_inGameCam)
        {
            inGameCamera.Priority = 0;
            drawingCamera.Priority = 1;
        }
        else
        {
            inGameCamera.Priority = 1;
            drawingCamera.Priority = 0;
        }

        _inGameCam = !_inGameCam;
    }
}
