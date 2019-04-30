using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObjectActivator : MonoBehaviour
{
    List<GameObject> DeadNPCist = new List<GameObject>();

    private void OnEnable()
    {
        FlyStuffAway();
    }

    void FlyStuffAway()
    {
        switch (GodManager.Instance.WindLevel)
        {
            case 2:
                foreach (var _npc in AIManager.Instance.NPCs)
                {
                    if (_npc.GetComponent<NPC>().CurrentLocation == NPC.Location.Outside)
                    {
                        _npc.GetComponent<NPC>().Animator.SetBool("IsFlyingAway", true);
                        DeadNPCist.Add(_npc);
                    }
                }
                break;

            case 3:
                foreach (var _npc in AIManager.Instance.NPCs)
                {
                    if (_npc.GetComponent<NPC>().CurrentLocation == NPC.Location.Outside)
                    {
                        _npc.GetComponent<NPC>().Animator.SetBool("IsFlyingAway", true);
                        DeadNPCist.Add(_npc);
                    }
                }
                break;

            case 4:
                foreach (var _house in GodManager.Instance.SmallHouses)
                {
                    if (_house.activeInHierarchy)
                        _house.GetComponent<Animator>().SetBool("IsFlyingAway", true);
                }

                foreach (var _npc in AIManager.Instance.NPCs)
                {
                    if (_npc.GetComponent<NPC>().CurrentLocation == NPC.Location.Outside)
                    {
                        _npc.GetComponent<NPC>().Animator.SetBool("IsFlyingAway", true);
                        DeadNPCist.Add(_npc);
                    }
                }
                break;
        }
        
        //DANGER, HARDCODE FOUND!
        Invoke("RemoveDeadNPCs", 4);
    }

    void RemoveDeadNPCs()
    {
        foreach (var _npc in DeadNPCist)
        {
            AIManager.Instance.RemoveNPC(_npc);
        }
    }
}
