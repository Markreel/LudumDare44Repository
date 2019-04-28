using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public bool IsActive = true;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeCanvasActivity(bool _isActive)
    {
        GetComponent<GraphicRaycaster>().enabled = IsActive = _isActive;
    }
}
