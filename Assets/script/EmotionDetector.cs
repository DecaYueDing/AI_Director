using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EmotionDetector : MonoBehaviour
{
    public Text thistext; // 关联的按钮Text
    private string emotion;

    // 创建静态变量数组用来跨场景
    public static List<string> emotionList = new List<string>();
    public static List<float> confidenceList = new List<float>();

    void Start()
    {
        thistext.text = "MER"; // 设置按钮的初始文本为 "MER"

        // 随机初始化emotionList和confidenceList的长度都为24
        emotionList = Enumerable.Range(0, 24).Select(x => Random.Range(1, 6).ToString()).ToList();
        confidenceList = Enumerable.Range(0, 24).Select(x => Random.Range(0.6f, 0.99f)).ToList();
    }

    public void RunScript()
    {
        StartCoroutine(DetectingAnimation());
    }

    IEnumerator DetectingAnimation()
    {
        thistext.text = "detect";
        // 每隔0.1秒在“detecting”后面加一个“-”
        for (int i = 0; i < 4; i++)
        {
            thistext.text += "-";
            yield return new WaitForSeconds(0.2f);
        }

        // 计时0.5秒后改变文本
        yield return new WaitForSeconds(0.8f);

        // 显示“识别到情感：”加上emotionList的第一个元素
        thistext.text = $"情感：{emotionList.FirstOrDefault()}"; // 注意确保列表不会为空
        UnityEngine.Debug.Log("接收到的情绪列表: " + string.Join(", ", emotionList));
        UnityEngine.Debug.Log("接收到的置信度列表: " + string.Join(", ", confidenceList));
    }
}