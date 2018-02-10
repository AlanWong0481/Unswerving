using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemControllerWindow : MonoBehaviour
{
    public float Distance = 10;

    ParticleSystem system
	{
		get
		{
			if (_CachedSystem == null)
				_CachedSystem = GetComponent<ParticleSystem>();
			return _CachedSystem;
		}
	}
	private ParticleSystem _CachedSystem;

	public Rect windowRect = new Rect(0, 0, 300, 120);

	public bool includeChildren = true;

	void Update()
	{
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); //捉取滑鼠位置
        Vector3 pos = r.GetPoint(Distance);
        transform.position = pos;

        if (Input.GetMouseButtonDown(0))
			{
			system.Play(includeChildren);

			}
		if(Input.GetMouseButtonUp(0))
			{
				system.Stop(includeChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
			}
	}
}