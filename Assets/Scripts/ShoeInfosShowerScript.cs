using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShoeInfosShowerScript : MonoBehaviour
{
    [SerializeField] Animator Batterie, Pas, Connection, Chaussage;
    [SerializeField] TextMeshProUGUI connectionText;
    
    public void SwitchBatterie()
    {
        Batterie.SetBool("IsSelected", !Batterie.GetBool("IsSelected"));
    }

    public void SwitchPas()
    {
        Pas.SetBool("IsSelected", !Pas.GetBool("IsSelected"));
    }

    public void SwitchConnection()
    {
        Connection.SetBool("IsSelected", !Connection.GetBool("IsSelected"));
    }

    public void SwitchChaussage()
    {
        Chaussage.SetBool("IsSelected", !Chaussage.GetBool("IsSelected"));
    }

    private void Update()
    {
        connectionText.text = "Statut de connection :\n" + PlayerPrefs.GetString("BTStatus");
    }
}
