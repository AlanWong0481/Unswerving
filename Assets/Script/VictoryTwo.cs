using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTwo : MonoBehaviour
{
    public static VictoryTwo instance;
    public Transform canvasTransform;
    public GameObject WinBox;

    public void Awake()
    {
        instance = this;
    }

    public void WinGame()
    {
        Instantiate(WinBox, canvasTransform);
    }

    public void OnTriggerEnter(Collider other) //Enter函式是當兩個物件接觸的瞬間，會執行一次這個函式；
    {
        WinGame();

        if (other.gameObject.tag == "chess")
        {
            Datebase.instance.sceneLevel++;
            Application.LoadLevel(8);
        }
    }
}
