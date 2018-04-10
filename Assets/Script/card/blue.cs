using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class blue : Draggable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public override void OnEndDrag(PointerEventData eventData) {
        Debug.Log("blue here");
        gameController.instance.OnPlayerReadyToDropDownCards();
        //this.transform.SetParent(parentToReturnTo);
        if (!gameModel.instance.checkCostCanBeDeduct(cost)) {
            //out
            return;
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("chess") ) {
            Destroy(item);
        }
    }

}
