using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GodManager : MonoBehaviour
{
    public static GodManager Instance;
    public enum Choices { WindGod, SunGod, BuildingGod, VolcanoGod, FurtilityGod }

    public PlayableDirector Director;
    public Cutscene[] Cutscenes;

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
        OnChoiceMade((Choices)_choiceIndex);
        AIManager.Instance.SetSacrificePoints();
    }

    public void OnCullingDone(Choices _choice)
    {
        switch (CurrentLevel)
        {
            case 0:
                switch (_choice)
                {
                    case Choices.WindGod:
                        break;
                    case Choices.SunGod:
                        break;
                    case Choices.BuildingGod:
                        break;
                    case Choices.VolcanoGod:
                        Director.playableAsset = Cutscenes[CurrentLevel].Shots[1];
                        break;
                    case Choices.FurtilityGod:
                        break;
                }
                break;
            case 1:
                Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                break;
            case 2:
                Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                break;
        }

        CurrentLevel++;
        Director.Play();

        AIManager.Instance.Invoke("ReturnToHome", (float)Director.duration);
    }

    public void OnChoiceMade(Choices _choice)
    {
        UIManager.Instance.ChangeCanvasActivity(false);

        switch (CurrentLevel)
        {
            case 0:
                switch (_choice)
                {
                    case Choices.WindGod:
                        break;
                    case Choices.SunGod:
                        break;
                    case Choices.BuildingGod:
                        break;
                    case Choices.VolcanoGod:
                        Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                        break;
                    case Choices.FurtilityGod:
                        break;
                }
                break;

            case 1:
                switch (_choice)
                {
                    case Choices.WindGod:
                        break;
                    case Choices.SunGod:
                        break;
                    case Choices.BuildingGod:
                        break;
                    case Choices.VolcanoGod:
                        Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                        break;
                    case Choices.FurtilityGod:
                        break;
                }
                break;

            case 2:
                switch (_choice)
                {
                    case Choices.WindGod:
                        break;
                    case Choices.SunGod:
                        break;
                    case Choices.BuildingGod:
                        break;
                    case Choices.VolcanoGod:
                        Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                        break;
                    case Choices.FurtilityGod:
                        break;
                }
                break;

            case 3:
                switch (_choice)
                {
                    case Choices.WindGod:
                        break;
                    case Choices.SunGod:
                        break;
                    case Choices.BuildingGod:
                        break;
                    case Choices.VolcanoGod:
                        Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                        break;
                    case Choices.FurtilityGod:
                        break;
                }
                break;

            case 4:
                switch (_choice)
                {
                    case Choices.WindGod:
                        break;
                    case Choices.SunGod:
                        break;
                    case Choices.BuildingGod:
                        break;
                    case Choices.VolcanoGod:
                        Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                        break;
                    case Choices.FurtilityGod:
                        break;
                }
                break;

            case 5:
                switch (_choice)
                {
                    case Choices.WindGod:
                        break;
                    case Choices.SunGod:
                        break;
                    case Choices.BuildingGod:
                        break;
                    case Choices.VolcanoGod:
                        Director.playableAsset = Cutscenes[CurrentLevel].Shots[0];
                        break;
                    case Choices.FurtilityGod:
                        break;
                }
                break;
        }
        Director.Play();
    }

    void LevelUpGods()
    {
        if (!WindGodActive)
            WindLevel++;

        if (!SunGodActive)
            SunLevel++;

        if (BuildingGodActive)
            BuildingLevel++;

        if (!VolcanoGodActive)
            VolcanoLevel++;

        if (FurtilityGodActive)
            FurtilityLevel++;

        CheckGodStatus();
    }

    void CheckGodStatus()
    {
        switch(SunLevel)
        {
            case 1:
                //avond
                break;

            case 2:
                //pikdonker - GAME OVER
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

        switch(FurtilityLevel)
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
    public PlayableAsset[] Shots;
}
