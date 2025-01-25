using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirBar : MonoBehaviour
{
    Slider s;
    private void Start()
    {
        TryGetComponent(out s);
        s.maxValue = FishController.Instance._aimingControls._maxAir;
    }
    private void Update()
    {
        s.value = FishController.Instance._aimingControls.Air;
    }
}
