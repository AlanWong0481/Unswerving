using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    Camera myCamera;
    //public Transform parentToReturnTo = null;
    public Vector2 begainDragV2;

    public int cost;

    private void Awake() {
        myCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        begainDragV2 = GetComponent<RectTransform>().anchoredPosition;
        gameController.instance.OnPlayerPickUpCards(this);
        gameModel.instance.inPlayerDragSomeCard = true;
        Debug.Log("OnBeginDrag");

        //parentToReturnTo = this.transform.parent;
        //this.transform.SetParent(this.transform.parent.parent);
    }

   public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        this.transform.position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition = begainDragV2;
        gameModel.instance.inPlayerDragSomeCard = false;
    }
}
