using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILose 
{
    public void OnPlayerLose();
    public void OnMyDeath(Vector3 froceDirection);
}
