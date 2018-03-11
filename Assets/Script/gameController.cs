using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : SingletonMonoBehavior<gameController> {

    private void Start() {
        gameModel.instance.init();
        gameView.instance.init();
        init();
    }
    private void Update() {
        testFunction();
        
    }

    public void testFunction() {
        if (Input.anyKeyDown) {
            switch (Input.inputString) {
                case "Q":
                    OnPlayerReadyToDropDownCards();
                    break;
                case "W":
                    break;
                case "E":
                    break;
            }
        }
    }

    public void init() {

    }

    public void OnPlayerSelectedChessmanRunOutActionVal() {
        print("你的角色已用盡體力");
        if (gameModel.instance.checkIsAllWhiteChessRunOutActionVal()) {
            OnAllChessManRunOutActionVal();
        }
    }

    public void OnPlayerSelectedChessmanDied() {
        print("你的角色已陣亡");
        BoardManager.Instance.selectedChessman = null;
        if (gameModel.instance.checkIsAllWhiteChessDied()) {
            OnAllSelectedChessmanDied();
        }
    }

    public void OnAllSelectedChessmanDied() {
        print("你的所有角色已陣亡");
        gameModel.instance.gameoverReason = "所有角色已陣亡";

        gameover();

    }

    public void OnAllChessManRunOutActionVal() {
        print("你的所有角色已用盡體力");
        gameModel.instance.gameoverReason = "所有角色已用盡體力";


        gameover();
    }

    public void gameover() {
        print("你已經輸了 原因：" + gameModel.instance.gameoverReason);
    }

    public void OnPlayerPickUpCards(Draggable cards) {
        gameModel.instance. selectedCards = cards;
    }

    public void OnPlayerReadyToDropDownCards() {
        if (!gameModel.instance.checkCostCanBeDeduct(gameModel.instance.selectedCards.cost)) {
            //out
            return;
        }
        //do
        OnPlayerDropDownCards();
    }

    public void OnPlayerDropDownCards() {
        gameModel.instance.deductCost(gameModel.instance.selectedCards.cost);
        gameView.instance.updateCostDisplay();
    }
}
