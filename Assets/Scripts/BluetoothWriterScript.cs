using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothWriterScript : MonoBehaviour
{
    public List<byte> DataToSend = new List<byte>();
    string _hm10, ServiceUUID, Characteristic;

    private void Awake()
    {
        StartCoroutine(SendingData());
    }

    IEnumerator SendingData()
    {
        yield return new WaitForSeconds(0.5f);
        if(DataToSend.Count != 0 && PlayerPrefs.GetInt("Connected") == 1)
        {
            _hm10 = PlayerPrefs.GetString("_hm10", "");
            ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
            Characteristic = PlayerPrefs.GetString("Characteristic", "");
            byte[] data = new byte[] { DataToSend[0] };
            BluetoothLEHardwareInterface.WriteCharacteristic(_hm10, ServiceUUID, Characteristic, data, data.Length, false, (characteristicUUID) =>
            {
                BluetoothLEHardwareInterface.Log("Write Succeeded");
                DataToSend.Remove(DataToSend[0]);
            });
        }
    }
}
