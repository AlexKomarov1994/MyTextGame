using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//для получения событий с клавиатуры андроида

public class DalsheScript : MonoBehaviour
{
    public GameObject _questionPanel;//панель с вопросом
    public GameObject _buttonDalshe;//объект кнопки "дальше"
    public GameObject _textOnButton;//объект текста на кнопке
    public GameObject _textPanel;//объект панели с итоговым текстом
    public GameObject _quitPanel;//панель с вопросом выхода
    
    bool _forCoroutine = false;//переменная для того, чтобы вопросы быстро не перелистывались. 
    
    bool _endOfGame = false;
   TouchScreenKeyboard keyboardd;


    int _numberOfText = 0;//Номер текста будет передаваться из главной активности
    public TextAsset[] _texts;
    public GameObject _textForCountOfQuestions;//Текст вверху страницы, где будет написано сколько всего вопросов
    public GameObject _textForMainQuestion;//Объект главного вопроса
    public GameObject _textForSubQuestion;//Объект дополнительного вопроса
    public GameObject _textBoxForAnswer;//объект ответа на вопрос(textbox)
    public GameObject _textForItogText;//объект итогового тексста
    public GameObject _textForNazvanie;//текст для названия
    int _countOfQuestions = 0;//Переменная для количества вопросов
    string _myText = "";//переменная для полученного текста
    string[,] table;    
    int _questionNow = 0;//Какой вопрос в данный момент
    int _randomQuestion = 0;//переменная для рандомного вывода вопросов







    //}

    //Console.WriteLine(N); 
    //Console.ReadKey();

    public void OnInputEvent()
    {
        keyboardd = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false);
    }

   

    private void Awake()
    {

        //keyboardd = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false);//открываем клавиатуру
        //_textBoxForAnswer.GetComponent<InputField>().ActivateInputField();//для того, чтобы фокус не сходил с инпута - 1
        //_textBoxForAnswer.GetComponent<InputField>().Select();//для того, чтобы фокус не сходил с инпута - 2
       
        _myText = _texts[SceneChange._numberOfText].text;//Считываем текст по номеру из массива текстовых ассетов
        for (int i = 0; i < _myText.Length; i++)
            if (_myText[i] == '$') _countOfQuestions++;//находим сколько замен слов в тексте 
        //Debug.Log("Выполнены все первоначальные действия по получению текста и получение его параметров. Количество вопросов в тексте: " + _countOfQuestions);
        table = new string[_countOfQuestions, 5];//Таблица вопросов-ответов
        int Dollar = 0; // номер символа, где был найден доллар 
        int Amper = 0;//Номер символа, где был найден амперсант 
        int Ili = 0;//Номер символа, где был найден знак ИЛИ 
        int Slash = 0;//Номер символа, где был найден Слэш 
        int Star = 0;//Номер символа, где была найден звезда 
        

        for (int i = 0; i < _countOfQuestions; i++)
        {
            Dollar = _myText.IndexOf('$', Dollar + 1);
            Amper = _myText.IndexOf('&', Amper + 1);
            Ili = _myText.IndexOf('|', Ili + 1);
            Slash = _myText.IndexOf('/', Slash + 1);
            Star = _myText.IndexOf('*', Star + 1);
            if (Dollar != -1 && Amper != -1 && Ili != -1 && Slash != -1 && Star != -1)
            {
                table[i, 0] = _myText.Substring(Dollar + 1, Amper - Dollar - 1);
                table[i, 1] = _myText.Substring(Amper + 1, Ili - Amper - 1);
                table[i, 2] = _myText.Substring(Ili + 1, Slash - Ili - 1);
                table[i, 3] = " ";
            }
            else
            { Debug.Log("Ошибка. Не найден один из внутренних символов: $&|/*"); }
        }
        
        _textForNazvanie.GetComponent<Text>().text = table[0, 0];//пишем название в блоке итогового текста. Название в фале после первого доллара


        //Работа с карточкой вопроса. Вопрос записывается первый раз при запуске
        //_textForCountOfQuestions.GetComponent<Text>().text = "1 / " + _countOfQuestions;
        //_textForMainQuestion.GetComponent<Text>().text = table[_questionNow, 1];
        //_textForSubQuestion.GetComponent<Text>().text = table[_questionNow, 2];
        _randomQuestion = Random.Range(0, _countOfQuestions-1);//гинирируем рандомное число от нуля до количества вопросов
        _textForCountOfQuestions.GetComponent<Text>().text = "1 / " + _countOfQuestions;
        _textForMainQuestion.GetComponent<Text>().text = table[_randomQuestion, 1];
        _textForSubQuestion.GetComponent<Text>().text = table[_randomQuestion, 2];

    }


    public void Tap()
    {
        Debug.Log("Нажатие дальше");
       
        Animation _anim = GetComponent<Animation>();
        _anim.Play("DalsheAnim");

        if (_endOfGame == false)
        {
            StartCoroutine(SmenaKart());
           
        }
        else
        {
            SmenaVoprosov();
        }
       




    }

    IEnumerator SmenaKart()
    {
        _forCoroutine = true;//делаем верно, для того, чтобы она не могла больше работать в Update
        _buttonDalshe.GetComponent<Button>().interactable = false;//отключаем кнопку Дальше
       // Debug.Log("Кнопка заблокирована");

        //проверяем не кончились ли вопросы
        
            if (_questionPanel.GetComponent<Animator>().enabled == false)
            {
                _questionPanel.GetComponent<Animator>().enabled = true;
                yield return new WaitForSeconds(0.3f);
                _questionPanel.GetComponent<Animator>().SetTrigger("IN");
                SmenaVoprosov();
            }
            else
            {
                _questionPanel.GetComponent<Animator>().SetTrigger("OUT");         
                                yield return new WaitForSeconds(0.3f);
            SmenaVoprosov();
            _questionPanel.GetComponent<Animator>().SetTrigger("IN");
            
            }
            yield return new WaitForSeconds(0.6f);
       
            _buttonDalshe.GetComponent<Button>().interactable = true;//после смены вопроса, включаем кнопку дальше
                                                                     //Debug.Log("кнопка разблокирована: " + _endOfGame);

        _forCoroutine = false;//делаем его доступным в update
       

    }

    void SmenaVoprosov()
    {

        //try
        //{

        //Записываем ответ в массив
        table[_randomQuestion, 3] = _textBoxForAnswer.GetComponent<InputField>().text;
        //Вставляем строку в текст
        //int Dollar = _myText.IndexOf('$');
        //int Star = _myText.IndexOf('*');
        //_myText = _myText.Remove(Dollar, Star - Dollar + 1);
        //_myText=_myText.Insert(Dollar, _textBoxForAnswer.GetComponent<InputField>().text);
        //Debug.Log(_myText);
        
         //   Debug.Log(table[_questionNow, 0] + ", " + table[_questionNow, 1] + ", " + table[_questionNow, 2] + ", " + table[_questionNow, 3] + ", ");
            _questionNow++;//прибавляем еденицу к вопросу в данный момент
           // Debug.Log("вопрос сейчас:" + _questionNow);
            if (_questionNow + 1 == _countOfQuestions)//Если аопрос последний, то меняем текст в кнопке "дальше" на "закончить"
            {
                _textOnButton.GetComponent<Text>().text = "закончить";
                _endOfGame = true;
              
            }


            if (_questionNow < _countOfQuestions)//Если вопрос в данный момент меньше общего количества вопросов
            {
                _textForCountOfQuestions.GetComponent<Text>().text = _questionNow + 1 + " / " + _countOfQuestions;//Изменяем строку, где написано сколько вопросов осталось

            //Меняем вопрос в карточке
          
            do
            {
                _randomQuestion = Random.Range(0, _countOfQuestions);
                Debug.Log("random: " +_randomQuestion + "  " + table[_randomQuestion, 3]);
               
            }
            while (table[_randomQuestion, 3] != " ");

              _textForMainQuestion.GetComponent<Text>().text = table[_randomQuestion, 1];
            _textForSubQuestion.GetComponent<Text>().text = table[_randomQuestion, 2];
            _textBoxForAnswer.GetComponent<InputField>().text = "";
            //далее меняем вопрос в карточке
            //_textForMainQuestion.GetComponent<Text>().text = table[_questionNow, 1];
            //    _textForSubQuestion.GetComponent<Text>().text = table[_questionNow, 2];
            //    _textBoxForAnswer.GetComponent<InputField>().text = "";

            //фокус на инпут
            _textBoxForAnswer.GetComponent<InputField>().ActivateInputField();//для того, чтобы фокус не сходил с инпута - 1
            _textBoxForAnswer.GetComponent<InputField>().Select();//для того, чтобы фокус не сходил с инпута - 2




        }
            else
            { //Debug.Log("Конец игры. вывод итогового текста");
                ItogText();
            }
        //}
        //catch { Debug.Log("печалька"); }
        
    }

    void ItogText()
    {
        //формирование нового текста из массива
        for (int i = 0; i < _countOfQuestions; i++)
        {
            //Вставляем строку в текст
            int Dollar = _myText.IndexOf('$');
            int Star = _myText.IndexOf('*');
            _myText = _myText.Remove(Dollar, Star - Dollar + 1);
            _myText = _myText.Insert(Dollar, table[i,3]);
        }


        // keyboardd.active = false;
        _textForItogText.GetComponent<Text>().text = _myText;
        
        //Анимация выдвижения панели
        if (_textPanel.GetComponent<Animator>().enabled == false)
        {
            _textPanel.GetComponent<Animator>().enabled = true;
        }
        
    }


    bool _activeQuitPanel = false;
   
    public void buttonNO()
    {
        _activeQuitPanel = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            {
                _activeQuitPanel = !_activeQuitPanel;
                Debug.Log("esc");
            }
            _quitPanel.SetActive(_activeQuitPanel);
        if (Application.platform == RuntimePlatform.Android) {


           



            if (keyboardd != null && keyboardd.status == TouchScreenKeyboard.Status.Done && _forCoroutine == false)//сравниваем _forCoroutine, что она доступна для нажатия
        {
            Tap();

              

        }
            
        }
    }
}
