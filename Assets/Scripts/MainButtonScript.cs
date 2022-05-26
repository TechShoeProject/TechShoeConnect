using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainButtonScript : MonoBehaviour
{
    string goTo;
    List<string> searchWords = new List<string>();
    [SerializeField] TMP_InputField searchField;
    [SerializeField] Image HM10StatusSetter;
    bool firstClick = true;
    GameObject holder;
    private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
        GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().HM10_Status = HM10StatusSetter;
        if (PlayerPrefs.GetInt("Connected") == 1) GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().HM10_Status.color = Color.green;
        else GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().HM10_Status.color = Color.yellow;
        if (PlayerPrefs.GetInt("Connected") == 0 && !GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().Initialized) GameObject.FindGameObjectWithTag("MultiScriptHolder").GetComponent<HM10Connect>().Initialize();

        searchWords.Add("Paramètres");
        searchWords.Add("Détection d'obstacles");
        searchWords.Add("Obstacles");
        searchWords.Add("Détection sonore");
        searchWords.Add("Sonore");
        searchWords.Add("GPS");
        searchWords.Add("Vibrations obstacles");
        searchWords.Add("Vibrations sonores");
        searchWords.Add("Vibrations GPS");
        searchWords.Add("Police");
        searchWords.Add("Thème");
        searchWords.Add("Accessibilité");
        searchWords.Add("Batterie");
        searchWords.Add("Pas");
        searchWords.Add("Débug");
    }

    public void ResetSearch()
    {

    }

    public void SearchWord()
    {
        if (firstClick)
        {
            int k = -1;
            goTo = string.Empty;
            for (int j = 0; j < 20; j++)
                foreach (string g in searchWords)
                {
                    k = j;
                    if (g.Length > j)
                        if (searchField.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Substring(0, searchField.transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>().text.Length - 1).Equals(g.Substring(0, g.Length - j), System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            goTo = g;
                            goto MotTrouve;
                        }
                }
            MotTrouve:
            if (k != 0)
            {
                if (!string.IsNullOrEmpty(goTo))
                {
                    searchField.text = goTo;
                    firstClick = false;
                }
                else
                {
                    searchField.text = "";
                }
            }
            else
            {
                gameObject.GetComponent<UltimateRedirector>().Redirector(goTo switch
                {
                    "Paramètre" => 17,
                    "Détection d'obstacles" => 3,
                    "Obstacles" => 3,
                    "Détection sonore" => 7,
                    "Sonore" => 7,
                    "GPS" => 11,
                    "Vibrations obstacles" => 5,
                    "Vibrations sonores" => 9,
                    "Vibrations GPS" => 14,
                    "Police" => 19,
                    "Thème" => 18,
                    "Accessibilité" => 21,
                    "Batterie" => 15,
                    "Pas" => 16,
                    "Débug" => 20,
                    _ => 1
                });
            }
        }
        else
        {
            gameObject.GetComponent<UltimateRedirector>().Redirector(goTo switch
            {
                "Paramètre" => 17,
                "Détection d'obstacles" => 3,
                "Obstacles" => 3,
                "Détection sonore" => 7,
                "Sonore" => 7,
                "GPS" => 11,
                "Vibrations obstacles" => 5,
                "Vibrations sonores" => 9,
                "Vibrations GPS" => 14,
                "Police" => 19,
                "Thème" => 18,
                "Accessibilité" => 21,
                "Batterie" => 15,
                "Pas" => 16,
                "Débug" => 20,
                _ => 1
            });
        }
    }


	public void Vibrate()
    {
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(2);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
        holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
    }

	public void QuitApp()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
