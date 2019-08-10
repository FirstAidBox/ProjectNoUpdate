using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScrpit : MonoBehaviour
{
    public GameObject mainTextBar;
    public Text mainText;
    public string lastFunc;

    // Use this for initialization
    void Start()
    {
        mainText = mainTextBar.GetComponentInChildren<Text>();
    }

    public void DisableMenu()
    {
        mainTextBar.SetActive(false);
    }

    public void ThisIsWarrior()
    {
        mainText.text = "이것은 전사이다.";
        mainTextBar.SetActive(true);
        lastFunc = "ThisIsWarrior";
    }

    public void YesButtonClick()
    {
        
    }
    
}
