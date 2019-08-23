using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject MainButton = null;
    [SerializeField]
    private GameObject Canvas = null;
    private void Awake()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor)
            Destroy(this.gameObject);
    }
    private void Start()
    {
        MainButton.SetActive(true);
    }
    public void ClickMainButton()
    {
        if (Canvas.activeSelf)
            Canvas.SetActive(false);
        else
            Canvas.SetActive(true);
    }
}
