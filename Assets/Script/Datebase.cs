using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datebase : MonoBehaviour
{
    public static Datebase instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int sceneLevel = 0;
}
