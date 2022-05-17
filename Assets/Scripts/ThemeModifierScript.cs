using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThemeModifierScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown themeChooser;
    private void Awake()
    {
        themeChooser.value = PlayerPrefs.GetInt("Theme");
    }

    public void ChangeTheme()
    {
        if (PlayerPrefs.GetInt("Theme") != themeChooser.value)
        {
            PlayerPrefs.SetInt("Theme", themeChooser.value);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
