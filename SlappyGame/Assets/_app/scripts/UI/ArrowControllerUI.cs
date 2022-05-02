using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControllerUI : MonoBehaviour
{
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = Camera.main; 
    }
    void Update()
    {
        transform.LookAt(m_Camera.transform.position);
    }
}
