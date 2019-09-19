using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "Event", menuName = "Event", order = 5)]
public class ScriptableEvent : ScriptableObject
{
    public PlayableAsset playable;
    public TextAsset dialog;
}
