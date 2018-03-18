using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum dir {
    up,
    down,
    left,
    right
} 

public class buttonController : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler {
    public dir buttonDir;

    public void OnbuttonMoveUp()
    {
        BoardManager.Instance.uiMovement(buttonDir);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("Mouse enter");
        gameController.instance.isPlayerOverTheFingerControl = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("Mouse exit");
        gameController.instance.isPlayerOverTheFingerControl = false;
    }

}
