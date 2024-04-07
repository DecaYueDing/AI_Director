using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class UploadAudio : MonoBehaviour
{

    // Ԥ������Ƶ�ļ���·��
    private string filePath = @"F:\AudioClassification-Pytorch\dataset\bgm.wav";

    // ����������ɰ�ť�ĵ���¼�����
    public void UploadFile()
    {
        StartCoroutine(UploadAudioFile());
    }

    IEnumerator UploadAudioFile()
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, "bgm.wav", "audio/wav");

        // ��ȷ�������URL��API����ȷ��ַ
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
            // �������ķ�����Ϣ����ʾ�ڿ���̨��
            Debug.Log(uwr.downloadHandler.text);
        }
    }
}