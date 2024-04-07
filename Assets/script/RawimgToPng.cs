using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Diagnostics;

public class RawimgToPng : MonoBehaviour
{
    
    public Text thistext;
    static string emotion;
    public string emo;//传递参数

    // Start is called before the first frame update
    void Start()
    {

       
        emotion = "none";
        

    }

    // Update is called once per frame
    void Update()
    {
        emo = emotion;
       


    }
   

  


    //-----------------------------------------------------------------------------
    public void RunPythonScript()
    {
        Process p = new Process();
        string path = @"F:\emotions\main.py";
        p.StartInfo.FileName = @"F:\emotions\interpreter\Scripts\python.exe";
        p.StartInfo.WorkingDirectory = @"F:\emotions\";
        p.StartInfo.Arguments = path;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = false;


        thistext.text = "情感：" + "detecting";
        p.Start();
        p.BeginOutputReadLine();
        p.OutputDataReceived += new DataReceivedEventHandler(ReceiveHandler);
        //p.WaitForExit();
        UnityEngine.Debug.Log("开始 ");

        

    }
    static void ReceiveHandler(object sender, DataReceivedEventArgs eventArg)
    {
        if (!string.IsNullOrEmpty(eventArg.Data))
        {

            emotion = eventArg.Data;
            print(eventArg.Data);
            UnityEngine.Debug.Log("完成 ");

        }
    }
}
