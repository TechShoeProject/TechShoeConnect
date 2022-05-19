using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainButtonScript : MonoBehaviour
{
    string _hm10, ServiceUUID, Characteristic, goTo;
    List<string> searchWords = new List<string>();
    [SerializeField] TMP_InputField searchField;
    bool firstClick = true;
    private void Awake()
    {
        searchWords.Add("Paramètre");
        searchWords.Add("Détection d'obstacles");
        searchWords.Add("Détection sonore");
        searchWords.Add("GPS");
        searchWords.Add("Vibrations obstacles");
        searchWords.Add("Vibrations sonores");
        searchWords.Add("Vibrations GPS");
        searchWords.Add("Police");
        searchWords.Add("Thème");
        searchWords.Add("Accessibilité");
        searchWords.Add("Batterie");
        searchWords.Add("Pas");
        searchWords.Add("Débug");
    }

    public void ResetSearch()
    {

    }

    public void SearchWord()
    {
        if (firstClick)
        {
            goTo = string.Empty;
            for (int j = 0; j < 20; j++)
                foreach (string g in searchWords)
                {
                    if (g.Length > j)
                        if (searchField.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Substring(0, searchField.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Length - 1).Equals(g.Substring(0, g.Length - j), System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            goTo = g;
                            break;
                        }
                }
            if (!string.IsNullOrEmpty(goTo))
            {
                searchField.text = goTo;
                firstClick = false;
            }
            else
            {
                searchField.text = "";
            }
        }
        else
        {
            gameObject.GetComponent<UltimateRedirector>().Redirector(goTo switch
            {
                "Paramètre" => 17,
                "Détection d'obstacles" => 3,
                "Détection sonore" => 7,
                "GPS" => 11,
                "Vibrations obstacles" => 5,
                "Vibrations sonores" => 9,
                "Vibrations GPS" => 14,
                "Police" => 19,
                "Thème" => 18,
                "Batterie" => 15,
                "Pas" => 16,
                "Débug" => 20,
                _ => 1
            });
        }
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

	public void Vibrate()
    {
        SendByte(2);
        SendByte(0);
    }

	public void QuitApp()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
