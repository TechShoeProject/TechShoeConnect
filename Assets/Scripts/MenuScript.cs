using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] MenusScriptableObject[] menus;
    public GameObject[] menusGO;
    [SerializeField] GameObject menuPrefab;
    GameObject temp;
    [SerializeField] List<TMP_FontAsset> fontList;

    private void Awake()
    {
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/LiberationSans SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Roboto-Regular SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Bahnschrifft SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Verdana SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Autumn SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Univers SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Comic-Sans-MS SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/DejaVuSans SDF"));
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
                if (PlayerPrefs.GetInt("Aveugle") == 1)
                {
                    menusGO[i].GetComponent<Button>().interactable = false;
                    menusGO[i].GetComponent<Button>().transition = Selectable.Transition.None;
                    menusGO[i].GetComponent<Button>().targetGraphic = null;
                    GameObject.Find("Canvas").GetComponent<AccessibilityScript>().boutons.Add(menusGO[i].GetComponent<Button>());
                }
            }
            else
            {
                menusGO[i].transform.Find("Activate").GetComponent<Toggle>().isOn = SceneManager.GetActiveScene().buildIndex == 3 ? PlayerPrefs.GetInt("ObstacleActive", 1) == 1 : PlayerPrefs.GetInt("SonoreActive", 1) == 1;
                gameObject.GetComponent<ActivateAndDeactivateScript>().DetecToggle = menusGO[i].transform.Find("Activate").GetComponent<Toggle>();
                if (PlayerPrefs.GetInt("Aveugle") == 1)
                {
                    menusGO[i].GetComponent<Button>().interactable = false;
                    menusGO[i].GetComponent<Button>().transition = Selectable.Transition.None;
                    menusGO[i].GetComponent<Button>().targetGraphic = null;
                    GameObject.Find("Canvas").GetComponent<AccessibilityScript>().boutons.Add(menusGO[i].transform.Find("Activate").GetComponent<Toggle>());
                }
            }
            int i2 = i;
            menusGO[i2].GetComponent<Button>().onClick.AddListener(delegate { MenuButtonClicked(i2); });
        }
        temp = null;

        foreach(GameObject x in menusGO)
        {
            x.GetComponent<Image>().color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Button_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Button_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Button_b"));
            x.transform.Find("Name").GetComponent<TextMeshProUGUI>().color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Text_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Text_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Text_b"));
            x.transform.Find("Name").GetComponent<TextMeshProUGUI>().font = fontList[PlayerPrefs.GetInt("Police")];
            x.transform.Find("Icon").GetComponent<Image>().color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_b"));
            if (x.transform.Find("Activate").transform.Find("Background") != null)
                x.transform.Find("Activate").transform.Find("Background").GetComponent<Image>().color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Checkmark_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Checkmark_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Checkmark_b"));
            if(x.transform.Find("Activate").transform.Find("Background").transform.Find("Checkmark") != null)
                x.transform.Find("Activate").transform.Find("Background").transform.Find("Checkmark").GetComponent<Image>().color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_b"));
            
        }
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
            case "Commencer un guidage":
                SceneManager.LoadScene(12);
                break;
            case "Guidage actuel":
                SceneManager.LoadScene(13);
                break;
            case "Modification des vibrations liées au GPS":
                SceneManager.LoadScene(14);
                break;
            case "Thème":
                SceneManager.LoadScene(18);
                break;
            case "Police":
                SceneManager.LoadScene(19);
                break;
            case "Options de débug":
                SceneManager.LoadScene(20);
                break;
            case "Accessibilité":
                PlayerPrefs.SetInt("Aveugle", -1);
                SceneManager.LoadScene(21);
                break;
        }
    }
}
