using UnityEngine;

public class PannelContral : MonoBehaviour
{
    public GameObject initialPanel; // 初始面板
    public GameObject newPanel; // 新面板

    void Start()
    {
        ShowInitialPanel(); // 游戏开始时显示初始面板
    }

    // 显示初始面板，并隐藏新面板
    public void ShowInitialPanel()
    {
        initialPanel.SetActive(true);
        newPanel.SetActive(false);
    }

    // 显示新面板，并隐藏初始面板
    public void ShowNewPanel()
    {
        initialPanel.SetActive(false);
        newPanel.SetActive(true);
    }
}