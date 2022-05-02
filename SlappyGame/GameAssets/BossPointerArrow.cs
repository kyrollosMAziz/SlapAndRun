using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPointerArrow : MonoBehaviour
{
    private Transform m_BossTransform;
    private Transform m_PlayerTransfrom;

    private void Start()
    {
        m_BossTransform = GameObject.FindObjectOfType<Boss>().transform;
        m_PlayerTransfrom = GameObject.FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        Vector3 bossDirection = Vector3.zero;
        bossDirection = m_PlayerTransfrom.eulerAngles;
        transform.rotation = Quaternion.Euler(0, 0, bossDirection.z);
    }
}
