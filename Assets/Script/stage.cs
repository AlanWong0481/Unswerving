using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage : MonoBehaviour
{
    public static stage instance;

    [SerializeField]
    private GameObject stagePanel;

    [SerializeField]
    private GameObject otherStagePanel;

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

    public void stageGame()
    {
        Time.timeScale = 0f;
        stagePanel.SetActive(true);
        otherStagePanel.SetActive(false);
    }

    public void closeStageGame()
    {
        Time.timeScale = 1f;
        stagePanel.SetActive(false);
    }
}
