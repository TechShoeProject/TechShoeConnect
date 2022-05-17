using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DangerositeWriterScript : MonoBehaviour
{
	string _hm10, ServiceUUID, Characteristic;
	[SerializeField] Slider value;
	[SerializeField] TextMeshProUGUI distance, variation;
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
	private void Awake()
    {
		SendByte(2);
		SendByte(2);
		value.maxValue = 255;
    }
    private void Update()
    {
		value.value = PlayerPrefs.GetInt("Dangerosite");
		distance.text = "Distance avec l'objet le plus proche :\n<color=red>" + PlayerPrefs.GetInt("Distance").ToString() + "cm</color>";
		variation.text = "Variation de distance avec l'objet le plus proche :\n<color=red>" + PlayerPrefs.GetInt("Variation").ToString() + "cm/s</color>";
	}
}
