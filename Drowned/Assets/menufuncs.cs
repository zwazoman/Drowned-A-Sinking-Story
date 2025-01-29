using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class menufuncs : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = FindObjectOfType<Slider>();
    }

    public void RustyHeight()
    {
        Application.OpenURL("https://store.steampowered.com/app/3352440/Rusty_Heights/");
    }

    public void Discord()
    {
        Application.OpenURL("https://discord.gg/xyXrqrXMV6");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateSound()
    {
        AudioListener.volume = slider.value;
    }
}
