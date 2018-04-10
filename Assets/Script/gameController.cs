using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : SingletonMonoBehavior<gameController> {

    public GameObject Poison;

    public GameObject winBox;

    public int thisRoundsPlayerTakeDamage;
    public int thisRoundsPlayerTakeSp;

    public bool isPlayerOverTheFingerControl = false;

    private void Start() {
        gameModel.instance.init();
        gameView.instance.init();
        init();
    }

    private void Update() {
        testFunction();
        if (gameView.instance.chessmanLerpMove != null && gameView.instance.chessmanLerpMove.isLerping) {
            BoardManager.Instance.selectedChessman.gameObject.transform.position = gameView.instance.chessmanLerpMove.update();
        }

    }

    public void testFunction() {
        if (Input.anyKeyDown) {
            switch (Input.inputString) {
                case "Q":
                    OnPlayerReadyToDropDownCards();
                    break;
                case "W":
                    print("MOVE");
                    gameView.instance.lerpMoveTest.startLerp(new Vector3(),new Vector3(1,1,1),1.0f );
                    break;
                case "E":
                    break;
            }
        }
    }

    public void init() {

    }

    public void DamageChassman(Chessman target) {
        target.health -= BoardManager.Instance.selectedChessman.damage;
        
        damageDisplay.instance.spawnDamageDisplay(BoardManager.Instance.selectedChessman.damage, 0, target.gameObject.transform);

        target.healthChecker();
    }

    public void healthChassman(Chessman target) {
        target.health += 10;
        if (target.health > target.maxHealth) {
            target.resetHealthVal();
            damageDisplay.instance.spawnDamageDisplay(10, 3, target.gameObject.transform);
            if (target == BoardManager.Instance.selectedChessman) {
                gameView.instance.updateHealthDisplay();
            }
        }
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

    public void OnPlayerClickSkillButton() {
        if (!BoardManager.Instance.selectedChessman) {
            return;
        }
        if (BoardManager.Instance.selectedChessman.group != groupEnum.white) {
            return;
        }

        print("OnPlayerClickSkillButton");

        int spCost = 10;
        if (BoardManager.Instance.selectedChessman.curActionVal < spCost) {
            return;
        }

        BoardManager.Instance.selectedChessman.curActionVal -= spCost;
        gameController.instance.thisRoundsPlayerTakeSp = spCost;
        gameView.instance.updateActonDisplay();
        gameView.instance.cameraZoomIn();
        BoardManager.Instance.selectedChessman.GetComponentInChildren<Animator>().SetTrigger("onSkill");
    }

    public void OnTriggerEnter(Collider other)
    {
        BoardManager.Instance.selectedChessman.curActionVal++;
    }
}
