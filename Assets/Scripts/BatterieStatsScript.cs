using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatterieStatsScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI percents;
    private void Update()
    {
        percents.text = PlayerPrefs.GetInt("BatterieStatus") == -1 ? "N/A %" : PlayerPrefs.GetInt("BatterieStatus").ToString() + " %";
    }
}
