using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncDanger : MonoBehaviour
{
    [SerializeField] Slider faible, moyen, haut, pop;
    bool faibleU, moyenU, hautU;
    GameObject holder;

	private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
        faible.value = PlayerPrefs.GetFloat("VibrationOFaible", 0.3f);
        moyen.value = PlayerPrefs.GetFloat("VibrationOMoyen", 0.6f);
        haut.value = PlayerPrefs.GetFloat("VibrationOHaut", 1f);
        pop.value = PlayerPrefs.GetFloat("VibrationOPop", 0.8f);
    }

	private void Update()
    {
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
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(3);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add((byte)vibType);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(toSend);
        PlayerPrefs.SetFloat("VibrationOFaible", faible.value);
        PlayerPrefs.SetFloat("VibrationOMoyen", moyen.value);
        PlayerPrefs.SetFloat("VibrationOHaut", haut.value);
        PlayerPrefs.SetFloat("VibrationOPop", pop.value);

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
