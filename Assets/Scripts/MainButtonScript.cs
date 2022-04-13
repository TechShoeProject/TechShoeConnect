using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainButtonScript : MonoBehaviour
{

    public void QuitApp()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
