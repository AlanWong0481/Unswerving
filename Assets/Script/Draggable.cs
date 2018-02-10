using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    Camera myCamera;
    public Transform parentToReturnTo = null;

    private void Awake() {
        myCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
    }

   public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        this.transform.position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent( parentToReturnTo );
    }
}
