using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedEgg : MonoBehaviour
{
    Transform child1;
    Transform child2;

    SpriteRenderer sr1;
    SpriteRenderer sr2;

    Vector2 target1;
    Vector2 target2;

    void Start()
    {
        child1 = transform.GetChild(0);
        child2 = transform.GetChild(1);
        sr1 = child1.gameObject.GetComponent<SpriteRenderer>();
        sr2 = child2.gameObject.GetComponent<SpriteRenderer>();
        GetRandomTargets();
    }

    void GetRandomTargets() {
        target1 = new Vector2(child1.transform.position.x + Random.Range(-1.0f, 1.0f), child1.transform.position.y + Random.Range(0.25f, 1.0f));
        target2 = new Vector2(child2.transform.position.x + Random.Range(-1.0f, 1.0f), child2.transform.position.y - Random.Range(0.25f, 1.0f));
    }

    void DecreaseOpacity(SpriteRenderer sr) {
        if(sr.color.a >= 0.0f) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime);
        }
        else {
            Destroy(gameObject);
        }
    }

    void MoveTowards(Transform tf, Vector2 target) {
        float step = 2.0f * Time.deltaTime;
        tf.position = Vector2.MoveTowards(tf.position, target, step);
    }

    void Update()
    {
        DecreaseOpacity(sr1);
        DecreaseOpacity(sr2);   
        MoveTowards(child1, target1);  
        MoveTowards(child2, target2);
    }
}
