using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeController : MonoBehaviour, IPrizeGained
{
    [SerializeField] float m_Frequency = 1;
    [SerializeField] float m_Magnitude = 1;
    [SerializeField] float m_RotationSpeed = 1;

    private Vector3 m_StartPosition;
    private Animator m_Animator;

    private void Start()
    {
        m_StartPosition = transform.position;
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 mov = m_StartPosition + new Vector3(0, Mathf.Sin(m_Frequency * Time.time) * m_Magnitude, 0);
        transform.position = mov;
        transform.Rotate(Vector3.up * Time.deltaTime * m_RotationSpeed);
    }
    public void PrizeGained()
    {
        m_Animator.SetBool("Gained", true);
        StartCoroutine(OnAnimationEnd(m_Animator.GetCurrentAnimatorStateInfo(0).length));
    }
    IEnumerator OnAnimationEnd(float animationLength)
    {
        yield return new WaitForSeconds(animationLength + 0.01f);
        Destroy(gameObject);
        if (gameObject)
            gameObject.SetActive(false);
    }
}