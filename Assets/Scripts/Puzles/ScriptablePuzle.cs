using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle", order = 2)]
public class ScriptablePuzle : ScriptableObject
{
    public int id;
    public string nombre;
    public List<ScriptableObject> objetives;

    public bool ordered;
}

