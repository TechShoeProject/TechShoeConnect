using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AccessibilityScript : MonoBehaviour
{
    GameObject holder;
    public List<Component> boutons = new List<Component>();
    [SerializeField] List<AudioClip> audioC = new List<AudioClip>();
    int selected = 0;

    private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
        if (PlayerPrefs.GetInt("Aveugle", -1) != 0)
        {
            foreach (Component x in boutons)
            {
                if (x is Slider)
                {
                    x.gameObject.GetComponent<Slider>().interactable = false;
                }
            }
            gameObject.AddComponent<AudioSource>();
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioC[0]);
        }
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("Aveugle", -1) != 0)
        {
            if (holder.GetComponent<SwipeDetector>().SwipeDown)
            {
                selected = selected + 1 < boutons.Count ? selected + 1 : 0;
                gameObject.GetComponent<AudioSource>().PlayOneShot(audioC[selected]);
            }
            if (holder.GetComponent<SwipeDetector>().SwipeUp)
            {
                if (boutons[selected] is Button)
                {
                    if (boutons[selected].gameObject.name == "Menu(Clone)")
                    {
                        int getIndex(GameObject[] list, GameObject item)
                        {
                            for (int i = 0; i < list.Length; i++)
                            {
                                if (list[i] == item) return i;
                            }
                            return -1;
                        }
                        gameObject.transform.Find("Menu").GetComponent<MenuScript>().MenuButtonClicked(getIndex(gameObject.transform.Find("Menu").GetComponent<MenuScript>().menusGO, boutons[selected].gameObject));
                    }
                    boutons[selected].gameObject.GetComponent<Button>().onClick.Invoke();
                }
                else if (boutons[selected] is Toggle && boutons[selected].gameObject.GetComponent<Toggle>().interactable)
                {
                    boutons[selected].gameObject.GetComponent<Toggle>().isOn = !boutons[selected].gameObject.GetComponent<Toggle>().isOn;
                }
                else if(boutons[selected] is TMP_InputField)
                {
                    boutons[selected].gameObject.GetComponent<TMP_InputField>().Select();
                    boutons[selected].gameObject.GetComponent<TMP_InputField>().ActivateInputField();
                }else if(boutons[selected] is TMP_Dropdown)
                {
                    boutons[selected].gameObject.GetComponent<TMP_Dropdown>().value = boutons[selected].gameObject.GetComponent<TMP_Dropdown>().value + 1 < boutons[selected].gameObject.GetComponent<TMP_Dropdown>().options.Count ? boutons[selected].gameObject.GetComponent<TMP_Dropdown>().value + 1 : 0;
                }
            }
            if((holder.GetComponent<SwipeDetector>().SwipeLeft || holder.GetComponent<SwipeDetector>().SwipeRight) && boutons[selected] is Slider)
            {
                if (holder.GetComponent<SwipeDetector>().SwipeLeft)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 5)
                    {
                        GameObject.Find("Canvas").GetComponent<SyncDanger>().Used(selected - 1);
                        StartCoroutine(WaitBeforeDeuse(selected + 2));
                    }
                    boutons[selected].gameObject.GetComponent<Slider>().value = Mathf.Clamp(boutons[selected].gameObject.GetComponent<Slider>().value - (boutons[selected].gameObject.GetComponent<Slider>().maxValue / 20), boutons[selected].gameObject.GetComponent<Slider>().minValue, boutons[selected].gameObject.GetComponent<Slider>().maxValue);
                }else if (holder.GetComponent<SwipeDetector>().SwipeRight)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 5)
                    {
                        GameObject.Find("Canvas").GetComponent<SyncDanger>().Used(selected - 1);
                        StartCoroutine(WaitBeforeDeuse(selected + 2));
                    }
                    boutons[selected].gameObject.GetComponent<Slider>().value = Mathf.Clamp(boutons[selected].gameObject.GetComponent<Slider>().value + (boutons[selected].gameObject.GetComponent<Slider>().maxValue / 20), boutons[selected].gameObject.GetComponent<Slider>().minValue, boutons[selected].gameObject.GetComponent<Slider>().maxValue);
                }
            }
        }
    }

    IEnumerator WaitBeforeDeuse(int index)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("Canvas").GetComponent<SyncDanger>().Used(index);
        GameObject.Find("Canvas").GetComponent<SyncDanger>().SendDataVibationsDanger(selected);
    }
}
