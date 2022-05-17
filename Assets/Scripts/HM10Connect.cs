using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;

public class HM10Connect : MonoBehaviour
{
	public string DeviceName = "DSD TECH";
	public string ServiceUUID = "FFE0";
	public string Characteristic = "FFE1";

	[SerializeField] Image HM10_Status;

	enum States
	{
		None,
		Scan,
		Connect,
		RequestMTU,
		Subscribe,
		Unsubscribe,
		Disconnect,
		Communication,
	}

	private bool _workingFoundDevice = true;
	private bool _connected = false;
	private float _timeout = 0f;
	private States _state = States.None;
	private bool _foundID = false;

	// this is our hm10 device
	private string _hm10;


	void Reset()
	{
		_workingFoundDevice = false;    // used to guard against trying to connect to a second device while still connecting to the first
		_connected = false;
		_timeout = 0f;
		_state = States.None;
		_foundID = false;
		_hm10 = null;
	}

	void SetState(States newState, float timeout)
	{
		_state = newState;
		_timeout = timeout;
	}

	void StartProcess()
	{
		HM10_Status.color = Color.black;
		PlayerPrefs.SetString("BTStatus", "<color=black><b>Pas de Bluetooth</color>");

		Reset();
		BluetoothLEHardwareInterface.Initialize(true, false, () => {

			SetState(States.Scan, 0.1f);
			HM10_Status.color = Color.gray;
			PlayerPrefs.SetString("BTStatus", "<color=gray><b>Bluetooth activé</color>");

		}, (error) => {

			BluetoothLEHardwareInterface.Log("Error: " + error);
		});
	}

	// Use this for initialization
	void Start()
	{
		HM10_Status.color = Color.white;

		StartProcess();
	}

	// Update is called once per frame
	void Update()
	{
		if (_timeout > 0f)
		{
			_timeout -= Time.deltaTime;
			if (_timeout <= 0f)
			{
				_timeout = 0f;

				switch (_state)
				{
					case States.None:
						break;

					case States.Scan:
						HM10_Status.color = Color.blue;
						PlayerPrefs.SetString("BTStatus", "<color=blue><b>Recherche</color>");

						BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) => {

							// we only want to look at devices that have the name we are looking for
							// this is the best way to filter out devices
							if (name.Contains(DeviceName))
							{
								_workingFoundDevice = true;

								// it is always a good idea to stop scanning while you connect to a device
								// and get things set up
								BluetoothLEHardwareInterface.StopScan();

								// add it to the list and set to connect to it
								_hm10 = address;

								HM10_Status.color = Color.cyan;

								SetState(States.Connect, 0.5f);
								PlayerPrefs.SetString("BTStatus", "<color=blue><b>Connection en cours</color>");

								_workingFoundDevice = false;
							}

						}, null, false, false);
						break;

					case States.Connect:
						// set these flags
						_foundID = false;

						HM10_Status.color = Color.yellow;

						// note that the first parameter is the address, not the name. I have not fixed this because
						// of backwards compatiblity.
						// also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
						// the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
						// large enough that it will be finished enumerating before you try to subscribe or do any other operations.
						BluetoothLEHardwareInterface.ConnectToPeripheral(_hm10, null, null, (address, serviceUUID, characteristicUUID) => {

							if (IsEqual(serviceUUID, ServiceUUID))
							{
								// if we have found the characteristic that we are waiting for
								// set the state. make sure there is enough timeout that if the
								// device is still enumerating other characteristics it finishes
								// before we try to subscribe
								if (IsEqual(characteristicUUID, Characteristic))
								{
									_connected = true;
									SetState(States.RequestMTU, 2f);

									HM10_Status.color = new Vector4(1, 0.6f, 0, 1);
								}
							}
						}, (disconnectedAddress) => {
							BluetoothLEHardwareInterface.Log("Device disconnected: " + disconnectedAddress);
							HM10_Status.color = Color.red;
							PlayerPrefs.SetString("BTStatus", "<color=red><b>Appareil déconnecté</color>");
						});
						break;

					case States.RequestMTU:
						HM10_Status.color = Color.magenta;

						BluetoothLEHardwareInterface.RequestMtu(_hm10, 185, (address, newMTU) =>
						{
							HM10_Status.color = new Vector4(0.6f, 0, 1, 1);

							SetState(States.Subscribe, 0.1f);
						});
						break;

					case States.Subscribe:
						HM10_Status.color = new Vector4(0, 0.25f, 0, 1);

						BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_hm10, ServiceUUID, Characteristic, null, (address, characteristicUUID, bytes) => {

							HM10_Status.color = new Vector4(0, 0.5f, 0, 1);
						});

						// set to the none state and the user can start sending and receiving data
						_state = States.None;
						HM10_Status.color = Color.green;
						PlayerPrefs.SetString("BTStatus", "<color=green><b>Connecté</color>");
						PlayerPrefs.SetString("_hm10", _hm10);
						PlayerPrefs.SetString("ServiceUUID", ServiceUUID);
						PlayerPrefs.SetString("Characteristic", Characteristic);
						PlayerPrefs.SetInt("Connected", 1);
						SendByte(66);
						SendByte(255);
						break;

					case States.Unsubscribe:
						BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_hm10, ServiceUUID, Characteristic, null);
						SetState(States.Disconnect, 4f);
						break;

					case States.Disconnect:
						if (_connected)
						{
							BluetoothLEHardwareInterface.DisconnectPeripheral(_hm10, (address) => {
								BluetoothLEHardwareInterface.DeInitialize(() => {
									PlayerPrefs.SetString("BTStatus", "<color=red><b>Appareil déconnecté</color>");
									_connected = false;
									_state = States.None;
								});
							});
						}
						else
						{
							BluetoothLEHardwareInterface.DeInitialize(() => {

								_state = States.None;
							});
						}
						break;
				}
			}
		}
	}

	string FullUUID(string uuid)
	{
		return "0000" + uuid + "-0000-1000-8000-00805F9B34FB";
	}

	bool IsEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
			uuid1 = FullUUID(uuid1);
		if (uuid2.Length == 4)
			uuid2 = FullUUID(uuid2);

		return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
	}

	void SendString(string value)
	{
		var data = Encoding.UTF8.GetBytes(value);
		// notice that the 6th parameter is false. this is because the HM10 doesn't support withResponse writing to its characteristic.
		// some devices do support this setting and it is prefered when they do so that you can know for sure the data was received by 
		// the device
		BluetoothLEHardwareInterface.WriteCharacteristic(_hm10, ServiceUUID, Characteristic, data, data.Length, false, (characteristicUUID) => {

			BluetoothLEHardwareInterface.Log("Write Succeeded");
		});
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
}
