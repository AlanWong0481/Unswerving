﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class redTwo : Draggable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        gameController.instance.OnPlayerReadyToDropDownCards();
        //this.transform.SetParent(parentToReturnTo);
        if (!gameModel.instance.checkCostCanBeDeduct(cost)) {
            //out
            return;
        }

        Debug.Log("red here");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform.gameObject.tag == "plane")
            {
                Vector3 v3 = hit.point;
                v3 = BoardManager.Instance.getTileCenter((int)v3.x, (int)v3.z);
                BoardManager.Instance.spawnChessman(0, (int)v3.x, (int)v3.z);
            }
        }
    }
}
