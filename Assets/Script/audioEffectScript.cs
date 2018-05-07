using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioEffectScript : SingletonMonoBehavior<audioEffectScript> {
    [HideInInspector]
    public AudioSource allGameSoundEffect;
    public AudioClip[] inGameAudio;
    public bool soundLocker;

    public override void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        base.Awake();
    }

    // Use this for initialization
    void Start() {
        allGameSoundEffect = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        //Bgm();
    }


    public void Bgm() {
        //allGameSoundEffect.clip = inGameAudio[0];
        //allGameSoundEffect.Play();
    }

    public void attackBoy() {
        if (soundLocker) {
            return;
        }
        StartCoroutine(playDelay(inGameAudio[ 0 ], 0.3f, 1));

    }
    public void attackEnemy() {
        if (soundLocker) {
            return;
        }
        StartCoroutine(playDelay(inGameAudio[ 1 ], 0, 1));

    }
    public void attackGirl() {
        if (soundLocker) {
            return;
        }
        StartCoroutine(playDelay(inGameAudio[ 2 ], 0.3f, 1));

    }


    IEnumerator playDelay(AudioClip clip, float delay, float volScale) {
        yield return new WaitForSeconds(delay);
        
            allGameSoundEffect.PlayOneShot(clip, volScale);

    }
}
