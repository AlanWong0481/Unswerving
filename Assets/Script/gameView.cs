using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameView : SingletonMonoBehavior<gameView> {

    public void init() {

    }

    public void updateCostDisplay() {
        // Cost Display
        print(gameModel.instance.cur_Cost);
    }
    public void updateActonDisplay() {
        int val = gameModel.instance.getCurrentChessmanActionVal();
        print(gameModel.instance.getCurrentChessmaName() + "的移動值還有" + val + "步");
    }

    public void updateHealthDisplay() {
        int val = gameModel.instance.getCurrentChessmanHealth();
        print(gameModel.instance.getCurrentChessmaName() + "的血量值還有" + val + "");
    }

    public void showupPlayerSelectWhatChessman() {
        int healthval = gameModel.instance.getCurrentChessmanHealth();
        int attackval = gameModel.instance.getCurrentChessmanDamage();
        int Actionval = gameModel.instance.getCurrentChessmanActionVal();

        print("你選擇了" + gameModel.instance.getCurrentChessmaName() + "他的血量值有" + healthval + " 他的攻擊值有" + attackval + "  他的移動值有" + Actionval);
    }
}
