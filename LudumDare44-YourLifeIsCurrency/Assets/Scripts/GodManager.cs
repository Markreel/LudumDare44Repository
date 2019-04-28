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
    public PlayableAsset VolcanoLevel4;
    public PlayableAsset VolcanoLevel5; //(Uitbarsting) - Game over


    public int CurrentLevel;

    public bool WindGodActive, SunGodActive, BuildingGodActive, VolcanoGodActive, FurtilityGodActive = false;
    public int WindLevel, SunLevel, BuildingLevel, VolcanoLevel, FurtilityLevel = 0;

    public GameObject Campfire;
    public GameObject[] SmallHouses;
    public GameObject[] BigHouses;
    public GameObject Palace;

    private void Awake()
    {
        Instance = this;

        Campfire.SetActive(true);
        SmallHouses[0].SetActive(false);
        SmallHouses[1].SetActive(false);
        BigHouses[0].SetActive(false);
        BigHouses[1].SetActive(false);
        Palace.SetActive(false);
    }

    public void OnChoiceMade(int _choiceIndex)
    {
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

        Invoke("LevelUpGods", (float)Director.duration);
        AIManager.Instance.Invoke("ReturnToHome", (float)Director.duration);
    }

    void LevelUpGods()
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
        switch (SunLevel)
        {
            case 1:
                Director.playableAsset = SunGoingDown1;
                Director.Play();
                break;

            case 2:
                Director.playableAsset = SunGoingDown2;
                Director.Play();
                break;
        }

        switch (WindLevel)
        {
            case 1:
                //kampvuur uit waaien
                break;

            case 2:
                //mensen wegwaaien
                break;

            case 3:
                //mensen en level1 gebouwen wegwaaien
                break;

            case 4:
                //alle mensen en gebouwen wegwaaien - GAME OVER
                break;
        }

        switch (BuildingLevel)
        {
            case 1:
                SmallHouses[0].SetActive(true);
                break;

            case 2:
                SmallHouses[1].SetActive(true);
                break;

            case 3:
                BigHouses[0].SetActive(true);
                break;

            case 4:
                BigHouses[0].SetActive(true);
                break;

            case 5:
                Palace.SetActive(true);
                break;
        }

        switch (VolcanoLevel)
        {
            case 1:
                //zachte tril en beetje licht
                break;

            case 2:
                //iets hardere tril en iets meer licht
                break;

            case 3:
                //iets hardere tril, meer licht en een beetje rook
                break;

            case 4:
                //harde tril, veel licht en een veel rook
                break;

            case 5:
                //enorme tril, enorm veel licht, enorm veel rook en eruptie - GAME OVER
                break;
        }

        switch (FurtilityLevel)
        {
            case 1:
                //+15 mensen
                break;

            case 2:
                //+15 mensen
                break;

            case 3:
                //+15 mensen
                break;

            case 4:
                //+15 mensen
                break;

            case 5:
                //+15 mensen
                break;
        }

    }
}

[System.Serializable]
public class Cutscene
{
    public string Name = "New Cutscene";
    public PlayableAsset[] Shots;
}
