using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncDanger : MonoBehaviour
{
    [SerializeField] Slider faible, moyen, haut, pop;
    bool faibleU, moyenU, hautU;
    string _hm10, ServiceUUID, Characteristic;
	//enum States
	//{
	//	None,
	//	Scan,
	//	Connect,
	//	RequestMTU,
	//	Subscribe,
	//	Unsubscribe,
	//	Disconnect,
	//	Communication,
	//}

	//private bool _workingFoundDevice = true;
	//private bool _connected = false;
	//private float _timeout = 0f;
	//private States _state = States.None;
	//private bool _foundID = false;
	public string DeviceName = "DSD TECH";

	private void Awake()
    {
        _hm10 = PlayerPrefs.GetString("_hm10", "");
        ServiceUUID = PlayerPrefs.GetString("ServiceUUID", "");
        Characteristic = PlayerPrefs.GetString("Characteristic", "");
    }
	//void SetState(States newState, float timeout)
	//{
	//	_state = newState;
	//	_timeout = timeout;
	//}
	//bool IsEqual(string uuid1, string uuid2)
	//{
	//	if (uuid1.Length == 4)
	//		uuid1 = FullUUID(uuid1);
	//	if (uuid2.Length == 4)
	//		uuid2 = FullUUID(uuid2);

	//	return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
	//}
	//string FullUUID(string uuid)
	//{
	//	return "0000" + uuid + "-0000-1000-8000-00805F9B34FB";
	//}

	private void Update()
    {
		//if (_timeout > 0f)
		//{
		//	_timeout -= Time.deltaTime;
		//	if (_timeout <= 0f)
		//	{
		//		_timeout = 0f;

		//		switch (_state)
		//		{
		//			case States.None:
		//				break;

		//			case States.Scan:

		//				BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) => {

		//					// we only want to look at devices that have the name we are looking for
		//					// this is the best way to filter out devices
		//					if (name.Contains(DeviceName))
		//					{
		//						_workingFoundDevice = true;

		//						// it is always a good idea to stop scanning while you connect to a device
		//						// and get things set up
		//						BluetoothLEHardwareInterface.StopScan();

		//						// add it to the list and set to connect to it
		//						_hm10 = address;

		//						SetState(States.Connect, 0.5f);

		//						_workingFoundDevice = false;
		//					}

		//				}, null, false, false);
		//				break;

		//			case States.Connect:
		//				// set these flags
		//				_foundID = false;

		//				// note that the first parameter is the address, not the name. I have not fixed this because
		//				// of backwards compatiblity.
		//				// also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
		//				// the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
		//				// large enough that it will be finished enumerating before you try to subscribe or do any other operations.
		//				BluetoothLEHardwareInterface.ConnectToPeripheral(_hm10, null, null, (address, serviceUUID, characteristicUUID) => {

		//					if (IsEqual(serviceUUID, ServiceUUID))
		//					{
		//						// if we have found the characteristic that we are waiting for
		//						// set the state. make sure there is enough timeout that if the
		//						// device is still enumerating other characteristics it finishes
		//						// before we try to subscribe
		//						if (IsEqual(characteristicUUID, Characteristic))
		//						{
		//							_connected = true;
		//							SetState(States.RequestMTU, 2f);
		//						}
		//					}
		//				}, (disconnectedAddress) => {
		//					BluetoothLEHardwareInterface.Log("Device disconnected: " + disconnectedAddress);
		//				});
		//				break;

		//			case States.RequestMTU:

		//				BluetoothLEHardwareInterface.RequestMtu(_hm10, 185, (address, newMTU) =>
		//				{
		//					SetState(States.Subscribe, 0.1f);
		//				});
		//				break;

		//			case States.Subscribe:

		//				BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_hm10, ServiceUUID, Characteristic, null, (address, characteristicUUID, bytes) => {

		//				});

		//				// set to the none state and the user can start sending and receiving data
		//				_state = States.None;
		//				PlayerPrefs.SetString("_hm10", _hm10);
		//				PlayerPrefs.SetString("ServiceUUID", ServiceUUID);
		//				PlayerPrefs.SetString("Characteristic", Characteristic);

		//				break;

		//			case States.Unsubscribe:
		//				BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_hm10, ServiceUUID, Characteristic, null);
		//				SetState(States.Disconnect, 4f);
		//				break;

		//			case States.Disconnect:
		//				if (_connected)
		//				{
		//					BluetoothLEHardwareInterface.DisconnectPeripheral(_hm10, (address) => {
		//						BluetoothLEHardwareInterface.DeInitialize(() => {

		//							_connected = false;
		//							_state = States.None;
		//						});
		//					});
		//				}
		//				else
		//				{
		//					BluetoothLEHardwareInterface.DeInitialize(() => {

		//						_state = States.None;
		//					});
		//				}
		//				break;
		//		}
		//	}
		//}
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
		SendByte(3);
		SendByte(1);
		SendByte((byte)vibType);
		SendByte(toSend);
		
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
