using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GodManager : MonoBehaviour
{
    public static GodManager Instance;
    public enum Choices { WindGod, SunGod, BuildingGod, VolcanoGod, FurtilityGod }

    public int CurrentLevel;

    public PlayableDirector Director;
    public Cutscene[] Cutscenes;

    private void Awake()
    {
        Instance = this;
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
}

[System.Serializable]
public class Cutscene
{
    public PlayableAsset[] Shots;
}
