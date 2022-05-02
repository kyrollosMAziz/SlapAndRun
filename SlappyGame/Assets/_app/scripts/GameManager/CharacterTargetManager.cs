using System.Collections.Generic;
using UnityEngine;

public class CharacterTargetManager : MonoBehaviour
{
    List<Transform> targets = new List<Transform>(); 
    private void Awake()
    {
        foreach (Transform child in transform) 
        {
            targets.Add(child);
        }
    }
    public Transform GetTarget() 
    {
        return targets[Random.Range(0,targets.Count-1)];
    }
}
