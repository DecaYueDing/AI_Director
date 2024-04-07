using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionControlledLight2 : MonoBehaviour
{
    public Renderer lightPillarRenderer; // 用于控制灯柱renderer的公共变量引用
    public Transform parentTransform;    // 用于控制的父物体的引用
    public float rotationSpeedMultiplier = 10f; // 旋转速度的乘数

    private List<string> emotionList;
    private List<float> confidenceList;

    // 定义每种情感对应的色环起始和结束Hue值
    private float[,] emotionHueRanges = new float[5, 2] {
        { 0f, 0.1f },  // 0 类情感颜色范围（红到橙）
        { 0.1f, 0.3f },  // 1 类情感颜色范围（橙到黄）
        { 0.3f, 0.5f },  // 2 类情感颜色范围（黄到绿）
        { 0.5f, 0.7f },  // 3 类情感颜色范围（绿到蓝）
        { 0.7f, 0.9f }   // 4 类情感颜色范围（蓝到紫）
    };

    private void Start()
    {
        StartCoroutine(ControlLightRoutine());
    }

    private IEnumerator ControlLightRoutine()
    {
        while (true)
        {
            //emotionList = EmoDetection.emotionList;
            //confidenceList = EmoDetection.confidenceList;

            emotionList = EmotionDetector.emotionList;
            confidenceList = EmotionDetector.confidenceList;

            for (int i = 0; i < emotionList.Count; i++)
            {
                int emotionIndex = int.Parse(emotionList[i]);
                float startHue = emotionHueRanges[emotionIndex, 0];
                float endHue = emotionHueRanges[emotionIndex, 1];

                // 控制旋转
                float confidence = confidenceList[i];
                float duration = 10f; // 10秒内的运动
                float time = 0;

                while (time < duration)
                {
                    time += Time.deltaTime;
                    float rotationX = Mathf.Lerp(-110f, -90f, confidence); // 置信度相关X轴旋转
                    float rotationZ = Mathf.Lerp(0f, 30f, Mathf.PingPong(time * rotationSpeedMultiplier * confidence, 1)); // Z轴循环旋转
                    parentTransform.localRotation = Quaternion.Euler(rotationX, 0f, rotationZ);

                    // 在指定的色环部分进行颜色渐变，同时循环改变透明度
                    float hue = Mathf.Lerp(startHue, endHue, Mathf.PingPong(time * 0.5f, 1));
                    float alpha = Mathf.PingPong(time, 1f);
                    Color targetColor = Color.HSVToRGB(hue, 1f, 1f, true);
                    targetColor.a = alpha;
                    lightPillarRenderer.material.SetColor("_TintColor", targetColor);

                    yield return null;
                }
            }

            yield return new WaitForSeconds(10f); // 每个情感和运动持续10秒
        }
    }
}