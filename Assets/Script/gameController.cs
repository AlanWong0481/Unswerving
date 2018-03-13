using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : SingletonMonoBehavior<gameController> {

    public GameObject Poison;

    public GameObject winBox;

    private void Start() {
        gameModel.instance.init();
        gameView.instance.init();
        init();
    }
    private void Update()
    {
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

    public void DamageChassman(Chessman target) {
        target.health -= BoardManager.Instance.selectedChessman.damage;
        Instantiate(gameView.instance.hitEnemyParticle, target.gameObject.transform.position, Quaternion.identity);
        damageDisplay.instance.spawnDamageDisplay(BoardManager.Instance.selectedChessman.damage, 0, target.gameObject.transform);

        target.healthChecker();
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
        Vector2 v2 = new Vector2(BoardManager.Instance.selectedChessman.CurrentX, BoardManager.Instance.selectedChessman.CurrentY);
        List<Vector2> attackV2List = new List<Vector2>();
        attackV2List.Add(new Vector2(v2.x - 1, v2.y - 1));
        attackV2List.Add(new Vector2(v2.x , v2.y - 1));
        attackV2List.Add(new Vector2(v2.x + 1, v2.y - 1));
        attackV2List.Add(new Vector2(v2.x - 1, v2.y ));
        attackV2List.Add(new Vector2(v2.x + 1, v2.y ));
        attackV2List.Add(new Vector2(v2.x - 1, v2.y + 1));
        attackV2List.Add(new Vector2(v2.x , v2.y + 1));
        attackV2List.Add(new Vector2(v2.x + 1, v2.y + 1));

        foreach (var item in attackV2List) {
            int x = (int)item.x;
            int y = (int)item.y;
            if (!BoardManager.Instance.isInBoardRange(x,y)) {
                continue;
            }
            if (!BoardManager.Instance.Chessmans[x,y]) {
                continue;
            }
            if (BoardManager.Instance.Chessmans[ x, y ].group == groupEnum.black) {
                //attackable
                DamageChassman(BoardManager.Instance.Chessmans[ x, y ]);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        BoardManager.Instance.selectedChessman.curActionVal++;
    }
}
