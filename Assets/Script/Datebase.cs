using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datebase : MonoBehaviour
{
    public static Datebase instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
    }

    public int sceneLevel = 0;
}
