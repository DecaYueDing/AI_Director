using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Newtonsoft.Json;
using UnityEngine.UI;

public class EmoDetection : MonoBehaviour
{
    public Text thistext;
    private string emotion; // ��Ȼ����Ϊ�Ǿ�̬��������Ϊ��������UIԪ���������ÿ��������UI���ܲ�ͬ

    private static bool emotionUpdated = false; // ���˱�����Ϊ��̬���Ա�糡����������Ƿ��Ѹ���
    public static List<string> emotionList; // ��Ϊ��̬����
    public static List<float> confidenceList; // ��Ϊ��̬����

    
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
        if (emotionUpdated) // ��������Ѿ�����
        {
            emotionUpdated = false; // �����������±�־
            thistext.text = "��У�" + emotion; // ����UI��ʾ
            UnityEngine.Debug.Log("���յ��������б�: " + string.Join(", ", emotionList));
            UnityEngine.Debug.Log("���յ������Ŷ��б�: " + string.Join(", ", confidenceList));

        }
    }

    // ��������ʱ�Ļص�����

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

        thistext.text = "��У�" + "detecting";
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
                // ���Խ����յ������ݽ���Ϊ JSON ����
                var data = JsonConvert.DeserializeObject<Dictionary<string, List<object>>>(eventArg.Data);

                if (data != null && data.ContainsKey("emotions") && data.ContainsKey("confidences"))
                {
                    // �ֱ��� emotions �� confidences �ֶ�
                    emotionList = new List<string>();
                    confidenceList = new List<float>();

                    foreach (var item in data["emotions"])
                    {
                        emotionList.Add(item.ToString()); // �� emotions ���� emotionList
                    }

                    foreach (var item in data["confidences"])
                    {
                        confidenceList.Add(float.Parse(item.ToString())); // �� confidences ���� confidenceList
                    }

                    // �˴����� emotion �����洢�׸� emotion�������Ը�����Ҫ���е���
                    emotion = emotionList.Count > 0 ? emotionList[0] : "";
                    emotionUpdated = true; // ���ø��±�־

                }
            }
            catch (JsonException jsonEx)
            {
                UnityEngine.Debug.LogError("JSON ��������: " + jsonEx.Message);
            }
        }
    }


    // �ṩ�����Թ������ű���ȡ emotionList �� confidenceList
    public List<string> GetEmotionList()
    {
        return emotionList;
    }

    public List<float> GetConfidenceList()
    {
        return confidenceList;
    }
}