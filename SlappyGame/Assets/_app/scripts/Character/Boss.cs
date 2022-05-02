using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy , ILeveledUp
{
  
    private bool isWin;
    private NavMeshAgent m_navMeshAgent;

     public override void Start()
    {
        Vector3 myPos = new Vector3(GameManager.Instance.SceneController.GetTargetManager().GetTarget().position.x, 0, GameManager.Instance.SceneController.GetTargetManager().GetTarget().position.z);
        transform.position = myPos;
        m_Level = GameManager.Instance.SceneController.PlayerData.GetPlayerLevel() + Random.Range(2,5);
        if (levelIndicator)
            levelIndicator.text = " LEVEL " + m_Level.ToString();
        GameManager.Instance.SceneController.AssignToWinEvent(this);
    }
    private void FixedUpdate()
    {
        var enemiesInArea = Physics.OverlapSphere(transform.position, m_PlayerCheckingArea, m_layerMask).ToList();

        if (enemiesInArea.Count > 0)
        {
            NPCSlap(enemiesInArea.FirstOrDefault(e => e.GetComponent<Player>()).GetComponent<Player>());
        }
        if (m_Player && !isWin)
        {
            Move(new Vector3(m_Player.transform.position.x, transform.position.y, m_Player.transform.position.z));
        }
        else if(isWin)
        {
            Move();
        }
    }
    void OnDrawGizmosSelected()
    {
        //Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, m_PlayerCheckingArea);
    }

    IEnumerator KillPlayer(float animationClipLength , Character challenger) 
    {
        yield return new WaitForSeconds(animationClipLength);
        challenger.Death();
    }
    public override void Death()
    {
        base.Death();
    }

    public void OnPlayerLeveldUp()
    {
        isWin = true;
    }
}
