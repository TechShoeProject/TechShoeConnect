using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DangerositeWriterScript : MonoBehaviour
{
	[SerializeField] Slider value;
	[SerializeField] TextMeshProUGUI distance, variation;
	GameObject holder;
	private void Awake()
	{
		holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(2);
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(2);
		holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
		value.maxValue = 255;
    }
    private void Update()
    {
		value.value = PlayerPrefs.GetInt("Dangerosite");
		distance.text = "Distance avec l'objet le plus proche :\n<color=red>" + PlayerPrefs.GetInt("Distance").ToString() + "cm</color>";
		variation.text = "Variation de distance avec l'objet le plus proche :\n<color=red>" + PlayerPrefs.GetInt("Variation").ToString() + "cm/s</color>";
	}
}
