using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour , ILeveledUp
{
    private Boss boss;

    [SerializeField] 
    private Transform cameraPositionForBoss;

    private bool isWin;
    public bool IsWin 
    {
        get 
        {
            return isWin;
        }
        set 
        {
            if (value == true)
            {
                StartCoroutine(LookAtEnemy());
            }
            isWin = value;
        }
    }

    void Start()
    {
        boss = GameObject.FindObjectOfType<Boss>();
        if (boss != null) 
        {
            GameManager.Instance.SceneController.AssignToWinEvent(this);
        }
    }
    IEnumerator LookAtEnemy() 
    {
        float lookTime = 1;
        while (lookTime>0.5f)
        {
            lookTime -= Time.deltaTime;
            transform.LookAt(boss.transform.position);
        }
        IsWin = false;
        yield return null;

    }
    public void OnPlayerLeveldUp()
    {
        IsWin = true;
    }
}
