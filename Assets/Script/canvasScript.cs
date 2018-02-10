using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasScript : MonoBehaviour {
    public static canvasScript Static; //指定canvasScript為靜態

    public bool openMoveButton; //unity是會優先看public的內容
    public bool openAttackButton;

    private void Awake()
    {
        Static = this;
        Debug.Log(openMoveButton);
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
