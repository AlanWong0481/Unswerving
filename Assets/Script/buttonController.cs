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

public class buttonController : MonoBehaviour
{
    public dir buttonDir;

    public void OnbuttonMoveUp()
    {
        BoardManager.Instance.uiMovement(buttonDir);
    }
}
