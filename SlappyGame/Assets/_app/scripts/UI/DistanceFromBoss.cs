using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceFromBoss : MonoBehaviour
{
    Player player;
    Boss boss;
    TextMeshProUGUI text;

    private void Start()
    {
        player = GameManager.FindObjectOfType<Player>();
        boss = GameManager.FindObjectOfType<Boss>();
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        text.text = ((int)Vector3.Distance(player.transform.position,boss.transform.position)).ToString() + " Meter";
    }
}
