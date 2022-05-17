using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainButtonScript : MonoBehaviour
{
    string _hm10, ServiceUUID, Characteristic;
    private void Awake()
    {
        _hm10 = PlayerPrefs.GetString("_hm10", "");
        ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
        Characteristic = PlayerPrefs.GetString("Characteristic", "");
    }
    void SendByte(byte value)
	{
		byte[] data = new byte[] { value };
		// notice that the 6th parameter is false. this is because the HM10 doesn't support withResponse writing to its characteristic.
		// some devices do support this setting and it is prefered when they do so that you can know for sure the data was received by 
		// the device
		BluetoothLEHardwareInterface.WriteCharacteristic(_hm10, ServiceUUID, Characteristic, data, data.Length, false, (characteristicUUID) => {

			BluetoothLEHardwareInterface.Log("Write Succeeded");
		});
	}

	public void Vibrate()
    {
        SendByte();
    }

	public void QuitApp()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
