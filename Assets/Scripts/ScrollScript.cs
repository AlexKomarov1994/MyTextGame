using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
  public  GameObject _scrolpanel;

    public void _scroll()
    {
        Debug.Log(_scrolpanel.GetComponent<RectTransform>().localPosition);
        if (_scrolpanel.GetComponent<RectTransform>().localPosition.y < 0)
            _scrolpanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        
    }
}
