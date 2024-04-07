using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    // 在Inspector中关联Toggle组件
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public Toggle toggle4;

    

    // 在Start方法中添加Toggle的监听器
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

    // 当Toggle的值发生变化时调用的方法
    void OnToggleValueChanged(string[] values)
    {
        // 直接赋值给emotionList
        EmotionDetector.emotionList = new List<string>(values);

        // 输出结果，根据需要注释掉这行代码
        //Debug.Log("EmotionList: " + string.Join(", ", EmotionDetector.emotionList.ToArray()));
    }
}
