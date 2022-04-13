using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UltimateRedirector : MonoBehaviour
{
    public void Redirector(int i)
    {
        if (i == 1)
            SceneManager.LoadScene(PlayerPrefs.GetInt("Aveugle") == 1 ? 2 : 1);
        else
            SceneManager.LoadScene(i);
    }
}
