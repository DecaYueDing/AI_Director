using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour
{
    public void quitgame()
    {
#if UNITY_EDITOR //�༭�����˳���Ϸ
        UnityEditor.EditorApplication.isPlaying = false;
#else //Ӧ�ó������˳���Ϸ
	        UnityEngine.Application.Quit();
#endif
    }
}