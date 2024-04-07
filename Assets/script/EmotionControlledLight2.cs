using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionControlledLight2 : MonoBehaviour
{
    public Renderer lightPillarRenderer; // ���ڿ��Ƶ���renderer�Ĺ�����������
    public Transform parentTransform;    // ���ڿ��Ƶĸ����������
    public float rotationSpeedMultiplier = 10f; // ��ת�ٶȵĳ���

    private List<string> emotionList;
    private List<float> confidenceList;

    // ����ÿ����ж�Ӧ��ɫ����ʼ�ͽ���Hueֵ
    private float[,] emotionHueRanges = new float[5, 2] {
        { 0f, 0.1f },  // 0 �������ɫ��Χ���쵽�ȣ�
        { 0.1f, 0.3f },  // 1 �������ɫ��Χ���ȵ��ƣ�
        { 0.3f, 0.5f },  // 2 �������ɫ��Χ���Ƶ��̣�
        { 0.5f, 0.7f },  // 3 �������ɫ��Χ���̵�����
        { 0.7f, 0.9f }   // 4 �������ɫ��Χ�������ϣ�
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

                // ������ת
                float confidence = confidenceList[i];
                float duration = 10f; // 10���ڵ��˶�
                float time = 0;

                while (time < duration)
                {
                    time += Time.deltaTime;
                    float rotationX = Mathf.Lerp(-110f, -90f, confidence); // ���Ŷ����X����ת
                    float rotationZ = Mathf.Lerp(0f, 30f, Mathf.PingPong(time * rotationSpeedMultiplier * confidence, 1)); // Z��ѭ����ת
                    parentTransform.localRotation = Quaternion.Euler(rotationX, 0f, rotationZ);

                    // ��ָ����ɫ�����ֽ�����ɫ���䣬ͬʱѭ���ı�͸����
                    float hue = Mathf.Lerp(startHue, endHue, Mathf.PingPong(time * 0.5f, 1));
                    float alpha = Mathf.PingPong(time, 1f);
                    Color targetColor = Color.HSVToRGB(hue, 1f, 1f, true);
                    targetColor.a = alpha;
                    lightPillarRenderer.material.SetColor("_TintColor", targetColor);

                    yield return null;
                }
            }

            yield return new WaitForSeconds(10f); // ÿ����к��˶�����10��
        }
    }
}