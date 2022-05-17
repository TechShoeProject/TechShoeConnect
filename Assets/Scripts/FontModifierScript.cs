using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FontModifierScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown fontChooser;
    private void Awake()
    {
        fontChooser.value = PlayerPrefs.GetInt("Police");
    }

    public void ChangeFont()
    {
        if (PlayerPrefs.GetInt("Police") != fontChooser.value)
        {
            PlayerPrefs.SetInt("Police", fontChooser.value);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
