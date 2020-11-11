using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2ExitButton : MonoBehaviour
{
    public void ex()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
