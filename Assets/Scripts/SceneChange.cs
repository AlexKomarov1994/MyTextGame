using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static int _numberOfText;
  
    public GameObject panel;
   
    public void ChangeLevel(int level)
    {
       StartCoroutine( Perehod(level));
    }

    IEnumerator Perehod(int lvl)
    {
        panel.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.8f);
        _numberOfText = lvl;
        SceneManager.LoadScene("SobstvennoGame");
    }

    //для окна закрытия
    public GameObject _quitPanel;//панель закрытия
    bool _activeQuitPanel=false;//переменная отвечающая за активность этой панели закрытия
    public void _vozvrat()//если нажмут кнопку нет, панель должна скрыться
    {
        _activeQuitPanel = false;
    }
    void Update()
    {


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _activeQuitPanel = !_activeQuitPanel;

            }
            _quitPanel.SetActive(_activeQuitPanel);
     
    }
}
