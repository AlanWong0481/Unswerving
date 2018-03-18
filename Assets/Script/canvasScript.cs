using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class canvasScript : SingletonMonoBehavior<canvasScript> {

    public bool openMoveButton; //unity是會優先看public的內容
    public bool openAttackButton;

    public Text hpText;
    public Text spText;

    public Slider hpSlider;
    public Slider spSlider;

    public List<Image> playerImageList;

    public void updateBarInformation() {
        foreach (var item in playerImageList) {
            if (!item) {
                continue;
            }
            item.gameObject.SetActive(false);
        }
        playerImageList[ BoardManager.Instance.selectedChessman.id ].gameObject.SetActive(true);

        

        float maxhp = BoardManager.Instance.selectedChessman.maxHealth;
        float hp = BoardManager.Instance.selectedChessman.health;
        float maxsp = BoardManager.Instance.selectedChessman.ActionVal;
        float sp = BoardManager.Instance.selectedChessman.curActionVal;


        if (hp <= 0) {
            playerImageList[ BoardManager.Instance.selectedChessman.id ].color = new Color(0.3f, 0.3f, 0.3f, 1);
            hp =  0;
        }


        hpText.text = "HP : "+ hp + " / " + maxhp;
        spText.text = "SP : "+ sp + " / " + maxsp;

        float hpPercentage = hp / maxhp ;
        float spPercentage = sp / maxsp ;
        print("hpPercentage" + hpPercentage*100 + "%");
        print("spPercentage" + spPercentage*100 + "%");

        hpSlider.value = hpPercentage;
        spSlider.value = spPercentage;

    }
    public void moveButton()
    {
        openMoveButton = !openMoveButton; //偵測移動按鈕是開還是關
        if (openMoveButton)
        {
            Vector2 v2 = BoardManager.Instance.saveChessmanVector2;
            BoardManager.Instance.selectChessman((int)v2.x, (int)v2.y, false);
            openAttackButton = false;
            AttackHighlights.Instance.Hidehighlights();
        }
        else
        {
            BoardHighlights.Instance.Hidehighlights();
            BoardManager.Instance.selectedChessman = null;
        }
    } //移動按鈕

    public void attackButton()
    {
        openAttackButton = !openAttackButton;
        if (openAttackButton)
        {
            Vector2 v2 = BoardManager.Instance.saveChessmanVector2;
            BoardManager.Instance.selectChessman((int)v2.x, (int)v2.y, true);
            openMoveButton = false;
            BoardHighlights.Instance.Hidehighlights();
        }
        else
        {
            AttackHighlights.Instance.Hidehighlights();
            BoardManager.Instance.selectedChessman = null;
        }
    } //攻擊按鈕
}
