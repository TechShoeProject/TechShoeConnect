using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugScript : MonoBehaviour
{
    [SerializeField] TMP_InputField toSend;
    [SerializeField] TextMeshProUGUI received;
    string _hm10, ServiceUUID, Characteristic;

    private void Awake()
    {

    }
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

    public void SendData()
    {
        SendByte((byte)Mathf.Clamp(int.Parse(toSend.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Substring(0, toSend.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Length - 1)), 0, 255));
    }

    private void Update()
    {
        //received.text = PlayerPrefs.GetInt("LastData4").ToString() + " " + PlayerPrefs.GetInt("LastData3").ToString() + " " + PlayerPrefs.GetInt("LastData2").ToString() + " " + PlayerPrefs.GetInt("LastData1").ToString();
        BluetoothLEHardwareInterface.ReadCharacteristic(_hm10, ServiceUUID, Characteristic, (characteristic, bytes) =>
        {
            if (int.TryParse(System.Text.Encoding.UTF8.GetString(bytes), out int x))
            {
                received.text = x.ToString();
            }
        });
    }
}
