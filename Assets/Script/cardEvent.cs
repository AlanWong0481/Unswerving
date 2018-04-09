using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardEvent : MonoBehaviour
{
    public static cardEvent instance;

    [SerializeField]
    private GameObject cardExitPanel;

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

    public void openCardHandinGame()
    {
        Time.timeScale = 0f;
        cardExitPanel.SetActive(true);
    }

    public void closeCardHandinGame()
    {
        Time.timeScale = 1f;
        cardExitPanel.SetActive(false);
    }
}
