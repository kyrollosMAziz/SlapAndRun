using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region PlayerCharacter
    private List<ILeveledUp> m_WinAssignees = new List<ILeveledUp>();
    private List<ILose> m_LoseAssignees = new List<ILose>();
    private UserInput m_UserInput;
    
    private PlayerData playerData = new PlayerData();
    public PlayerData PlayerData
    {
        get => playerData;
        set => playerData = value;
    }
    #endregion

    #region Boss
    private Boss boss;
    public Boss Boss 
    {
        get => boss;
    }
    #endregion

    #region GameManagers
    [SerializeField]
    private CharacterTargetManager targetManager;
    
    public UIManager m_UIManager;
    public RagdollManager m_RollManager;
    #endregion

    private void Awake()
    {
        m_RollManager = GetComponent<RagdollManager>();
    }
    private void Start()
    {
        boss = GameObject.FindObjectOfType<Boss>();
        m_UserInput = GetComponent<UserInput>() ? GetComponent<UserInput>() : gameObject.AddComponent<UserInput>();
    }
    public void AssignToLoseEvent(ILose loseAssignee)
    {
        m_LoseAssignees.Add(loseAssignee);
    }
    public void AssignToWinEvent(ILeveledUp winAssignee)
    {
        m_WinAssignees.Add(winAssignee);
    }
    public void PlayerLeveledUp()
    {
        m_WinAssignees.ForEach(w => w.OnPlayerLeveldUp());
    }
    public void PlayerLost() 
    {
        m_LoseAssignees.ForEach(w => w.OnPlayerLose());
    }
    public CharacterTargetManager GetTargetManager()
    {
        return targetManager;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
