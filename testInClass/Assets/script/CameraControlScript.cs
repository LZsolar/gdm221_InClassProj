using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.Audio;

public class CameraControlScript : MonoBehaviour
{
    public GameObject playerCamObj, finishCamObj;
    public GameObject mainCamera, character;
    public TMP_Text welcomeText;
    private CinemachineBrain cinemachineBrain;
    bool isGameStarted = false;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    // Start is called before the first frame update
    void Start()
    {
        paused.TransitionTo(.05f);
        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
        welcomeText.SetText("Get the BANANA!");
        character.GetComponent<CharacterScript>().enabled = false;
        finishCamObj.SetActive(true);
        playerCamObj.SetActive(false);
        StartCoroutine(FinishToPlayerCam());
    }

    IEnumerator FinishToPlayerCam()
    {
        yield return new WaitForSeconds(2);
        finishCamObj.SetActive(false);
        playerCamObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cinemachineBrain.IsBlending)
        {
            ICinemachineCamera finishCam = finishCamObj.GetComponent<ICinemachineCamera>();
            bool finishCamLive = CinemachineCore.Instance.IsLive(finishCam);
            if (!finishCamLive && !isGameStarted)
            {
                isGameStarted = true;
                character.GetComponent<CharacterScript>().enabled = true;
                StartCoroutine(DisplayTextWithDelay(welcomeText, "GO!!!", 2.0f));
                unpaused.TransitionTo(.05f);
            }
        }
    }

    IEnumerator DisplayTextWithDelay(TMP_Text textObj, string text, float delay)
    {
        textObj.SetText(text);
        yield return new WaitForSeconds(delay);
        textObj.gameObject.SetActive(false);
    }
 
}
