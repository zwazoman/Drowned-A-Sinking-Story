using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weakpoint : MonoBehaviour
{
    Animation a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animation>();
        GetComponent<Health>().OnDie += Die;
        GetComponent<Health>().OnDamageTaken += playAnim;
    }

    void playAnim(float b)
    {
        a.Play();
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject,.1f);
    }
}
