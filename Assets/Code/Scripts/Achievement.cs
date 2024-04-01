using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievement", order = 0)]
public class Achievement : ScriptableObject
{
    public Sprite sprite;
    public string title;
    public string description;
    public bool achieved;
    public string type;
    public int goal;
}
