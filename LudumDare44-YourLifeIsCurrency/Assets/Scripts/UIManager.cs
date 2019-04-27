using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour

{
    public bool IsActive = true;


    void Start()
    {

    }

    void Update()
    {

    }

    void ChangeActivity(bool _isActive)
    {
        GetComponent<GraphicRaycaster>().enabled = IsActive = _isActive;
    }
}
