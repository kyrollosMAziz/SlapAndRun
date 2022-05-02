using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : FighterCharacter
{
    public int m_PlayerCheckingArea;
    public LayerMask m_layerMask;
    public GameObject m_spine;
    public NavMeshAgent m_NavMeshAgent;

    protected Player m_Player;

    [SerializeField]
    private Vector3 target;
    private bool _isCheckingDistanceToPlayer = false;

    public override void Awake()
    {
        base.Awake();
        gameObject.layer = 6;
        m_Player = GameObject.FindObjectOfType<Player>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }
    public virtual void Start()
    {
        m_PlayerCheckingArea = 12;
        m_layerMask = 3;
        m_Level = GameManager.Instance.SceneController.PlayerData.GetPlayerLevel() + Random.Range(0, 2);
        if (levelIndicator)
            levelIndicator.text = " LEVEL " + m_Level.ToString();

        target = GameManager.Instance.SceneController.GetTargetManager().GetTarget().position;
    }
    private void FixedUpdate()
    {
        var enemiesInArea = Physics.OverlapSphere(transform.position, m_PlayerCheckingArea).ToList();
        if (enemiesInArea.Count > 0 && enemiesInArea.Any(e => e.GetComponent<Player>()))
        {
            NPCSlap(enemiesInArea.FirstOrDefault(e => e.GetComponent<Player>()).GetComponent<Player>());
        }
        Move();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, m_PlayerCheckingArea);
    }
    public override void Move()
    {
        if (m_Player && Vector3.Distance(transform.position, target) > 50 && (Vector3.Distance(transform.position, m_Player.transform.position) > 50 || _isCheckingDistanceToPlayer))
        {
            m_NavMeshAgent.SetDestination(target);
        }
        else
        {
            StartCoroutine(IsCheckingDistnaceToPlayer());
            target = GameManager.Instance.SceneController.GetTargetManager().GetTarget().position;
        }
    }
    IEnumerator IsCheckingDistnaceToPlayer()
    {
        _isCheckingDistanceToPlayer = true;
        yield return new WaitForSeconds(2);
        _isCheckingDistanceToPlayer = false;
    }
    public override void Move(Vector3 target)
    {
        m_NavMeshAgent.SetDestination(new Vector3(target.x, 0, target.z));
    }
    IEnumerator KillPlayer(float animationClipLength, Character challenger)
    {
        yield return new WaitForSeconds(animationClipLength);
        challenger.Death();
    }

    IEnumerator PushMe(Rigidbody spineRb, Vector3 forceDirection)
    {
        float pushingTime = 1;
        while (pushingTime != 0)
        {
            spineRb.AddForce(forceDirection * 2);
            yield return new WaitForEndOfFrame();
            pushingTime -= 0.1f;
        }
    }
    public virtual void NPCSlap(Player player)
    {
        if (player.m_Level < m_Level)
        {
            player.m_Speed = 0;
            GameManager.Instance.SceneController.PlayerLost();
        }
    }
    public Transform testPusher;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
        }
    }
}
