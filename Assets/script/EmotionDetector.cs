using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EmotionDetector : MonoBehaviour
{
    public Text thistext; // �����İ�ťText
    private string emotion;

    // ������̬�������������糡��
    public static List<string> emotionList = new List<string>();
    public static List<float> confidenceList = new List<float>();

    void Start()
    {
        thistext.text = "MER"; // ���ð�ť�ĳ�ʼ�ı�Ϊ "MER"

        // �����ʼ��emotionList��confidenceList�ĳ��ȶ�Ϊ24
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
        // ÿ��0.1���ڡ�detecting�������һ����-��
        for (int i = 0; i < 4; i++)
        {
            thistext.text += "-";
            yield return new WaitForSeconds(0.2f);
        }

        // ��ʱ0.5���ı��ı�
        yield return new WaitForSeconds(0.8f);

        // ��ʾ��ʶ����У�������emotionList�ĵ�һ��Ԫ��
        thistext.text = $"��У�{emotionList.FirstOrDefault()}"; // ע��ȷ���б���Ϊ��
        UnityEngine.Debug.Log("���յ��������б�: " + string.Join(", ", emotionList));
        UnityEngine.Debug.Log("���յ������Ŷ��б�: " + string.Join(", ", confidenceList));
    }
}