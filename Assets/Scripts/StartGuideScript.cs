using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGuideScript : MonoBehaviour
{
    [SerializeField] GameObject[] Adresses;
    float timeElapsedPressingD = 0, timeElapsedPressingT = 0;
    [SerializeField] GameObject goDomicile, goTravail;
    [SerializeField] TextMeshProUGUI adresseDomicile, adresseTravail, adresseCustom;
    bool pressingD = false, pressingT = false;

    void ReloadAdresses()
    {
        Adresses[0].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("AdresseDomicile", "N/A");
        Adresses[1].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("AdresseTravail", "N/A");
        Adresses[2].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("AdresseRecente1", "N/A");
        Adresses[3].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("AdresseRecente2", "N/A");
        Adresses[4].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("AdresseRecente3", "N/A");
        Adresses[5].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("AdresseRecente4", "N/A");
        Adresses[6].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("AdresseRecente5", "N/A");
    }
    private void Awake()
    {
        ReloadAdresses();
    }
    public void StartGPSModule(int adresse)
    {
        if(adresse == 0)
        {
            PlayerPrefs.SetString("Adresse", PlayerPrefs.GetString("AdresseDomicile"));
        }else if (adresse == 1)
        {
            PlayerPrefs.SetString("Adresse", PlayerPrefs.GetString("AdresseTravail"));
        }
        else if(adresse == 2)
        {
            PlayerPrefs.SetString("Adresse", PlayerPrefs.GetString("AdresseRecente1"));
        }
        else if (adresse == 3)
        {
            PlayerPrefs.SetString("Adresse", PlayerPrefs.GetString("AdresseRecente2"));
        }
        else if (adresse == 4)
        {
            PlayerPrefs.SetString("Adresse", PlayerPrefs.GetString("AdresseRecente3"));
        }
        else if (adresse == 5)
        {
            PlayerPrefs.SetString("Adresse", PlayerPrefs.GetString("AdresseRecente4"));
        }
        else if (adresse == 6)
        {
            PlayerPrefs.SetString("Adresse", PlayerPrefs.GetString("AdresseRecente5"));
        }
        else if (adresse == 7)
        {
            PlayerPrefs.SetString("Adresse", adresseCustom.text);
        }
        SceneManager.LoadScene(13);
    }
    public void Pressing(int type)
    {
        if (type == 0)
            pressingD = true;
        else
            pressingT = true;
        StartCoroutine(Press(type));
    }
    public void UnPressing(int type)
    {
        if (type == 0)
        {
            pressingD = false;
        }
        else
        {
            pressingT = false;
        }
    }

    public void SaveAdress(int type)
    {
        if (type == 0)
        {
            Adresses[0].SetActive(true);
            goDomicile.SetActive(false);
            PlayerPrefs.SetString("AdresseDomicile", adresseDomicile.text);
            Debug.Log(PlayerPrefs.GetString("AdresseDomicile"));
            goDomicile.GetComponentInParent<Button>().interactable = true;
            ReloadAdresses();
        }
        else if (type == 1)
        {
            Adresses[1].SetActive(true);
            goTravail.SetActive(false);
            PlayerPrefs.SetString("AdresseTravail", adresseTravail.text);
            goTravail.GetComponentInParent<Button>().interactable = true;
            ReloadAdresses();
        }
    }

    IEnumerator Press(int type)
    {
        if(type == 0)
        {
            while (timeElapsedPressingD < 2 && pressingD)
            {
                yield return null;
                timeElapsedPressingD += Time.deltaTime;
            }
            if (timeElapsedPressingD >= 2)
            {
                Adresses[0].SetActive(false);
                goDomicile.SetActive(true);
                goDomicile.GetComponentInParent<Button>().interactable = false;
            }
            timeElapsedPressingD = 0;
        }
        else if(type == 1)
        {
            while (timeElapsedPressingT < 2 && pressingT)
            {
                yield return null;
                timeElapsedPressingT += Time.deltaTime;
            }
            if (timeElapsedPressingT >= 2)
            {
                Adresses[1].SetActive(false);
                goTravail.SetActive(true);
                goTravail.GetComponentInParent<Button>().interactable = false;
            }
            timeElapsedPressingT = 0;
        }
    }
}
