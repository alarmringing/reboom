using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float spawnHeight = 10f;

    public Camera topDownCamera;
    public Camera characterCamera;
    public ChuckMainInstance chuckMainInstance;
    public GameObject monitor;
    public GameObject cellPhone;
    public TimestepAdvancer timestepAdvancer;

    private SoundWasteType soundTypeToGen = SoundWasteType.BigDrum;

	void Start () {
        topDownCamera.enabled = false;
        characterCamera.enabled = true;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) {
            topDownCamera.enabled = !topDownCamera.enabled;
            characterCamera.enabled = !characterCamera.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            soundTypeToGen = SoundWasteType.BigDrum;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("HiHat pressed");
            soundTypeToGen = SoundWasteType.HiHat;
        }

        if (Input.GetMouseButtonDown(0) && topDownCamera.enabled) {
            RaycastHit hit;
            var ray = topDownCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPoint = new Vector3(hit.point.x, spawnHeight, hit.point.z);
                GameObject objectToGen = monitor;
                switch(soundTypeToGen)
                {
                    case SoundWasteType.BigDrum:
                        objectToGen = monitor;
                        break;
                    case SoundWasteType.HiHat:
                        objectToGen = cellPhone;
                        break;
                }
                GameObject newObj = Instantiate(objectToGen, spawnPoint, Random.rotation);
                newObj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                SoundObject newSoundObject = newObj.GetComponent<SoundObject>();
                newSoundObject.soundWasteType = soundTypeToGen;
                timestepAdvancer.AddSoundObject(newSoundObject);
            }
        }
    }
}
