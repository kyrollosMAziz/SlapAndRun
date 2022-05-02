using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance { get => m_Instance; }

    private UserInput m_UserInput;
    public UserInput UserInput
    {
        get
        {
            if (m_UserInput == null)
            {
                m_UserInput = !gameObject.GetComponent<UserInput>() ? gameObject.AddComponent<UserInput>() : gameObject.GetComponent<UserInput>();
            }
            return m_UserInput;
        }
    }

    private SceneController m_sceneController;
    public SceneController SceneController
    {
        get
        {
            if (!m_sceneController)
            {
                m_sceneController = GetComponent<SceneController>() ? GetComponent<SceneController>() : gameObject.AddComponent<SceneController>();
            }
            return m_sceneController;
        }
    }

    private void Awake()
    {
        m_sceneController = GetComponent<SceneController>() ? GetComponent<SceneController>() : gameObject.AddComponent<SceneController>();
        m_Instance = this;
    }
}
