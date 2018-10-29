using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    private Light flickerLight;
    private float maxIntensity = 8f;
    private float decayDuration = 0.7f;
    private float timeSinceFlickerOnset = 0f;

    // Controlled by Chuck
    public int offset = 0;
    public int triggerEvery = 1;

    private void Start () {
        flickerLight = GetComponent<Light>();
        Debug.Assert(flickerLight != null);
        flickerLight.intensity = 0;
	}

    public void Trigger() {
        flickerLight.intensity = maxIntensity;
        timeSinceFlickerOnset = 0;
    }
	
	private void Update () {
		if (flickerLight.intensity > 0) {
            float t = Mathf.Pow(Mathf.Clamp01(decayDuration - timeSinceFlickerOnset), 2f);
            flickerLight.intensity = Mathf.Lerp(0, maxIntensity, t);
            timeSinceFlickerOnset += Time.deltaTime;
        }
	}
}
