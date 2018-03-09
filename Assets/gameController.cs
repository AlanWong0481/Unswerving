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
