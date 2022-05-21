using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ActivateAndDeactivateScript : MonoBehaviour
{
	bool isAsking = false, toggleValue;
	public Toggle DetecToggle;
	GameObject holder;
	
    private void Awake()
    {
		holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
	}
    private void Start()
    {
		toggleValue = DetecToggle.isOn;
    }

    void ObstacleToggle()
	{
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(4);
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
		isAsking = true;
    }
	void SonoreToggle()
	{
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(4);
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(1);
		isAsking = true;
	}

	private void Update()
    {
        if (isAsking && SceneManager.GetActiveScene().buildIndex == 3)
        {
			if(PlayerPrefs.GetInt("LastData4") == 4 && PlayerPrefs.GetInt("LastData3") == 0 && PlayerPrefs.GetInt("LastData2") == 0 && PlayerPrefs.GetInt("LastData1") == 0)
            {
				DetecToggle.transform.Find("Background").GetComponent<Image>().color = Color.green;
				isAsking = false;
				DetecToggle.interactable = true;
				PlayerPrefs.SetInt("ObstacleActive", DetecToggle.isOn ? 1 : 0);
			}
        }

		if (isAsking && SceneManager.GetActiveScene().buildIndex == 7)
		{
			if (PlayerPrefs.GetInt("LastData4") == 4 && PlayerPrefs.GetInt("LastData3") == 1 && PlayerPrefs.GetInt("LastData2") == 0 && PlayerPrefs.GetInt("LastData1") == 0)
			{
				DetecToggle.transform.Find("Background").GetComponent<Image>().color = Color.green;
				isAsking = false;
				DetecToggle.interactable = true;
				PlayerPrefs.SetInt("SonoreActive", DetecToggle.isOn ? 1 : 0);
			}
		}

		if (toggleValue != DetecToggle.isOn)
        {
			toggleValue = DetecToggle.isOn;
			DetecToggle.interactable = false;
			if (SceneManager.GetActiveScene().buildIndex == 3)
				ObstacleToggle();
			else if (SceneManager.GetActiveScene().buildIndex == 7)
				SonoreToggle();
        }
    }
}
