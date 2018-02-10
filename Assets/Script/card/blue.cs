using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class blue : Draggable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public override void OnEndDrag(PointerEventData eventData) {
        Debug.Log("blue here");
        
        foreach (var item in GameObject.FindGameObjectsWithTag("chess") ) {
            Destroy(item);
        }
        this.transform.SetParent(parentToReturnTo);
    }

}
