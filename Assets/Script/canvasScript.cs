using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class canvasScript : SingletonMonoBehavior<canvasScript> {

    public bool openMoveButton; //unity是會優先看public的內容
    public bool openAttackButton;

    public Text hpText;
    public Text spText;
    public Image hpImage;
    public Image spImage;

    public void updateBarInformation() {
        float maxhp = BoardManager.Instance.selectedChessman.maxHealth;
        float hp = BoardManager.Instance.selectedChessman.health;
        float maxsp = BoardManager.Instance.selectedChessman.ActionVal;
        float sp = BoardManager.Instance.selectedChessman.curActionVal;

        hpText.text = hp + " / " + maxhp;
        spText.text = sp + " / " + maxsp;

        float hpPercentage = hp / maxhp ;
        float spPercentage = sp / maxsp ;
        print("hpPercentage" + hpPercentage*100 + "%");
        print("spPercentage" + spPercentage*100 + "%");

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
