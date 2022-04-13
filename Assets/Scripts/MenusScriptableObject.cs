using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu", menuName = "ScriptableObjects", order = 1)]
public class MenusScriptableObject : ScriptableObject
{
    public string nom;
    public Sprite icon;
    public bool activation;
}
