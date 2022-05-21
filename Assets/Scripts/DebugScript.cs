using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugScript : MonoBehaviour
{
    [SerializeField] TMP_InputField toSend;
    [SerializeField] TextMeshProUGUI received;
    GameObject holder;

    private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
    }
    public void SendData()
    {
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add((byte)Mathf.Clamp(int.Parse(toSend.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Substring(0, toSend.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Length - 1)), 0, 255));
    }

    private void Update()
    {
        received.text = PlayerPrefs.GetInt("LastData4").ToString() + " " + PlayerPrefs.GetInt("LastData3").ToString() + " " + PlayerPrefs.GetInt("LastData2").ToString() + " " + PlayerPrefs.GetInt("LastData1").ToString();
    }
}
