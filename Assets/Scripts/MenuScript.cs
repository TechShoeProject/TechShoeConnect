using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] MenusScriptableObject[] menus;
    GameObject[] menusGO;
    [SerializeField] GameObject menuPrefab;
    GameObject temp;

    private void Awake()
    {
        menusGO = new GameObject[menus.Length];
        for (int i = 0; i < menus.Length; i++)
        {
            temp = Instantiate(menuPrefab, gameObject.transform);
            menusGO[i] = temp;
            menusGO[i].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = menus[i].nom;
            menusGO[i].transform.Find("Icon").GetComponent<Image>().sprite = menus[i].icon;
            if (menus[i].icon == null)
            {
                menusGO[i].transform.Find("Icon").gameObject.SetActive(false);
            }
            if (!menus[i].activation)
            {
                menusGO[i].transform.Find("Activate").gameObject.SetActive(false);
            }
            int i2 = i;
            menusGO[i2].GetComponent<Button>().onClick.AddListener(delegate { MenuButtonClicked(i2); });
        }
        temp = null;
    }

    public void MenuButtonClicked(int x)
    {
        switch (menusGO[x].transform.Find("Name").GetComponent<TextMeshProUGUI>().text)
        {
            case "Affichage de la dangerosité en temps réel":
                SceneManager.LoadScene(4);
                break;
            case "Modification des vibrations":
                SceneManager.LoadScene(5);
                break;
            case "Statistiques":
                SceneManager.LoadScene(6);
                break;
            case "Affichage du niveau sonore en temps réel":
                SceneManager.LoadScene(8);
                break;
            case "Modification des vibrations liées au son":
                SceneManager.LoadScene(9);
                break;
            case "Statistiques sonores":
                SceneManager.LoadScene(10);
                break;
        }
    }
}
