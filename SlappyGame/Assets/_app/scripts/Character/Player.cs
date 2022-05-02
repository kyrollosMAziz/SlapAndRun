using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : FighterCharacter, ILeveledUp
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody m_RigidBody;
    [SerializeField] private Collider m_Collider;
    [SerializeField] private int m_PlayerCheckingArea;
    [SerializeField] private float m_fallingSpeed;
    [SerializeField] private CinemachineBrain m_CinemachineBrain;
    [SerializeField] private List<Transform> m_StartingPosition;
    [SerializeField] private Transform m_PrizeEffect;
    [SerializeField] private Transform m_RightHand;
    [SerializeField] private Transform m_LefttHand;

    private float m_Vecrtical;
    public float Vertical
    {
        get => m_Vecrtical;
        set
        {
            m_Vecrtical = value;
        }
    }
    private float m_Horizontal;

    public float Horizontal
    {
        get => m_Horizontal;
        set
        {
            m_Horizontal = value;
        }
    }

    private void Awake()
    {
        base.Awake();
        m_ScaleChange = new Vector3(0.05f, 0.05f, 0.05f);
        m_Collider = GetComponent<Collider>();
    }
    private void Start()
    {
        Vector3 myPos = m_StartingPosition[UnityEngine.Random.Range(0, m_StartingPosition.Count - 1)].position;
        transform.position = new Vector3(myPos.x, 0, myPos.z);
        _collider = m_Collider;
        GameManager.Instance.SceneController.AssignToWinEvent(this);
        m_Level = GameManager.Instance.SceneController.PlayerData.GetPlayerLevel();
        if (levelIndicator)
            levelIndicator.text = " LEVEL " + m_Level.ToString();
    }
    // if you use physics , use FixedUpdate for the movement setup.
    private void FixedUpdate()
    {
        var enemiesInArea = Physics.OverlapSphere(transform.position, m_PlayerCheckingArea, layerMask).ToList();

        if (enemiesInArea.Count > 0 && !isFight)
        {
            isFight = true;

            PickFight(enemiesInArea.FirstOrDefault(e => e.GetComponent<Character>()).GetComponent<Enemy>());
        }
        else
        {
            Move();
        }
        if (levelIndicator)
        {
            levelIndicator.text = " LEVEL " + m_Level.ToString();
        }

    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Direction, m_PlayerCheckingArea);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 7:
                Debug.Log("Trapped");
                StartCoroutine(OnBeingTrapped());
                //other.gameObject.SetActive(false);
                Destroy(other.gameObject);
                break;
            case 8:
                Debug.Log("Hole");
                StartCoroutine(OnHole());
                break;
            case 9:
                Debug.Log("SpeedUp");
                StartCoroutine(OnSpeedUpCollection());
                StartCoroutine(OnPrizeGained(other));
                break;
            case 10:
                Debug.Log("LevelUp");
                LevelUp();
                StartCoroutine(OnPrizeGained(other));
                break;
        }
    }
    private void LevelUp()
    {
        m_Level++;
        levelIndicator.text = m_Level.ToString();
        GameManager.Instance.SceneController.PlayerData.SetPlayerLevel(m_Level);
    }
    public override void Move()
    {
        // we only need XZ plan and we don't have any elevation in the level so Y can be set 0 and that actually what seem 
        // fix you issue.
        m_RigidBody.velocity = new Vector3(Horizontal * m_Speed, 0/*m_RigidBody.velocity.y*/, Vertical * m_Speed);
        if (Horizontal != 0 || Vertical != 0)
        {
            // don't update if we get a zero vector.
            if (m_RigidBody.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(m_RigidBody.velocity);
                anim?.SetBool("isWalking", true);
                var pos = transform.position;
                pos.y = 0;
                transform.position = pos;
            }
        }
        else
        {
            var pos = transform.position;
            pos.y = 0;
            transform.position = pos;
            if (anim) anim.SetBool("isWalking", false);
        }
    }
    public override void Death()
    {
        base.Death();
    }
    IEnumerator OnBeingTrapped()
    {
        m_Speed = m_Speed / 4;
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("SpeedUp");
        m_Speed = m_Speed * 4;
    }
    IEnumerator OnHole()
    {
        m_Speed = 0;
        m_CinemachineBrain.enabled = false;
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.SceneController.PlayerLost();
        m_Collider.enabled = false;
        m_RigidBody.useGravity = true;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            m_RigidBody.AddForce(-transform.up * m_fallingSpeed);
        }
    }
    IEnumerator OnSpeedUpCollection()
    {
        m_Speed = m_Speed * 2;
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("SlowDownAgain");
        m_Speed = m_Speed / 2;
    }
    IEnumerator OnPrizeGained(Collider other)
    {
        other.GetComponent<IPrizeGained>()?.PrizeGained();
        m_PrizeEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        m_PrizeEffect.gameObject.SetActive(false);
    }
    public void PickFight(Enemy challenger)
    {
        if (challenger.m_Level <= m_Level)
        {
            Fight(challenger);
            m_Level++;
            GameManager.Instance.SceneController.PlayerData.SetPlayerLevel(m_Level);
        }
        if (GameManager.Instance.SceneController.Boss.m_Level < m_Level)
        {
            GameManager.Instance.SceneController.PlayerLeveledUp();
        }
    }
    public override void Fight(Enemy challenger)
    {
        m_challenger = challenger;
        m_challenger.GetComponent<Collider>().enabled = false;

        if (m_challenger.m_Level <= m_Level)
        {
            m_challenger.levelIndicator.gameObject.SetActive(false);
            _collider.isTrigger = true;

            anim?.SetBool("Slap", true);

            m_challenger.m_NavMeshAgent.speed = 0;
            m_challenger.Death();
            StartCoroutine(OnAnimationEnd());
        }
    }
    IEnumerator OnAnimationEnd()
    {
        yield return new WaitForSeconds(0.2f);
        anim?.SetBool("Slap", false);
        AfterSlap();
    }

    public void AfterSlap()
    {
        isFight = false;
        _collider.isTrigger = false;
        transform.localScale += m_ScaleChange;
    }
    public void OnPlayerLeveldUp()
    {
        //m_Speed = 0;
    }

}
