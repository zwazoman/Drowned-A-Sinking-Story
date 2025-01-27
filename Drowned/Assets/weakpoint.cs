using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class weakpoint : MonoBehaviour
{
    Animation a;
    [SerializeField] VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        vfx.Stop();
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
        vfx.transform.parent = null;
        vfx.Play();
        Destroy(vfx.gameObject,5);
        Destroy(gameObject,.05f);
    }
}
