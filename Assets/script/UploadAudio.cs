using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class UploadAudio : MonoBehaviour
{

    // 预定义音频文件的路径
    private string filePath = @"F:\AudioClassification-Pytorch\dataset\bgm.wav";

    // 这个方法将由按钮的点击事件调用
    public void UploadFile()
    {
        StartCoroutine(UploadAudioFile());
    }

    IEnumerator UploadAudioFile()
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, "bgm.wav", "audio/wav");

        // 请确保这里的URL是API的正确地址
        string url = "http://region-3.seetacloud.com:27011//predict";
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            Debug.Log("Uploaded successfully");
            // 服务器的返回信息将显示在控制台中
            Debug.Log(uwr.downloadHandler.text);
        }
    }
}