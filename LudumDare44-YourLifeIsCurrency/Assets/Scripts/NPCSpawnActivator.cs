using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnActivator : MonoBehaviour
{
    private void OnEnable()
    {
        AIManager.Instance.SpawnNPCs(15, 3);
    }

}
