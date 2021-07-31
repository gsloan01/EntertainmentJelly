using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashLight : MonoBehaviour
{
    public float flickerMaxRate;
    public float flickerMinRate;
    private float currentRate;
    private float flickerTime;

    public int maxFlickerAmount;
    private int currentAmount;

    Light light;

    public float maxBattery;
    public float currentBattery;

    public float baseIntensity = 1f;



    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.intensity = baseIntensity;
        currentRate = Random.Range(flickerMinRate, flickerMaxRate);
        currentAmount = Random.Range(1, maxFlickerAmount + 1);
    }

    // Update is called once per frame
    void Update()
    {
        flickerTime += Time.deltaTime;

        if (flickerTime > currentRate)
        {
            StartCoroutine(Flicker(currentAmount));

            currentRate = Random.Range(flickerMinRate, flickerMaxRate);
            currentAmount = Random.Range(1, maxFlickerAmount + 1);

            flickerTime = 0;
        }
    }


    private IEnumerator Flicker(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            light.intensity = 0;
            yield return new WaitForSeconds(0.02f);
            light.intensity = baseIntensity / 2;
            yield return new WaitForSeconds(0.06f);
            light.intensity = 0;
            yield return new WaitForSeconds(0.03f);
            light.intensity = baseIntensity;
        }
    }
}
