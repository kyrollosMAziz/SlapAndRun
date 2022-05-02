using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int m_Speed;
    public int m_Level;

    public Animator anim;
    public TextMeshProUGUI levelIndicator;
    private Vector3 m_Direction;
    public Vector3 Direction { get => m_Direction; set => m_Direction = value; }

    public virtual void Awake()
    {
        if (levelIndicator)
            levelIndicator.text = " LEVEL " + m_Level.ToString();
    }
    public virtual void Move()
    {
        transform.Translate(transform.forward * m_Direction.y * m_Speed * Time.deltaTime);
        transform.Translate(transform.right * m_Direction.x * m_Speed * Time.deltaTime);
        Debug.LogWarning(transform.forward * m_Direction.y);
    }
    public virtual void Move(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, m_Speed * Time.deltaTime);
    }
    public virtual void Death()
    {
        anim?.SetBool("Fall",true);
        float animationLenght = anim.GetCurrentAnimatorClipInfo(0).Length;
        StartCoroutine(AfterDeath(animationLenght));
    }
    IEnumerator AfterDeath(float animLen) 
    {
        yield return new WaitForSeconds(animLen);
    }
    public virtual void Fight(Enemy challenger) 
    {
        
    }
    //public virtual void PlayerFight(Enemy challenger)
    //{
    //    m_challenger = challenger;
    //    m_challenger.GetComponent<Collider>().enabled = false;

    //    if (m_challenger.m_Level <= m_Level)
    //    {
    //        m_challenger.levelIndicator.gameObject.SetActive(false);
    //        _collider.isTrigger = true;
    //        anim?.SetTrigger("Slap");
    //        m_challenger.m_NavMeshAgent.speed = 0;
    //        if (m_challenger.GetComponent<ILose>() != null)
    //        {
    //            var lostCharacter = m_challenger.GetComponent<ILose>();
    //            lostCharacter.OnMyDeath(transform.position);
    //        }
    //        StartCoroutine(OnAnimationEnd());
    //    }
    //}
    public Animator GetAnimator()
    {
        return anim;
    }
}
