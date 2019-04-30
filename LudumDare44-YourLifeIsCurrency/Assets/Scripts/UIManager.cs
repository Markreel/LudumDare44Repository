using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject ButtonHolder;
    public GameObject[] Buttons;

    public GameObject GameOverWindow;
    public GameObject GameWonWindow;

    private void Awake()
    {
        Instance = this;
        ChangeCanvasActivity(false);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeCanvasActivity(bool _isActive)
    {
        ButtonHolder.SetActive(_isActive);
    }

    public void DisableButton(int _index)
    {
        Buttons[_index].GetComponent<Button>().interactable = false;
        Buttons[_index].transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverWindow.SetActive(true);
    }

    public void GameWon()
    {
        Time.timeScale = 0;
        GameWonWindow.SetActive(true);
    }
}
