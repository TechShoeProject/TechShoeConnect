using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncDanger : MonoBehaviour
{
    [SerializeField] Slider faible, moyen, haut, pop;
    bool faibleU, moyenU, hautU;
    string _hm10, ServiceUUID, Characteristic;
    QueuedData dataToSend;

	private void Awake()
    {
        _hm10 = PlayerPrefs.GetString("_hm10", "");
        ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
        Characteristic = PlayerPrefs.GetString("Characteristic", "");
        faible.value = PlayerPrefs.GetFloat("VibrationOFaible", 0.3f);
        moyen.value = PlayerPrefs.GetFloat("VibrationOMoyen", 0.6f);
        haut.value = PlayerPrefs.GetFloat("VibrationOHaut", 1f);
        pop.value = PlayerPrefs.GetFloat("VibrationOPop", 0.8f);
    }

	private void Update()
    {
		if (faible.value > moyen.value && faibleU)
        {
            moyen.value = faible.value;
        }
        if(moyen.value > haut.value && (moyenU || faibleU))
        {
            haut.value = moyen.value;
        }
        if(haut.value < moyen.value && hautU)
        {
            moyen.value = haut.value;
        }
        if(moyen.value < faible.value && (moyenU || hautU))
        {
            faible.value = moyen.value;
        }
    }

    public void SendDataVibationsDanger(int vibType)
    {
		Debug.Log("Vibration");
        float value;
        switch (vibType)
        {
            case 1:
                value = faible.value;
                break;
            case 2:
                value = moyen.value;
                break;
            case 3:
                value = haut.value;
                break;
            default:
                value = pop.value;
                break;
        }
        byte toSend = (byte)(int)(value * 255);
        dataToSend.valueToSend.Add(3);
        dataToSend.valueToSend.Add(1);
        dataToSend.valueToSend.Add(vibType);
        dataToSend.valueToSend.Add(toSend);
        PlayerPrefs.SetFloat("VibrationOFaible", faible.value);
        PlayerPrefs.SetFloat("VibrationOMoyen", moyen.value);
        PlayerPrefs.SetFloat("VibrationOHaut", haut.value);
        PlayerPrefs.SetFloat("VibrationOPop", pop.value);

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

	public void Used(int x)
    {
        switch (x)
        {
            case 0:
                faibleU = true;
                break;
            case 1:
                moyenU = true;
                break;
            case 2:
                hautU = true;
                break;
            case 3:
                faibleU = false;
                break;
            case 4:
                moyenU = false;
                break;
            case 5:
                hautU = false;
                break;
        }
    }
}
