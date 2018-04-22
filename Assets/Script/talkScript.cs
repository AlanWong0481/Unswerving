using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkScript : MonoBehaviour
{
    Text uiText;
    public Image charImage; //人物立繪
    public Image charImageTwo;
    public Image charImageThree;
    public Image charImageFour;
    public Image talkImage; //對話框背景

    int counter = -1;

    public List<string> textArray;
    public List<leveltalkPlayScript> leveltalkPlayScriptArray;
    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && counter < textArray.Count - 1) //左鍵繼續
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
        uiText.text = leveltalkPlayScriptArray[Datebase.instance.sceneLevel].playScriptArray[ counter ].text;
        float color = 0.6f;
        switch (leveltalkPlayScriptArray[ Datebase.instance.sceneLevel ].playScriptArray[ counter ].from) {
            case character.Narration:
                charImage.color = new Color(0, 0, 0, 0);
                charImageTwo.color = new Color(0, 0, 0, 0);
                charImageThree.color = new Color(0, 0, 0, 0);
                charImageFour.color = new Color(0, 0, 0, 0);

                break;
            case character.warrior:
                charImage.color = new Color(1, 1, 1, 1);
                charImageTwo.color = new Color(0, 0, 0, 0);
                charImageThree.color = new Color(0, 0, 0, 0);
                charImageFour.color = new Color(0, 0, 0, 0);
                break;
            case character.priest:
                charImage.color = new Color(0, 0, 0, 0);
                charImageTwo.color = new Color(1, 1, 1, 1);
                charImageThree.color = new Color(0, 0, 0, 0);
                charImageFour.color = new Color(0, 0, 0, 0);
                break;
            case character.knight:
                charImage.color = new Color(0, 0, 0, 0);
                charImageTwo.color = new Color(0, 0, 0, 0);
                charImageThree.color = new Color(1, 1, 1, 1);
                charImageFour.color = new Color(0, 0, 0, 0);
                break;
            case character.archer:
                charImage.color = new Color(0, 0, 0, 0);
                charImageTwo.color = new Color(0, 0, 0, 0);
                charImageThree.color = new Color(0, 0, 0, 0);
                charImageFour.color = new Color(1, 1, 1, 1);
                break;
        }

        if (counter >= leveltalkPlayScriptArray[ Datebase.instance.sceneLevel ].playScriptArray.Count - 1)
        {
            charImage.gameObject.SetActive(false);
            charImageTwo.gameObject.SetActive(false);
            if (charImageThree) {
                charImageThree.gameObject.SetActive(false);
            }
            talkImage.gameObject.SetActive(false);
        }
    }

    public enum character {
        Narration,
        warrior,
        priest,
        knight,
        archer,
    }

    [System.Serializable]
    public class leveltalkPlayScript {
        public List<talkPlayScript> playScriptArray;
    }

    [System.Serializable]
    public class talkPlayScript {
        public character from;
        public string text;
    }

}