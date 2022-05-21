using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasStatsScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI today, yesterday;
    private void Update()
    {
        today.text = PlayerPrefs.GetInt("PasStatus") == -1 ? "N/A" : PlayerPrefs.GetInt("PasStatus").ToString();
        yesterday.text = PlayerPrefs.GetInt("PasStatusYesterday", -1) == -1 ? "N/A" : PlayerPrefs.GetInt("PasStatusYesterday").ToString();
    }
}
