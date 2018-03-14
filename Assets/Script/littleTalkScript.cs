using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class littleTalkScript : MonoBehaviour
{
    Text uiText;
    int counter = -1;

    public List<string> textArray;

    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && counter < textArray.Count - 1) //右鍵繼續
        {
            counter++;
            textDataBase(counter);
        }
    }

    void textDataBase(int counter)
    {
        if (counter < 0 || counter >= textArray.Count)
        {
            return;
        }

        uiText.text = textArray[counter];
    }
}
