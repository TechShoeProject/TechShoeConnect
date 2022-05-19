using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BluetoothReaderScript : MonoBehaviour
{
	string _hm10, ServiceUUID, Characteristic;
	float actualTime, lastReceive;

    private void Awake()
    {
		actualTime = System.DateTime.Now.Second + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Hour * 3600;
		lastReceive = -1;
	}

    private void LateUpdate()
	{
		actualTime = System.DateTime.Now.Second + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Hour * 3600;
		if(actualTime - lastReceive > 2 && lastReceive != -1)
        {
			PlayerPrefs.SetInt("LastData4", -1);
			PlayerPrefs.SetInt("LastData3", -1);
			PlayerPrefs.SetInt("LastData2", -1);
			PlayerPrefs.SetInt("LastData1", -1);
			lastReceive = -1;
		}

		if (PlayerPrefs.GetInt("Connected") == 1)
		{
			_hm10 = PlayerPrefs.GetString("_hm10", "");
			ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
			Characteristic = PlayerPrefs.GetString("Characteristic", "");
			BluetoothLEHardwareInterface.ReadCharacteristic(_hm10, ServiceUUID, Characteristic, (characteristic, bytes) =>
			{
				if (PlayerPrefs.GetInt("LastData4") != -1)
					if (int.TryParse(System.Text.Encoding.UTF8.GetString(bytes), out int x))
					{
						PlayerPrefs.SetInt("LastData4", PlayerPrefs.GetInt("LastData3"));
						PlayerPrefs.SetInt("LastData3", PlayerPrefs.GetInt("LastData2"));
						PlayerPrefs.SetInt("LastData2", PlayerPrefs.GetInt("LastData1"));
						PlayerPrefs.SetInt("LastData1", x);
						lastReceive = actualTime;
					}
				else if (int.TryParse(System.Text.Encoding.UTF8.GetString(bytes), out int y))
					{
						PlayerPrefs.SetInt("LastData4", -1);
						PlayerPrefs.SetInt("LastData3", -1);
						PlayerPrefs.SetInt("LastData2", -1);
						PlayerPrefs.SetInt("LastData1", y);
						lastReceive = actualTime;
					}
			});
		}

		if(PlayerPrefs.GetInt("LastData4") == 1 && PlayerPrefs.GetInt("LastData3") == 0 && PlayerPrefs.GetInt("LastData2") == 2)
        {
			PlayerPrefs.SetInt("Dangerosite", PlayerPrefs.GetInt("LastData1"));
        }

		if(PlayerPrefs.GetInt("LastData3") == 1 && PlayerPrefs.GetInt("LastData3") == 0 && PlayerPrefs.GetInt("LastData2") == 3)
		{
			PlayerPrefs.SetInt("Distance", PlayerPrefs.GetInt("LastData1"));
		}

		if (PlayerPrefs.GetInt("LastData3") == 1 && PlayerPrefs.GetInt("LastData3") == 0 && PlayerPrefs.GetInt("LastData2") == 4)
		{
			PlayerPrefs.SetInt("Variation", PlayerPrefs.GetInt("LastData1"));
		}
	}
}
