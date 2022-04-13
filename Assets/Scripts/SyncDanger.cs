using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncDanger : MonoBehaviour
{
    [SerializeField] Slider faible, moyen, haut;
    bool faibleU, moyenU, hautU;
    private void Update()
    {
        if (faible.value > moyen.value && faibleU)
        {
            moyen.value = faible.value;
        }
        if(moyen.value > haut.value && (moyenU || faibleU))
        {
            haut.value = moyen.value;
        }
        if(haut.value < moyen.value && hautU)
        {
            moyen.value = haut.value;
        }
        if(moyen.value < faible.value && (moyenU || hautU))
        {
            faible.value = moyen.value;
        }
    }

    public void Used(int x)
    {
        switch (x)
        {
            case 0:
                faibleU = true;
                break;
            case 1:
                moyenU = true;
                break;
            case 2:
                hautU = true;
                break;
            case 3:
                faibleU = false;
                break;
            case 4:
                moyenU = false;
                break;
            case 5:
                hautU = false;
                break;
        }
    }
}
