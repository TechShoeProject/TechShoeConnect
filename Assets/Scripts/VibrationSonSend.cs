using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VibrationSonSend : MonoBehaviour
{
    [SerializeField] Slider value;
    [SerializeField] TMP_Dropdown type;
    GameObject holder;

	private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
        value.value = PlayerPrefs.GetFloat("VibrationS", 0.3f);
        type.value = PlayerPrefs.GetInt("VibrationST", 0);
    }


    public void SendDataVibationsSon()
    {
        float valeur = value.value;
        byte toSend = (byte)(int)(valeur * 255);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(3);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(1);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(toSend);
        PlayerPrefs.SetFloat("VibrationS", value.value);
    }

    public void SendDataVibrationSonType()
    {
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(3);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(1);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(1);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add((byte)type.value);
        PlayerPrefs.SetInt("VibrationST", type.value);
    }
}
