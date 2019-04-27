using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    public List<GameObject> NPCs = new List<GameObject>();

    public GameObject WellParent;
    List<Transform> standingPoints = new List<Transform>();

    Coroutine cullingRoutine;

    private void Awake()
    {
        Instance = this;
        //NPCs[0].GetComponent<NPC>().SetNavmeshTarget(WellParent.transform.position);

        foreach (Transform _npc in transform)
        {
            if (_npc.GetComponent<NavMeshAgent>() && _npc.gameObject.activeInHierarchy)
                NPCs.Add(_npc.gameObject);
        }

        foreach (Transform _point in WellParent.transform)
        {
            standingPoints.Add(_point);
        }

        SetSacrificePoints();
    }

    void SetSacrificePoints()
    {
        foreach (var _npc in NPCs)
        {
            if (standingPoints.Count > NPCs.IndexOf(_npc) && standingPoints[NPCs.IndexOf(_npc)] != null)
                _npc.GetComponent<NPC>().StartWalkingTowardsPit(standingPoints[NPCs.IndexOf(_npc)].position, WellParent.transform);
            else
            {
                Debug.LogError("NOT ENOUGH SPACE");
            }
        }
    }

    //5 VAN DE EERSTE 24 MANNEN MOETEN SPRINGEN
    public void CheckIfAllAtDestination()
    {
        bool allThere = true;
        foreach (var _npc in NPCs)
        {
            if (!_npc.GetComponent<NPC>().AtDestination)
                allThere = false;
        }

        if (allThere)
        {
            StartTheCulling();
            Debug.Log("ALL THERE BABY");
        }
    }

    void StartTheCulling()
    {
        if (cullingRoutine != null)
            StopCoroutine(cullingRoutine);
        cullingRoutine = StartCoroutine(ICulling());
    }

    IEnumerator ICulling()
    {

        List<int> randomNumberList = new List<int>();

        for (int i = 0; i < 24; i++)
        {
            randomNumberList.Add(i);
        }

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(3f);


            int randomIndex = Random.Range(0, randomNumberList.Count);
            NPCs[randomNumberList[randomIndex]].GetComponent<NPC>().JumpInPit();
            randomNumberList.Remove(randomIndex);
        }

        yield return null;
    }
}
