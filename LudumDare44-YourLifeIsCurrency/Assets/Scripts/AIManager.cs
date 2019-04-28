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

    public GameObject OutsidePointsParent;
    List<Transform> OutsidePoints = new List<Transform>();
    public List<Transform> SmallHousePoints = new List<Transform>();
    public List<Transform> BigHousePoints = new List<Transform>();
    public Transform PalacePoint;

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

        foreach (Transform _point in OutsidePointsParent.transform)
        {
            OutsidePoints.Add(_point);
        }
    }

    public void ReturnToHome()
    {
        List<Transform> VarifiedPoints = new List<Transform>();
        foreach (var _point in BigHousePoints)
        {
            if (_point != null && _point.gameObject.activeInHierarchy)
                VarifiedPoints.Add(_point);
        }
        foreach (var _point in SmallHousePoints)
        {
            if (_point != null && _point.gameObject.activeInHierarchy)
                VarifiedPoints.Add(_point);
        }

        foreach (var _npc in NPCs)
        {

            if (VarifiedPoints.Count > NPCs.IndexOf(_npc) && VarifiedPoints[NPCs.IndexOf(_npc)] != null)
                _npc.GetComponent<NPC>().StartWalkingTowardsPoint(VarifiedPoints[NPCs.IndexOf(_npc)].position);
            else
                Debug.LogError("NOT ENOUGH SPACE");
        }
    }

    public void SetSacrificePoints()
    {
        foreach (var _npc in NPCs)
        {
            if (standingPoints.Count > NPCs.IndexOf(_npc) && standingPoints[NPCs.IndexOf(_npc)] != null)
                _npc.GetComponent<NPC>().StartWalkingTowardsPoint(standingPoints[NPCs.IndexOf(_npc)].position, WellParent.transform);
            else
                Debug.LogError("NOT ENOUGH SPACE");
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
            Invoke("StartTheCulling", 1);
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
        //Start bowing animation for each NPC
        foreach (var _npc in NPCs)
        {
            _npc.GetComponent<NPC>().BowToPit();
        }

        //Create a list of only front row people
        List<int> randomNumberList = new List<int>();
        for (int i = 0; i < 24; i++)
        {
            randomNumberList.Add(i);
        }

        //Pick 5 random people from the front row to die
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(3f);


            int randomIndex = Random.Range(0, randomNumberList.Count);
            while (randomNumberList[randomIndex] >= NPCs.Count)
            {
                randomIndex = Random.Range(0, randomNumberList.Count);
                yield return null;
            }

            NPCs[randomNumberList[randomIndex]].GetComponent<NPC>().JumpInPit();
            Debug.Log(randomNumberList[randomIndex]);
            randomNumberList.RemoveAt(randomIndex);
        }

        yield return new WaitForSeconds(3f);

        //Stop bowing animation for each NPC
        foreach (var _npc in NPCs)
        {
            _npc.GetComponent<NPC>().StopBowing();
        }

        //Activate cutscene and after culling events
        GodManager.Instance.OnCullingDone(GodManager.Choices.VolcanoGod);

        yield return null;
    }
}
