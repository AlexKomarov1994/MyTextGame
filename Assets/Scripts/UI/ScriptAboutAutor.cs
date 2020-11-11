using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAboutAutor : MonoBehaviour
{
    bool activeQuitePanel;
    public GameObject panel;

    public void tapOpen()
    {
        panel.SetActive(true);
    }

    public void tapClose()
    {
        panel.SetActive(false);
    }

    public void tapURL()
    {
        Application.OpenURL("https://www.instagram.com/marsa.games/");
    }

   

   
}
