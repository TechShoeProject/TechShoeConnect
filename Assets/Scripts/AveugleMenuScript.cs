using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AveugleMenuScript : MonoBehaviour
{
    [SerializeField] Image HM10StatusSetter;
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().HM10_Status = HM10StatusSetter;
        if (PlayerPrefs.GetInt("Connected") == 1) GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().HM10_Status.color = Color.green;
        else GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().HM10_Status.color = Color.yellow;
        if (PlayerPrefs.GetInt("Connected") == 0 && !GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().Initialized) GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().Initialize();
    }
}
