using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    private Slider slider;
    public Text HydrationCounter;
    public GameObject playerState;
    private float currentHydration, maxHydration;
    void Start()
    {
        slider = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        currentHydration = playerState.GetComponent<PlayerState>().currentHydrationPercent;
        maxHydration = playerState.GetComponent<PlayerState>().maxHydrationPercent;

        float fillValue = currentHydration / maxHydration;
        slider.value = fillValue;
        HydrationCounter.text = currentHydration + "%";
    }
}
