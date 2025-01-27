using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class hitVFX : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        if (vfx == null) TryGetComponent(out vfx);
        vfx.Stop();
        health.OnDamageTaken += (float a) => vfx.Play();
    }

}
