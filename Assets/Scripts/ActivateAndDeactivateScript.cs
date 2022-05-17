using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ActivateAndDeactivateScript : MonoBehaviour
{
	string _hm10, ServiceUUID, Characteristic;
	bool isAsking = false, toggleValue;
	public Toggle DetecToggle;
	void SendByte(byte value)
	{
		_hm10 = PlayerPrefs.GetString("_hm10", "");
		ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
		Characteristic = PlayerPrefs.GetString("Characteristic", "");
		byte[] data = new byte[] { value };
		// notice that the 6th parameter is false. this is because the HM10 doesn't support withResponse writing to its characteristic.
		// some devices do support this setting and it is prefered when they do so that you can know for sure the data was received by 
		// the device
		BluetoothLEHardwareInterface.WriteCharacteristic(_hm10, ServiceUUID, Characteristic, data, data.Length, false, (characteristicUUID) => {

			BluetoothLEHardwareInterface.Log("Write Succeeded");
		});
	}

    private void Start()
    {
		toggleValue = DetecToggle.isOn;
    }

    void ObstacleToggle()
    {
		SendByte(4);
		SendByte(0);
		isAsking = true;
    }
	void SonoreToggle()
	{
		SendByte(4);
		SendByte(1);
		isAsking = true;
	}

	private void Update()
    {
        if (isAsking && SceneManager.GetActiveScene().buildIndex == 3)
        {
			if(PlayerPrefs.GetInt("LastData2") == 4 && PlayerPrefs.GetInt("LastData1") == 0)
            {
				DetecToggle.transform.Find("Background").GetComponent<Image>().color = Color.green;
				isAsking = false;
				DetecToggle.interactable = true;
				PlayerPrefs.SetInt("ObstacleActive", DetecToggle.isOn ? 1 : 0);
			}
        }

		if (isAsking && SceneManager.GetActiveScene().buildIndex == 7)
		{
			if (PlayerPrefs.GetInt("LastData2") == 4 && PlayerPrefs.GetInt("LastData1") == 1)
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
