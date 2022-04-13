using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoletManager : MonoBehaviour
{
    [SerializeField] Animator animatorVolet;
    [SerializeField] TextMeshProUGUI textVolet;

    public void ClickVolet()
    {
        animatorVolet.SetBool("Tirer", !animatorVolet.GetBool("Tirer"));
    }

    public void ChangeVoletState(string value)
    {
        textVolet.text = value;
    }
}
