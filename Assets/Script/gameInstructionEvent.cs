using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameInstructionEvent : MonoBehaviour
{
    public static gameInstructionEvent instance;

    [SerializeField]
    private GameObject gameInstructionEventPanel;

    private void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void gameInstructionEventGame()
    {
        Time.timeScale = 0f;
        gameInstructionEventPanel.SetActive(true);
    }

    public void closeGameInstructionEventGame()
    {
        Time.timeScale = 1f;
        gameInstructionEventPanel.SetActive(false);
    }
}
