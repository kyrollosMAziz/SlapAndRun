using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField]
    Joystick m_joystick;
    [SerializeField]
    Player m_Player;

    public delegate void OnJoysticMove();
    public OnJoysticMove onJoysticMove;

    private void Awake()
    {
        onJoysticMove += SetPlayerDestination;
    }
    void SetPlayerDestination() 
    {
        m_Player.Direction = m_joystick.GetPlayerTarget();
        m_Player.Horizontal = m_joystick.Horizontal;
        m_Player.Vertical = m_joystick.Vertical;
    }
}
