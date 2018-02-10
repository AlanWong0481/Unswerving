using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public static Lose instance;
    public Transform canvasTransform;
    public GameObject loseBox;

    public void loseGame()
    {
        Instantiate(loseBox, canvasTransform);
    }

    public void OnTriggerEnter(Collider other)
    {
        loseGame();
    }
}
