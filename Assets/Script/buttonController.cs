using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour
{
    public string dir = "";

    public void OnbuttonMoveUp()
    {
        print(dir);
    }
}
