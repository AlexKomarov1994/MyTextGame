using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaController : MonoBehaviour
{

    public Button _startButton;//Поле для главной кнопки, которая должна заблокироваться
    public GameObject _verh;//поле для верхней панели
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void nazadClick()//Клик на кнопке Назад
    {
        StartCoroutine(verhNaverh());
    }

    IEnumerator verhNaverh()//Анимация для задвигания
    {
        
        _verh.GetComponent<Animator>().SetTrigger("OUT");
        yield return new WaitForSeconds(1.2f);
        _verh.GetComponent<Animator>().enabled = false;
        _startButton.GetComponent<Button>().interactable = true;//разблокируем главную кнопку
        Debug.Log("Подняли штору");


    }
    public void myClickMethod() //нажатие на главную кнопку
    {
        Animation Anima = GetComponent<Animation>();
        Anima.Play("PlayButtonAnima");
        _startButton.GetComponent<Button>().interactable = false;//блокируем главную кнопку
        StartCoroutine(SpisokStart());
      


    }

    IEnumerator SpisokStart()//Опускание шторки
    {
        yield return new WaitForSeconds(0.25f);
        if (_verh.GetComponent<Animator>().enabled == false)
        {
            
            _verh.GetComponent<Animator>().enabled = true;
            _verh.GetComponent<Animator>().SetTrigger("In");
            Debug.Log("Активировали анимацию, опустили штору");
           
        }
        else
        { _verh.GetComponent<Animator>().SetTrigger("In"); }

    }


  
}
