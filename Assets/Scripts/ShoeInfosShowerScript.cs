using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShoeInfosShowerScript : MonoBehaviour
{
    [SerializeField] Animator Batterie, Pas, Connection, Chaussage;
    [SerializeField] TextMeshProUGUI connectionText, chaussageText, pasText, batterieText;
    [SerializeField] Image batterieCharge;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("BatterieInfos") == 1) Batterie.SetBool("IsSelected", true);
        if (PlayerPrefs.GetInt("PasInfos") == 1) Pas.SetBool("IsSelected", true);
        if (PlayerPrefs.GetInt("ConnectionInfos") == 1) Connection.SetBool("IsSelected", true);
        if (PlayerPrefs.GetInt("ChaussageInfos") == 1) Chaussage.SetBool("IsSelected", true);
    }

    public void SwitchBatterie()
    {
        Batterie.SetBool("IsSelected", !Batterie.GetBool("IsSelected"));
        PlayerPrefs.SetInt("BatterieInfos", Batterie.GetBool("IsSelected") ? 1 : 0);
    }

    public void SwitchPas()
    {
        Pas.SetBool("IsSelected", !Pas.GetBool("IsSelected"));
        PlayerPrefs.SetInt("PasInfos", Pas.GetBool("IsSelected") ? 1 : 0);
    }

    public void SwitchConnection()
    {
        Connection.SetBool("IsSelected", !Connection.GetBool("IsSelected"));
        PlayerPrefs.SetInt("ConnectionInfos", Connection.GetBool("IsSelected") ? 1 : 0);
    }

    public void SwitchChaussage()
    {
        Chaussage.SetBool("IsSelected", !Chaussage.GetBool("IsSelected"));
        PlayerPrefs.SetInt("ChaussageInfos", Chaussage.GetBool("IsSelected") ? 1 : 0);
    }

    private void Update()
    {
        connectionText.text = "Statut de connection :\n" + PlayerPrefs.GetString("BTStatus");
        chaussageText.text = PlayerPrefs.GetInt("ChaussageStatus") switch
        {
            0 => "Etat de chaussage :\n<color=orange><b>Non chaussée</color>",
            1 => "Etat de chaussage :\n<color=orange><b>Chaussée</color>",
            _ => "Etat de chaussage :\n<color=red><b>Aucune donnée</color>",
        };
        pasText.text = PlayerPrefs.GetInt("PasStatus") == -1 ? "Nombre de pas:\n<color=red><b>Aucune donnée</color>" : "Nombre de pas:\n<color=blue><b>" + PlayerPrefs.GetInt("PasStatus") + "</color>";
        if(PlayerPrefs.GetInt("BatterieStatus") == -1)
        {
            batterieText.gameObject.SetActive(true);
            batterieCharge.fillAmount = 0;
        }
        else
        {
            batterieText.gameObject.SetActive(false);
            batterieCharge.fillAmount = PlayerPrefs.GetInt("BatterieStatus") / 100;
        }
    }
}
