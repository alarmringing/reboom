using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundWasteType { BigDrum, HiHat };

public class SoundObject : MonoBehaviour {

    public ParticleSystem particleSystem;
    public SoundWasteType soundWasteType;
    [HideInInspector]
    public string chuckFileName;


    private List<LightController> lightsInScene;
    private float maxActivation = 5f;
    private float decayDuration = 0.4f;

    [HideInInspector]
    public List<int> triggerEveryList;

    private void Start () {
        triggerEveryList = new List<int>();

        switch(soundWasteType) {
            case SoundWasteType.BigDrum:
                chuckFileName = "BigDrum.ck";
                return;
            case SoundWasteType.HiHat:
                chuckFileName = "HiHat.ck";
                return;
        }
    }

    public void Activate() {

        // Emit Sound
        particleSystem.Play();
        // TODO: Emit Light
        // flickerLight.intensity = maxActivation;
        //timeSinceFlickerOnset = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        LightController lightController = other.transform.GetComponent<LightController>();
        if (lightController) {
            int newTriggerEvery = lightController.triggerEvery;
            for (int i = 0; i < triggerEveryList.Count; i++) {
                if (newTriggerEvery % triggerEveryList[i] == 0) return;
            }
            triggerEveryList.Add(newTriggerEvery);
        }
    }

    void OnTriggerExit(Collider other)
    {
        LightController lightController = other.transform.GetComponent<LightController>();
        if (lightController) {
            triggerEveryList.Remove(lightController.triggerEvery);
        }
    }

    private void Update () {
        /*
        if (flickerLight.intensity > 0) {
            float t = Mathf.Clamp01(decayDuration - timeSinceFlickerOnset);
            // flickerLight.intensity = Mathf.Lerp(0, maxActivation, t);
            timeSinceFlickerOnset += Time.deltaTime;
        }
        */
    }
}
