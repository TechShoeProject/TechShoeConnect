using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SonWriterScript : MonoBehaviour
{
	[SerializeField] Slider value;
	[SerializeField] TextMeshProUGUI encoded;
	GameObject holder;
	private void Awake()
	{
		holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(2);
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(1);
		value.maxValue = 255;
    }
    private void Update()
    {
		value.value = PlayerPrefs.GetInt("Son");
		encoded.text = "Valeur encodée :\n<color=red>" + PlayerPrefs.GetInt("Son") + "</color>";
	}
}
