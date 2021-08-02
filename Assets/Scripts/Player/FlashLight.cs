using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class FlashLight : MonoBehaviour
{
    public float flickerMaxRate;
    public float flickerMinRate;
    private float currentRate;
    private float flickerTime;

    public int maxFlickerAmount;
    private int currentAmount;

    Light light;

    public float batteryLifetime;
    private float currentBattery;

    public float MaxLifetimeInSeconds
    {
        get { return batteryLifetime * 60; }
    }

    public float baseIntensity = 1f;

    private bool lightOn = true;

    AudioSource audio;
    public AudioClip onSound;
    public AudioClip offSound;

    public Slider batterySlider;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        light = GetComponent<Light>();
        light.intensity = baseIntensity;
        currentRate = Random.Range(flickerMinRate, flickerMaxRate);
        currentAmount = Random.Range(1, maxFlickerAmount + 1);

        Charge();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentBattery > 0) Toggle();
        batterySlider.value = (currentBattery / MaxLifetimeInSeconds);

        if (lightOn)
        {
            HandleFlicker();
            currentBattery -= Time.deltaTime;
            

            if (currentBattery <= 0)
            {
                TurnOff(false);
            }
        }
    }

    public void Charge()
    {
        currentBattery = MaxLifetimeInSeconds;
    }

    private void Toggle()
    {
        if (lightOn)
        {
            TurnOff(true);
        } else
        {
            TurnOn(true);
        }
    }

    private void TurnOff(bool useAudio)
    {
        light.intensity = 0;
        lightOn = false;


        if (useAudio)
        {
            audio.clip = offSound;
            audio.Play();
        }
    }

    private void TurnOn(bool useAudio)
    {
        light.intensity = baseIntensity;
        lightOn = true;

        if (useAudio)
        {
            audio.clip = onSound;
            audio.Play();
        }
        
    }

    private void HandleFlicker()
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
            TurnOff(false);
            yield return new WaitForSeconds(0.02f);
            light.intensity = baseIntensity / 2;
            yield return new WaitForSeconds(0.06f);
            TurnOff(false);
            yield return new WaitForSeconds(0.03f);
            TurnOn(false);
        }
    }
}
