using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class changecamera : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineVirtualCamera cv1;
    public CinemachineVirtualCamera cv2;
    public CinemachineVirtualCamera cv3;
    public CinemachineVirtualCamera cv4;
    public CinemachineVirtualCamera cv5;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    private bool autoSwitch = true;
    private Coroutine switchCoroutine;

    // ��ǰ������������
    private int currentCameraIndex = 0;

    // ����ת�ƾ���
    private float[,] transitionMatrix = new float[,]
    {
        {0.0f, 0.5f, 0.2f, 0.2f, 0.1f},
        {0.4f, 0.0f, 0.3f, 0.2f, 0.1f},
        {0.3f, 0.3f, 0.0f, 0.2f, 0.2f},
        {0.2f, 0.2f, 0.3f, 0.0f, 0.3f},
        {0.1f, 0.1f, 0.2f, 0.4f, 0.2f}
    };

    void Start()
    {
        SwitchCamerasCoroutine();

        // Ϊÿ����ť��ӵ���¼�������
        button1.onClick.AddListener(() => SetCameraPriority(cv1, button1, 0));
        button2.onClick.AddListener(() => SetCameraPriority(cv2, button2, 1));
        button3.onClick.AddListener(() => SetCameraPriority(cv3, button3, 2));
        button4.onClick.AddListener(() => SetCameraPriority(cv4, button4, 3));
        button5.onClick.AddListener(() => SetCameraPriority(cv5, button5, 4));
    }

    void Update()
    {
        // ��������������ʵʱ���µ��߼�
    }

    void camera_change()
    {
        int nextCameraIndex = ChooseNextCamera(currentCameraIndex);
        ResetCameraPriorities();
        Button selectedButton = null;

        switch (nextCameraIndex)
        {
            case 0:
                cv1.Priority = 100;
                selectedButton = button1;
                break;
            case 1:
                cv2.Priority = 100;
                selectedButton = button2;
                break;
            case 2:
                cv3.Priority = 100;
                selectedButton = button3;
                break;
            case 3:
                cv4.Priority = 100;
                selectedButton = button4;
                break;
            case 4:
                cv5.Priority = 100;
                selectedButton = button5;
                break;
        }

        currentCameraIndex = nextCameraIndex; // ���µ�ǰ�������

        if (selectedButton != null)
        {
            UpdateButtonColors(selectedButton);
        }
    }

    int ChooseNextCamera(int currentCamera)
    {
        float diceRoll = Random.Range(0.0f, 1.0f);
        float cumulative = 0.0f;

        for (int i = 0; i < 5; i++)
        {
            cumulative += transitionMatrix[currentCamera, i];
            if (diceRoll < cumulative)
            {
                return i;
            }
        }

        return currentCamera; // ���û��ѡ��ɹ������ֵ�ǰ���
    }

    void SetCameraPriority(CinemachineVirtualCamera camera, Button button, int cameraIndex)
    {
        if (switchCoroutine != null)
        {
            StopCoroutine(switchCoroutine); // ֹͣ�Զ��л�
        }
        autoSwitch = false;

        currentCameraIndex = cameraIndex; // ���µ�ǰ�������
        ResetCameraPriorities();
        camera.Priority = 100;
        UpdateButtonColors(button);
    }

    void UpdateButtonColors(Button activeButton)
    {
        button1.image.color = Color.white;
        button2.image.color = Color.white;
        button3.image.color = Color.white;
        button4.image.color = Color.white;
        button5.image.color = Color.white;

        activeButton.image.color = Color.red;
    }

    void SwitchCamerasCoroutine()
    {
        switchCoroutine = StartCoroutine(SwitchCameras());
    }

    IEnumerator SwitchCameras()
    {
        while (autoSwitch)
        {
            camera_change();
            yield return new WaitForSeconds(4.0f);
        }
    }

    void ResetCameraPriorities()
    {
        cv1.Priority = 10;
        cv2.Priority = 10;
        cv3.Priority = 10;
        cv4.Priority = 10;
        cv5.Priority = 10;
    }

    public void Stop()
    {
        if (switchCoroutine != null)
        {
            StopCoroutine(switchCoroutine);
        }
        autoSwitch = false;
        ResetCameraPriorities();
    }

    public void Director()
    {
        Stop();
        autoSwitch = true;
        SwitchCamerasCoroutine();
    }
}