using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public static Victory instance;
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
            Application.LoadLevel(6);
        }
    }
}
