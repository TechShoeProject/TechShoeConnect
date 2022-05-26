using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothWriterScript : MonoBehaviour
{
    public List<byte> DataToSend = new List<byte>();
    public List<byte> OnlyOneData = new List<byte>();
    string _hm10, ServiceUUID, Characteristic;

    private void Awake()
    {
        StartCoroutine(SendingData());
    }

    IEnumerator SendingData()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (DataToSend.Count >= 4 && PlayerPrefs.GetInt("Connected") == 1)
            {
                _hm10 = PlayerPrefs.GetString("_hm10", "");
                ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
                Characteristic = PlayerPrefs.GetString("Characteristic", "");
                byte[] data = new byte[] { DataToSend[0], DataToSend[1], DataToSend[2], DataToSend[3]};
                BluetoothLEHardwareInterface.WriteCharacteristic(_hm10, ServiceUUID, Characteristic, data, data.Length, false, (characteristicUUID) =>
                {
                    BluetoothLEHardwareInterface.Log("Write Succeeded");
                    DataToSend.Remove(DataToSend[3]);
                    DataToSend.Remove(DataToSend[2]);
                    DataToSend.Remove(DataToSend[1]);
                    DataToSend.Remove(DataToSend[0]);
                });
            }else if (PlayerPrefs.GetInt("Connected") == 1 && OnlyOneData.Count != 0)
            {
                _hm10 = PlayerPrefs.GetString("_hm10", "");
                ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
                Characteristic = PlayerPrefs.GetString("Characteristic", "");
                byte[] data = new byte[] { OnlyOneData[0] };
                BluetoothLEHardwareInterface.WriteCharacteristic(_hm10, ServiceUUID, Characteristic, data, data.Length, false, (characteristicUUID) =>
                {
                    BluetoothLEHardwareInterface.Log("Write Succeeded");
                    OnlyOneData.Remove(OnlyOneData[0]);
                });
            }
        }
    }
}
