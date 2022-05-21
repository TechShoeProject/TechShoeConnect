using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VibrationGPS : MonoBehaviour
{
    [SerializeField] TMP_Dropdown type, maneuvre;
    GameObject holder;
    bool skippedFrame = false;

    private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
        maneuvre.value = PlayerPrefs.GetInt("ActualManeuverGPSVib", 0);
        ReloadDropdowns();
    }

    public void ReloadDropdowns()
    {
        skippedFrame = true;
        StartCoroutine(SkipFrame());
        type.value = PlayerPrefs.GetInt("VibrationGPS" + maneuvre.value.ToString(), 0);
        PlayerPrefs.SetInt("ActualManeuverGPSVib", maneuvre.value);
    }

    IEnumerator SkipFrame()
    {
        yield return new WaitForSeconds(0.1f);
        skippedFrame = false;
    }

    public void SendDataVibationsGPS()
    {
        if (!skippedFrame)
        {
            holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(3);
            holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(2);
            holder.GetComponent<BluetoothWriterScript>().DataToSend.Add((byte)maneuvre.value);
            holder.GetComponent<BluetoothWriterScript>().DataToSend.Add((byte)type.value);
            PlayerPrefs.SetInt("VibrationGPS" + maneuvre.value.ToString(), type.value);
        }
    }
}
