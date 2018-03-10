using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameModel : SingletonMonoBehavior<gameModel> {
    public int costTotal;
    public int cur_Cost;

    public string gameoverReason = "";

    public Draggable selectedCards;

    public void init() {
        initCost();
    }

    void initCost() {
        costTotal = 10;
        cur_Cost = costTotal;
    }

    public void deductCost(int val) {
        cur_Cost -= val;
    }

    public bool checkCostCanBeDeduct(int val) {
        if (cur_Cost - val < 0) {
            return false;
        }
        return true;
    }

    public bool checkIsAllWhiteChessRunOutActionVal() {
        foreach (var item in BoardManager.Instance.whiteChess) {
            if (!item) {
                continue;
            }
            if (item.actionVal>0) {
                return false;
            }
        }
        return true;
    }

    public bool checkIsAllWhiteChessDied() {
        foreach (var item in BoardManager.Instance.whiteChess) {
            if (!item) {
                continue;
            }
            if (item.health > 0) {
                return false;
            }
        }
        return true;
    }

    public int getCurrentChessmanHealth() {
        return BoardManager.Instance.selectedChessman.health;
    }

    public int getCurrentChessmanDamage() {
        return BoardManager.Instance.selectedChessman.damage;
    }

    public int getCurrentChessmanActionVal() {
        return BoardManager.Instance.selectedChessman.actionVal;
    }

}
