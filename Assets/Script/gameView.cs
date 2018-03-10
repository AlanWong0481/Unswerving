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
}
