using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChancelButton : MonoBehaviour
{
    public GameObject reklamaWindow;
    int level = 0;
    // Start is called before the first frame update
    public void tapNO()
    {
        reklamaWindow.SetActive(false);
    }
   public SceneChange sc = new SceneChange();
    public void tapYES()
    {
        
        //Вставить показ рекламы
        //Если реклама просмотрена, то
        sc.ChangeLevel(level);
        //иначе
       // reklamaWindow.SetActive(false);
    }


    public void tapOnHistory(int lvl)
    {
        level = lvl;
        reklamaWindow.SetActive(true);
    }
}
