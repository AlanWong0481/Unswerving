using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle : Chessman {
    public void itemEvent() {
        BoardManager.Instance.selectedChessman.resetActionVal();
        BoardManager.Instance.selectedChessman.curActionVal++;
    }
}
