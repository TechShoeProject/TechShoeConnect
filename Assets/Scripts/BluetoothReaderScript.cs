using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BluetoothReaderScript : MonoBehaviour
{
	string _hm10, ServiceUUID, Characteristic;
	public TextMeshProUGUI DetectA;

	private void Update()
	{
		if (PlayerPrefs.GetInt("Connected") == 1)
		{
			_hm10 = PlayerPrefs.GetString("_hm10", "");
			ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
			Characteristic = PlayerPrefs.GetString("Characteristic", "");
			BluetoothLEHardwareInterface.ReadCharacteristic(_hm10, ServiceUUID, Characteristic, (characteristic, bytes) =>
			{
				if(int.TryParse(System.Text.Encoding.UTF8.GetString(bytes), out int x)){
					PlayerPrefs.SetInt("LastData3", PlayerPrefs.GetInt("LastData2"));
					PlayerPrefs.SetInt("LastData2", PlayerPrefs.GetInt("LastData1"));
					PlayerPrefs.SetInt("LastData1", x);
					DetectA.text = x.ToString();
				}
			});
		}

		if(PlayerPrefs.GetInt("LastData3") == 1 && PlayerPrefs.GetInt("LastData2") == 2)
        {
			PlayerPrefs.SetInt("Dangerosite", PlayerPrefs.GetInt("LastData1"));
        }

		if(PlayerPrefs.GetInt("LastData3") == 1 && PlayerPrefs.GetInt("LastData2") == 3)
		{
			PlayerPrefs.SetInt("Distance", PlayerPrefs.GetInt("LastData1"));
		}

		if (PlayerPrefs.GetInt("LastData3") == 1 && PlayerPrefs.GetInt("LastData2") == 4)
		{
			PlayerPrefs.SetInt("Variation", PlayerPrefs.GetInt("LastData1"));
		}
	}
}
