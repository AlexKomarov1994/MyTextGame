using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelOUT : MonoBehaviour
{
   public GameObject panel;
    void Start()
    {
        StartCoroutine(panOUT());
    }

    IEnumerator panOUT()
    {
        panel.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.9f);
        panel.GetComponent<Animator>().enabled = false;
    }
}
