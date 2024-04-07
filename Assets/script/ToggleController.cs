using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    // ��Inspector�й���Toggle���
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public Toggle toggle4;

    

    // ��Start���������Toggle�ļ�����
    void Start()
    {
        toggle1.onValueChanged.AddListener(delegate { OnToggleValueChanged(new string[] { "0", "0", "1", "0", "0", "1", "1", "1", "1", "1", "2", "0", "0", "0", "1", "1", "2", "1", "1", "0", "0", "0", "3", "3" }
); });
        toggle2.onValueChanged.AddListener(delegate { OnToggleValueChanged(new string[] { "3", "2", "1", "3", "3", "1", "1", "1", "2", "2", "2", "3", "3", "3", "1", "1", "1", "3", "3", "2", "2", "2", "3", "3" }
); });
        toggle3.onValueChanged.AddListener(delegate { OnToggleValueChanged(new string[] { "4", "4", "4", "0", "0", "0", "4", "4", "4", "0", "0", "0", "4", "4", "4", "0", "0", "0", "4", "4", "4", "1", "1", "1" }
); });
        toggle4.onValueChanged.AddListener(delegate { OnToggleValueChanged(new string[] { "1", "1", "1", "2", "2", "2", "3", "3", "3", "1", "1", "1", "3", "3", "3", "1", "1", "1", "2", "2", "2", "3", "3", "3" }
); });
    }

    // ��Toggle��ֵ�����仯ʱ���õķ���
    void OnToggleValueChanged(string[] values)
    {
        // ֱ�Ӹ�ֵ��emotionList
        EmotionDetector.emotionList = new List<string>(values);

        // ��������������Ҫע�͵����д���
        //Debug.Log("EmotionList: " + string.Join(", ", EmotionDetector.emotionList.ToArray()));
    }
}
