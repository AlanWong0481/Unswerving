using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameModel : SingletonMonoBehavior<gameModel> {
    public int costTotal;
    public int cur_Cost;

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

}
