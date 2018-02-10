using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrender : MonoBehaviour
{
    public static Surrender instance;
    public Transform canvasTransform;
    public GameObject DefeatObject;

    public void Defeat()
    {
        Instantiate(DefeatObject, canvasTransform);
        DefeatObject.SetActive(true);
    }
}
