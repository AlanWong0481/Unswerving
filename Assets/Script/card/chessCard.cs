using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class chessCard : Draggable, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public int chessNumberIndexInDatabase;
    public override void OnEndDrag(PointerEventData eventData) {
        base.OnEndDrag(eventData);
        if (!gameModel.instance.checkCostCanBeDeduct(cost)) {
            //out
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f)) {
            if (hit.transform.gameObject.tag == "plane") {
                Vector3 v3 = hit.point;
                v3 = BoardManager.Instance.getTileCenter((int)v3.x, (int)v3.z);
                if (!isinSpawnZone((int)v3.x,(int)v3.z)) {
                    return;
                }
                BoardManager.Instance.spawnChessman(chessNumberIndexInDatabase, (int)v3.x, (int)v3.z);
                Destroy(gameObject);
            }
        }
    }

    bool isinSpawnZone(int x,int y) {
        if (x > 4 || x <= 1 || y > 2) {
            return false;
        }
        return true;
    }
}
