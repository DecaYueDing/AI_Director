using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Newtonsoft.Json;
using UnityEngine.UI;

public class EmoDetection : MonoBehaviour
{
    public Text thistext;
    private string emotion; // 仍然保留为非静态变量，因为它可能与UI元素相关联且每个场景的UI可能不同

    private static bool emotionUpdated = false; // 将此变量改为静态，以便跨场景检查情绪是否已更新
    public static List<string> emotionList; // 改为静态变量
    public static List<float> confidenceList; // 改为静态变量

    
    // Start is called before the first frame update
    void Start()
    {
        emotion = "none";

        var random = new System.Random();  // Instantiate outside to reuse for both lists
        if (emotionList == null)
        {
            emotionList = new List<string>();
            for (int i = 0; i < 18; ++i)
            {
                emotionList.Add(random.Next(1, 6).ToString());  // Random number between 1 and 5
            }
        }

        if (confidenceList == null)
        {
            confidenceList = new List<float>();
            for (int i = 0; i < 18; ++i)
            {
                confidenceList.Add((float)random.NextDouble());  // Random number between 0.0 and 1.0
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (emotionUpdated) // 如果情绪已经更新
        {
            emotionUpdated = false; // 重置情绪更新标志
            thistext.text = "情感：" + emotion; // 更新UI显示
            UnityEngine.Debug.Log("接收到的情绪列表: " + string.Join(", ", emotionList));
            UnityEngine.Debug.Log("接收到的置信度列表: " + string.Join(", ", confidenceList));

        }
    }

    // 场景加载时的回调方法

    public void RunPythonScript()
    {
        Process p = new Process();
        string path = @"F:\EMOPIA_cls-main\slice_inference.py";
        p.StartInfo.FileName = @"F:\EMOPIA_cls-main\interpreter\Scripts\python.exe";
        p.StartInfo.WorkingDirectory = @"F:\EMOPIA_cls-main\";
        p.StartInfo.Arguments = path;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;

        thistext.text = "情感：" + "detecting";
        p.Start();
        p.BeginOutputReadLine();
        p.OutputDataReceived += new DataReceivedEventHandler(ReceiveHandler);
    }

    private void ReceiveHandler(object sender, DataReceivedEventArgs eventArg)
    {
        if (!string.IsNullOrEmpty(eventArg.Data))
        {
            try
            {
                // 尝试将接收到的数据解析为 JSON 对象
                var data = JsonConvert.DeserializeObject<Dictionary<string, List<object>>>(eventArg.Data);

                if (data != null && data.ContainsKey("emotions") && data.ContainsKey("confidences"))
                {
                    // 分别处理 emotions 与 confidences 字段
                    emotionList = new List<string>();
                    confidenceList = new List<float>();

                    foreach (var item in data["emotions"])
                    {
                        emotionList.Add(item.ToString()); // 将 emotions 存入 emotionList
                    }

                    foreach (var item in data["confidences"])
                    {
                        confidenceList.Add(float.Parse(item.ToString())); // 将 confidences 存入 confidenceList
                    }

                    // 此处假设 emotion 变量存储首个 emotion，您可以根据需要进行调整
                    emotion = emotionList.Count > 0 ? emotionList[0] : "";
                    emotionUpdated = true; // 设置更新标志

                }
            }
            catch (JsonException jsonEx)
            {
                UnityEngine.Debug.LogError("JSON 解析错误: " + jsonEx.Message);
            }
        }
    }


    // 提供方法以供其他脚本获取 emotionList 和 confidenceList
    public List<string> GetEmotionList()
    {
        return emotionList;
    }

    public List<float> GetConfidenceList()
    {
        return confidenceList;
    }
}