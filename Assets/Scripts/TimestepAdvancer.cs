using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimestepAdvancer : MonoBehaviour {

    public float timeStep = 1f;

    private ChuckSubInstance chuckSubInstance;
    private ChuckEventListener myAdvancerListener;
    private ChuckFloatSyncer myFloatSyncer;
    private int timeStepCount = 0;

    private List<LightController> lightsInScene;
    private List<SoundObject> soundsInScene;

    // Use this for initialization
    private void Start() {
        chuckSubInstance = gameObject.GetComponent<ChuckSubInstance>();
        myFloatSyncer = gameObject.AddComponent<ChuckFloatSyncer>();
        myAdvancerListener = gameObject.AddComponent<ChuckEventListener>();

        lightsInScene = new List<LightController>(FindObjectsOfType<LightController>());
        soundsInScene = new List<SoundObject>(FindObjectsOfType<SoundObject>());

        StartChuckTimer();
    }

    private void StartChuckTimer() {
        chuckSubInstance.RunCode(@"
			1 => global float timeStep;
			global Event notifier;
			
			while( true )
			{
				notifier.broadcast();
				timeStep::second => now;
			}
		");

        myAdvancerListener.ListenForEvent(chuckSubInstance, "notifier", TimeStepDone);
    }

    public void TimeStepDone() {
        TriggerSoundObjects();
        TriggerLights();
        timeStepCount++;
    }

    private void TriggerLights() {
        foreach (LightController light in lightsInScene) {
            if (Mathf.Abs(timeStepCount - 1) % light.triggerEvery == 0) {
                light.Trigger();
            }
        }
    }

    public void AddLight(LightController light) {
        lightsInScene.Add(light);
    }

    private void TriggerSoundObjects() {
        for (int i = 0; i < soundsInScene.Count; i++) {
            for (int j = 0; j < soundsInScene[i].triggerEveryList.Count; j++) {
                if (timeStepCount % soundsInScene[i].triggerEveryList[j] == 0) {
                    chuckSubInstance.RunFile(soundsInScene[i].chuckFileName);
                    soundsInScene[i].Activate();
                    break;
                }
            }
        }
    }

    public void AddSoundObject(SoundObject soundObject) {
        soundsInScene.Add(soundObject);
    }

    private void Update () {
        chuckSubInstance.SetFloat("timeStep", timeStep);
    }
}
