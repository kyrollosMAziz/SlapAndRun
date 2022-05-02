using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RagdollManager : MonoBehaviour
{
    struct NPCRagdoll
    {
        public string name;
        public Rigidbody body;
        public CharacterJoint characterJoint;
    }

    List<NPCRagdoll> ragDolls = new List<NPCRagdoll>();

    private void Awake()
    {
        //var npcs = GameObject.FindObjectsOfType<CharacterJoint>().ToList();
        //npcs.ForEach(npc =>
        //{
        //    ragDolls.Add(new NPCRagdoll
        //    {
        //        name = npc.transform.root.name,
        //        body = npc.GetComponent<Rigidbody>(),
        //        characterJoint = npc.GetComponent<CharacterJoint>()
        //    });
        //    npc.GetComponent<Rigidbody>().isKinematic = true;
        //}
        //);
        
    }

    public void ActivateRagdoll(Character npcToKill) 
    {
        npcToKill.GetAnimator().enabled = false;
        npcToKill.GetComponent<NavMeshAgent>().speed = 0;
        var ragdollToActivate = ragDolls.FindAll(n => n.name == npcToKill.name).ToList();
        ragdollToActivate.ForEach(r => r.body.isKinematic = false);
    }
}
