using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GodManager : MonoBehaviour
{
    public static GodManager Instance;
    public enum Choices { WindGod, SunGod, BuildingGod, VolcanoGod, FurtilityGod }
    public Choices currentChoice;


    public PlayableDirector Director;
    public Cutscene[] Cutscenes;

    [Space]

    public PlayableAsset PanToPitPlayable;

    [Space]

    public PlayableAsset WindGodPlayable;
    public PlayableAsset SunGodPlayable;
    public PlayableAsset BuildingGodPlayable;
    public PlayableAsset VolcanoGodPlayable;
    public PlayableAsset FurtilityGodPlayable;

    [Space]

    public PlayableAsset SunGoingDown1;
    public PlayableAsset SunGoingDown2; //Pikdonker - Game over

    [Space]

    public PlayableAsset VolcanoLevel1;
    public PlayableAsset VolcanoLevel2;
    public PlayableAsset VolcanoLevel3;
    public PlayableAsset VolcanoLevel4; //(Uitbarsting) - Game over

    [Space]

    public PlayableAsset WindLevel1;
    public PlayableAsset WindLevel2;
    public PlayableAsset WindLevel3;
    public PlayableAsset WindLevel4; //(Iedereen en alles waait weg) - Game over
    public bool IsWaitingForAllToGoInside = false;


    public int CurrentLevel;

    public bool WindGodActive, SunGodActive, BuildingGodActive, VolcanoGodActive, FurtilityGodActive = false;
    public int WindLevel, SunLevel, BuildingLevel, VolcanoLevel, FurtilityLevel = 0;

    public GameObject Campfire;
    public GameObject[] SmallHouses;
    public GameObject[] BigHouses;
    public GameObject Palace;

    Coroutine checkGodStatusRoutine;

    private void Awake()
    {
        Instance = this;

        Campfire.SetActive(true);
        SmallHouses[0].SetActive(false);
        SmallHouses[1].SetActive(false);
        BigHouses[0].SetActive(false);
        BigHouses[1].SetActive(false);
        Palace.SetActive(false);

        WindGodActive = SunGodActive = BuildingGodActive = VolcanoGodActive = FurtilityGodActive = false;
        WindLevel = SunLevel = BuildingLevel = VolcanoLevel = FurtilityLevel = 0;
    }

    public void OnChoiceMade(int _choiceIndex)
    {
        UIManager.Instance.ChangeCanvasActivity(false);
        UIManager.Instance.DisableButton(_choiceIndex);

        Director.playableAsset = PanToPitPlayable;
        Director.Play();

        AIManager.Instance.SetSacrificePoints();

        currentChoice = (Choices)_choiceIndex;
    }

    public void OnCullingDone()
    {
        switch (currentChoice)
        {
            case Choices.WindGod:
                WindGodActive = true;
                Director.playableAsset = WindGodPlayable;
                break;
            case Choices.SunGod:
                SunGodActive = true;
                Director.playableAsset = SunGodPlayable;
                break;
            case Choices.BuildingGod:
                BuildingGodActive = true;
                Director.playableAsset = BuildingGodPlayable;
                break;
            case Choices.VolcanoGod:
                VolcanoGodActive = true;
                Director.playableAsset = VolcanoGodPlayable;
                break;
            case Choices.FurtilityGod:
                FurtilityGodActive = true;
                Director.playableAsset = FurtilityGodPlayable;
                break;
        }


        Director.Play();
        AIManager.Instance.Invoke("ReturnToHome", (float)Director.duration);
    }

    public void LevelUpGods()
    {
        if (!WindGodActive)
            WindLevel++;
        else
            WindLevel = 0;


        if (!SunGodActive)
            SunLevel++;
        else
            SunLevel = 0;


        if (BuildingGodActive)
            BuildingLevel++;

        if (!VolcanoGodActive)
            VolcanoLevel++;
        else
            VolcanoLevel = 0;


        if (FurtilityGodActive)
            FurtilityLevel++;

        CheckGodStatus();
    }

    void CheckGodStatus()
    {
        if (checkGodStatusRoutine != null) StopCoroutine(checkGodStatusRoutine);
        checkGodStatusRoutine = StartCoroutine(ICheckGodStatus());
    }

    bool IsEveryoneInside()
    {
        if (IsWaitingForAllToGoInside)
            return false;
        else
            return true;
    }

    IEnumerator ICheckGodStatus()
    {
        switch (FurtilityLevel)
        {
            case 2:
                AIManager.Instance.SpawnNPCs(15, 3);
                yield return new WaitForSeconds(3);
                break;

            case 3:
                AIManager.Instance.SpawnNPCs(15, 3);
                yield return new WaitForSeconds(3);
                break;

            case 4:
                AIManager.Instance.SpawnNPCs(15, 3);
                yield return new WaitForSeconds(3);
                break;

            case 5:
                AIManager.Instance.SpawnNPCs(15, 3);
                yield return new WaitForSeconds(3);
                break;
        }

        switch (BuildingLevel)
        {
            //case 1:
            //    SmallHouses[0].SetActive(true);
            //    yield return new WaitForSeconds(1);
            //    break;

            case 2:
                SmallHouses[1].SetActive(true);
                yield return new WaitForSeconds(1);
                break;

            case 3:
                BigHouses[0].SetActive(true);
                yield return new WaitForSeconds(1);
                break;

            case 4:
                BigHouses[1].SetActive(true);
                yield return new WaitForSeconds(1);
                break;

            case 5:
                Palace.SetActive(true);
                yield return new WaitForSeconds(1);
                break;
        }

        switch (WindLevel)
        {
            case 1:
                //kampvuur uit waaien

                //Wacht tot iedereen binnen is voor animatie
                //IsWaitingForAllToGoInside = true;
                AIManager.Instance.GoInside();

                //Ranzig gehardcode want dat andere joch werkt voor een of andere reden niet
                yield return new WaitForSeconds(7);
                //yield return new WaitUntil(IsEveryoneInside);

                Director.playableAsset = WindLevel1;
                Director.Play();
                yield return new WaitForSeconds((float)WindLevel1.duration);
                break;

            case 2:
                //mensen wegwaaien
                AIManager.Instance.GoInside();
                yield return new WaitForSeconds(7);

                Director.playableAsset = WindLevel2;
                Director.Play();
                yield return new WaitForSeconds((float)WindLevel2.duration);
                break;

            case 3:
                //mensen en simpele gebouwen wegwaaien
                AIManager.Instance.GoInside();
                yield return new WaitForSeconds(7);

                Director.playableAsset = WindLevel3;
                Director.Play();
                yield return new WaitForSeconds((float)WindLevel3.duration);
                break;

            case 4:
                //alle mensen en gebouwen wegwaaien GAME OVER
                AIManager.Instance.GoInside();
                yield return new WaitForSeconds(7);

                Director.playableAsset = WindLevel4;
                Director.Play();
                yield return new WaitForSeconds((float)WindLevel4.duration);
                break;
        }

        switch (VolcanoLevel)
        {
            case 1:
                Director.playableAsset = VolcanoLevel1;
                Director.Play();
                yield return new WaitForSeconds((float)VolcanoLevel1.duration);
                //zachte tril en beetje licht
                break;

            case 2:
                Director.playableAsset = VolcanoLevel2;
                Director.Play();
                yield return new WaitForSeconds((float)VolcanoLevel2.duration);
                //iets hardere tril, iets meer licht en beetje rook
                break;

            case 3:
                Director.playableAsset = VolcanoLevel3;
                Director.Play();
                yield return new WaitForSeconds((float)VolcanoLevel3.duration);
                //iets hardere tril, meer licht en meer rook
                break;

            case 4:
                Director.playableAsset = VolcanoLevel4;
                Director.Play();
                yield return new WaitForSeconds((float)VolcanoLevel4.duration);
                //enorme tril, enorm veel licht, enorm veel rook en eruptie - GAME OVER
                break;
        }

        switch (SunLevel)
        {
            case 1:
                Director.playableAsset = SunGoingDown1;
                Director.Play();
                yield return new WaitForSeconds((float)SunGoingDown1.duration);
                break;

            case 2:
                Director.playableAsset = SunGoingDown2;
                Director.Play();
                yield return new WaitForSeconds((float)SunGoingDown2.duration);
                break;
        }

        if (AIManager.Instance.NPCs.Count == 0 || SunLevel >= 2 || VolcanoLevel >= 4)
            UIManager.Instance.GameOver();
        else if (BuildingLevel >= 5 || FurtilityLevel >= 5)
            UIManager.Instance.GameWon();
        else
            UIManager.Instance.ChangeCanvasActivity(true);


        yield return null;
    }
}

[System.Serializable]
public class Cutscene
{
    public string Name = "New Cutscene";
    public PlayableAsset[] Shots;
}
