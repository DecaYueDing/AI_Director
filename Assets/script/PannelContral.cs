using UnityEngine;

public class PannelContral : MonoBehaviour
{
    public GameObject initialPanel; // ��ʼ���
    public GameObject newPanel; // �����

    void Start()
    {
        ShowInitialPanel(); // ��Ϸ��ʼʱ��ʾ��ʼ���
    }

    // ��ʾ��ʼ��壬�����������
    public void ShowInitialPanel()
    {
        initialPanel.SetActive(true);
        newPanel.SetActive(false);
    }

    // ��ʾ����壬�����س�ʼ���
    public void ShowNewPanel()
    {
        initialPanel.SetActive(false);
        newPanel.SetActive(true);
    }
}