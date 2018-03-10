using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDisplay : MonoBehaviour {
    public static damageDisplay instance;
    SpriteRenderer myImage;
    public Sprite[] mySprites;

    public GameObject simplePopupObject;

    public GameObject simplePopupParentObject;
    [SerializeField]
     float textDistance = 0.75f;
    [SerializeField]
    float existTime = 5.0f;
    [SerializeField]
    float forceHeight;
    [SerializeField]
    float randomDisForce;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    public void spawnDamageDisplay(int damage, int type, Transform popupValueObject) {
        GameObject NumberParent = Instantiate(simplePopupParentObject, popupValueObject);
        NumberParent.transform.localPosition = new Vector3();
        NumberParent.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-randomDisForce, randomDisForce), forceHeight, Random.Range(-randomDisForce, randomDisForce)));

        short digits = 0;
        if (damage != 0) {
            digits = (short)getDigits(damage, 0);
        } else {
            digits = 1;
        }
        float spawnXAxisLimit = ((textDistance * digits) / 2) - (textDistance / 2);
        //work the damage to digits function is work
        int number = damage;
        for (int i = 0; i < digits; i++) {
            GameObject emptyGameObject = Instantiate(simplePopupObject, NumberParent.transform);
            myImage = emptyGameObject.GetComponent<SpriteRenderer>();

            emptyGameObject.transform.localPosition = new Vector3(spawnXAxisLimit - (textDistance * i), 0, 0);
            ChangeSprite(number % 10, type);
            number /= 10;

            emptyGameObject.transform.position -= emptyGameObject.transform.forward;
        }

        StartCoroutine(SpriteParentEnumerator(NumberParent));
    }

    static int getDigits(int n1, int nodigits) {
        if (n1 == 0)
            return nodigits;

        return getDigits(n1 / 10, ++nodigits);
    }


    void ChangeSprite(int number, int type) {
        myImage.sprite = mySprites[ number + (type * 10) ];
    }


    IEnumerator SpriteParentEnumerator(GameObject SpriteParent) {
        yield return new WaitForSeconds(existTime);
        Destroy(SpriteParent);
    }

}
