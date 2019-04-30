using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    public List<GameObject> NPCs = new List<GameObject>();
    public List<GameObject> NPCPrefabs = new List<GameObject>();

    public GameObject WellParent;
    List<Transform> standingPoints = new List<Transform>();

    public GameObject OutsidePointsParent;
    public List<Transform> OutsidePoints = new List<Transform>();
    public List<Transform> SmallHousePoints = new List<Transform>();
    public List<Transform> BigHousePoints = new List<Transform>();
    public Transform PalacePoint;

    [Header("Settings:")]
    public float DelayPerSuicide = 3;

    Coroutine spawnNPCsRoutine;
    Coroutine cullingRoutine;

    private void Awake()
    {
        Instance = this;

        //Time.timeScale = 2;

        //Gets all possible standingpoints around the pit
        foreach (Transform _point in WellParent.transform)
        {
            standingPoints.Add(_point);
        }

        //Gets all possible home points outside
        foreach (Transform _point in OutsidePointsParent.transform)
        {
            OutsidePoints.Add(_point);
        }

        //Spawns the starting amount of NPCs
        SpawnNPCs(15,1, true);

        //Gets all NPC's that are parented to this gameobject
        RefreshNPCList();
    }

    /// <summary>
    /// Spawns a given amount of NPC's at a random OutsidePoint
    /// </summary>
    /// <param name="_amount"></param>
    public void SpawnNPCs(int _amount, float _spawnDuration, bool _enableCanvasAfterwards = false)
    {
        if (spawnNPCsRoutine != null) StopCoroutine(spawnNPCsRoutine);
        spawnNPCsRoutine = StartCoroutine(ISpawnNPCs(_amount, _spawnDuration, _enableCanvasAfterwards));
    }

    IEnumerator ISpawnNPCs(int _amount, float _spawnDuration, bool _enableCanvasAfterwards = false)
    {
        List<Transform> TempPoints = new List<Transform>();
        foreach (var _point in OutsidePoints)
        {
            TempPoints.Add(_point);
        }

        for (int i = 0; i < _amount; i++)
        {
            //Get random prefab
            int _randomNum = Random.Range(0, NPCPrefabs.Count);

            //Get a random unused point. If all used, get used point.
            Transform _point = TempPoints.Count > 0 ? TempPoints[Random.Range(0, TempPoints.Count)] : OutsidePoints[Random.Range(0, OutsidePoints.Count)];

            //Instantiate prefab
            GameObject _obj = Instantiate(NPCPrefabs[_randomNum], _point.position, NPCPrefabs[_randomNum].transform.rotation, transform);
            //Add instantiated object to the NPC list
            NPCs.Add(_obj);

            //Remove used point from points list
            TempPoints.Remove(_point);

            yield return new WaitForSeconds(_spawnDuration / _amount);
        }

        if (_enableCanvasAfterwards)
            UIManager.Instance.ChangeCanvasActivity(true);

        yield return null;
    }

    /// <summary>
    /// Finds all NPC's that are parented to this gameobject and puts them in a list
    /// </summary>
    public void RefreshNPCList()
    {
        NPCs.Clear();

        foreach (Transform _npc in transform)
        {
            if (_npc.GetComponent<NavMeshAgent>() && _npc.gameObject.activeInHierarchy)
                NPCs.Add(_npc.gameObject);
        }
    }

    public void ReturnToHome()
    {
        //List<Transform> VarifiedPoints = new List<Transform>();
        //foreach (var _point in BigHousePoints)
        //{
        //    if (_point != null && _point.gameObject.activeInHierarchy)
        //        VarifiedPoints.Add(_point);
        //}
        //foreach (var _point in SmallHousePoints)
        //{
        //    if (_point != null && _point.gameObject.activeInHierarchy)
        //        VarifiedPoints.Add(_point);
        //}

        foreach (var _npc in NPCs)
        {

            //if (VarifiedPoints.Count > NPCs.IndexOf(_npc) && VarifiedPoints[NPCs.IndexOf(_npc)] != null)
               // _npc.GetComponent<NPC>().StartWalkingTowardsPoint(VarifiedPoints[NPCs.IndexOf(_npc)].position);
            //else if
            if (OutsidePoints.Count > NPCs.IndexOf(_npc) && OutsidePoints[NPCs.IndexOf(_npc)] != null)
                _npc.GetComponent<NPC>().StartWalkingTowardsHome(OutsidePoints[NPCs.IndexOf(_npc)].position);

            else
                Debug.LogError("NOT ENOUGH SPACE");
        }
    }

    public void GoInside()
    {
        Debug.Log("GOING INSIDE");
        List<Transform> _smallHousePoints = new List<Transform>();
        List<Transform> _bigHousePoints = new List<Transform>();

        foreach (var _point in BigHousePoints)
        {
            if (_point != null && _point.gameObject.activeInHierarchy)
                _bigHousePoints.Add(_point);
        }
        foreach (var _point in SmallHousePoints)
        {
            if (_point != null && _point.gameObject.activeInHierarchy)
                _smallHousePoints.Add(_point);
        }

        foreach (var _npc in NPCs)
        {
            

            if (_bigHousePoints.Count > NPCs.IndexOf(_npc) && _bigHousePoints[NPCs.IndexOf(_npc)] != null)
                _npc.GetComponent<NPC>().StartWalkingTowardsHome(_bigHousePoints[NPCs.IndexOf(_npc)].position, NPC.Location.BigHouse);

            else if (_smallHousePoints.Count + _bigHousePoints.Count > NPCs.IndexOf(_npc) && _smallHousePoints[NPCs.IndexOf(_npc) - _bigHousePoints.Count] != null)
                _npc.GetComponent<NPC>().StartWalkingTowardsHome(_smallHousePoints[NPCs.IndexOf(_npc) - _bigHousePoints.Count].position, NPC.Location.SmallHouse);

            else
                GodManager.Instance.IsWaitingForAllToGoInside = false;
        }
    }

    public void SetSacrificePoints()
    {
        foreach (var _npc in NPCs)
        {
            if (standingPoints.Count > NPCs.IndexOf(_npc) && standingPoints[NPCs.IndexOf(_npc)] != null)
                _npc.GetComponent<NPC>().StartWalkingTowardsPit(standingPoints[NPCs.IndexOf(_npc)].position, WellParent.transform);
            else
                Debug.LogError("NOT ENOUGH SPACE");
        }
    }

    //5 VAN DE EERSTE 24 MANNEN MOETEN SPRINGEN
    public void CheckIfAllAtPit()
    {
        bool allThere = true;
        foreach (var _npc in NPCs)
        {
            if (!_npc.GetComponent<NPC>().AtPit)
                allThere = false;
        }

        if (allThere)
        {
            Invoke("StartTheCulling", 1);
            Debug.Log("ALL THERE BABY");
        }
    }

    public void CheckIfAllAtHome()
    {
        bool allThere = true;
        foreach (var _npc in NPCs)
        {
            if (!_npc.GetComponent<NPC>().AtHome)
                allThere = false;
        }

        if (allThere)
            GodManager.Instance.LevelUpGods();
    }

    public void CheckIfAllInside()
    {
        bool allThere = true;
        foreach (var _npc in NPCs)
        {
            if (!_npc.GetComponent<NPC>().AtHome)
                allThere = false;
        }

        if (allThere)
            GodManager.Instance.IsWaitingForAllToGoInside = false;
    }

    void StartTheCulling()
    {
        if (cullingRoutine != null)
            StopCoroutine(cullingRoutine);
        cullingRoutine = StartCoroutine(ICulling());
    }

    IEnumerator ICulling()
    {
        //List of NPC to be removed from the list at the end of the culling
        List<GameObject> DeadNPCList = new List<GameObject>();

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
            yield return new WaitForSeconds(DelayPerSuicide);


            int randomIndex = Random.Range(0, randomNumberList.Count);
            while (randomNumberList[randomIndex] >= NPCs.Count)
            {
                randomIndex = Random.Range(0, randomNumberList.Count);
                yield return null;
            }

            NPCs[randomNumberList[randomIndex]].GetComponent<NPC>().JumpInPit();
            DeadNPCList.Add(NPCs[randomNumberList[randomIndex]]);
            randomNumberList.RemoveAt(randomIndex);
        }

        //Short delay after last jump
        yield return new WaitForSeconds(3f);

        //Stop bowing animation for each NPC
        foreach (var _npc in NPCs)
        {
            _npc.GetComponent<NPC>().StopBowing();
        }

        //Remove dead NPC's from the game and from the list
        foreach (var _npc in DeadNPCList)
        {
            RemoveNPC(_npc);
        }

        //Activate after culling behaviour
        GodManager.Instance.OnCullingDone();

        yield return null;
    }

    public void RemoveNPC(GameObject _npcObj)
    {
        if (NPCs.Contains(_npcObj))
            NPCs.Remove(_npcObj);

        Destroy(_npcObj);
    }
}
