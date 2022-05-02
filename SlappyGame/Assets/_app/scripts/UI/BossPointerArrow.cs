using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPointerArrow : MonoBehaviour
{
    private Transform m_BossPosition;

    private void Start()
    {
        m_BossPosition = GameObject.FindObjectOfType<Boss>().transform;
    }
    private void Update()
    {
        if (m_BossPosition != null)
        {
            transform.LookAt(new Vector3(m_BossPosition.position.x , 0 , m_BossPosition.position.z));
        }
    }
    /*
    private Transform m_BossTransform;
    private Transform m_PlayerTransfrom;
    private RectTransform m_RectTransform;
    private void Start()
    {
        m_BossTransform = GameObject.FindObjectOfType<Boss>().transform;
        m_PlayerTransfrom = GameObject.FindObjectOfType<Player>().transform;
        m_RectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 objScreenPos = Camera.main.WorldToScreenPoint(m_BossTransform.transform.position);

        Vector3 dir = (objScreenPos - m_RectTransform.position).normalized;

        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));

        Vector3 cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;

        m_RectTransform.localEulerAngles = new Vector3(m_RectTransform.localEulerAngles.x, m_RectTransform.localEulerAngles.y, angle);
    }*/
}
