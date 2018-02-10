using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory : MonoBehaviour {

    public GameObject go;

    public void Kills()
    {
        Destroy(go);
    }
}
