using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camerachange : MonoBehaviour
{
    public CinemachineVirtualCamera cv1;
    public CinemachineVirtualCamera cv2;
    public CinemachineVirtualCamera cv3;
    public CinemachineVirtualCamera cv4;
    public CinemachineVirtualCamera cv5;
    public void change()
    {
        cv1.Priority = 100;
        cv2.Priority = 10;
        cv3.Priority = 10;
        cv4.Priority = 10;
        cv5.Priority = 10;

    }
}
