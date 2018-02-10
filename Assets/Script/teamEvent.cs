using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamEvent : MonoBehaviour
{
    public static teamEvent instance;

    [SerializeField]
    private GameObject teamEventPanel;

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

    public void teamEventGame()
    {
        Time.timeScale = 0f;
        teamEventPanel.SetActive(true);
    }

    public void closeTeamEventGame()
    {
        Time.timeScale = 1f;
        teamEventPanel.SetActive(false);
    }
}
