using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    

   public void tap()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void insta()
    {
        Application.OpenURL("https://www.instagram.com/marsa.games/");
    }
}
