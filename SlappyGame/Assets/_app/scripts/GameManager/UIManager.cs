using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, ILeveledUp, ILose
{
    [SerializeField] CanvasGroup m_MainCanvas;
    [SerializeField] CanvasGroup m_WinCanvas;
    [SerializeField] CanvasGroup m_LoseCanvas;
    [SerializeField] CanvasGroup m_LeveldUpCanvas;

    private void Start()
    {
        GameManager.Instance.SceneController.AssignToLoseEvent(this);
        GameManager.Instance.SceneController.AssignToWinEvent(this);
    }

    public void OnPlayerLose()
    {
        RemoveMainCanvas();
        m_LoseCanvas.alpha = 1;
        m_LoseCanvas.blocksRaycasts = true;
        m_LoseCanvas.interactable = true;
    }
    public void OnPlayerLeveldUp()
    {
        m_LeveldUpCanvas.alpha = 1;
        m_LeveldUpCanvas.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OnPlayerWin()
    {
        RemoveMainCanvas();
        m_WinCanvas.alpha = 1;
        m_WinCanvas.interactable = true;
        m_WinCanvas.blocksRaycasts = true;
    }
    private void RemoveMainCanvas() 
    {
        m_MainCanvas.alpha = 0;
        m_MainCanvas.interactable = false;
        m_MainCanvas.blocksRaycasts = false;
    }

    public void OnMyDeath(Vector3 froceDirection)
    {

    }
}
