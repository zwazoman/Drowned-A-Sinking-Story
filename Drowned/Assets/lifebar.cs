using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifebar : MonoBehaviour
{
    [SerializeField] Health health;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out slider);
        slider.maxValue = health._maxHP;
        slider.value = health._currentHP;
        health.OnDamageTaken += (float a) => slider.value = health._currentHP;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
