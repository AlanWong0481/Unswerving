﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS : MonoBehaviour
{
    public void bnTwo()
    {
        SceneManager.LoadScene(1);
    }

    public void bn()
    {
        SceneManager.LoadScene(2);
    }

    public void mapBn()
    {
        SceneManager.LoadScene(3);
    }

    public void loadbn()
    {
        SceneManager.LoadScene(4);
        Datebase.instance.sceneLevel = 0;
    }

    public void loadbnTwo()
    {
        SceneManager.LoadScene(5);
    }

    public void playbn()
    {
        SceneManager.LoadScene(7);
        Datebase.instance.sceneLevel = 1;
    }

    public void playbnTwo()
    {
        SceneManager.LoadScene(9);
        Datebase.instance.sceneLevel = 2;
    }

    public void playbnThree()
    {
        SceneManager.LoadScene(11);
        Datebase.instance.sceneLevel = 3;
    }
}
