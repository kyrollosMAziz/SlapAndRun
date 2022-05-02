using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCharacter : Character
{
    protected Vector3 m_ScaleChange;
    protected Enemy m_challenger;
    protected int Speed = 0;
    protected Collider _collider;
    protected bool isFight;
}
