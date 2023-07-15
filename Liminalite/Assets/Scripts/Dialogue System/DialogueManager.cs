using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float textSpeed = 0.02f;
    public KeyCode skipKey = KeyCode.Z;
    public string currentText = "";
    public AudioSource voiceAudioSource;
    public AudioClip whatever;
    public bool skipFinishesDialogue;
    private int index = 0;
    private Dialogue[] dialogues;
    private Coroutine loadRoutine;

    void Start()
    {
        textUI.text = currentText;
        index = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(skipKey))
        {
            if (skipFinishesDialogue) Whatever();
            else FastLoad();
        }
    }

    public void StartDialogue(Dialogue[] dias)
    {
        loadRoutine = StartCoroutine(LoadDialogue(dias[index]));
        dialogues = dias;
    }

    public IEnumerator LoadDialogue(Dialogue dia)
    {
        currentText = String.Format("<color=#E0FFFF>{0}: <color=\"white\">", dia.name);
        voiceAudioSource.clip = dia.voiceDub;
        voiceAudioSource.Play();
        Invoke("NextDialogue", dia.voiceDub.length);
        for (int i = 0; i < dia.text.Length; i++)
        {
            currentText += dia.text[i];
            UpdateUI();
            Debug.Log(Time.time);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextDialogue()
    {
        if (index < dialogues.Length - 1)
        {
            index += 1;
            loadRoutine = StartCoroutine(LoadDialogue(dialogues[index]));
        }
        else
        {
            CancelInvoke();
            index = 0;
            if (loadRoutine != null) StopCoroutine(loadRoutine);
            loadRoutine = null;
            voiceAudioSource.clip = null;
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        currentText = "";
        UpdateUI();
    }

    public void FastLoad()
    {
        CancelInvoke();
        if (loadRoutine == null) return;
        StopCoroutine(loadRoutine);
        currentText = String.Format("<color=#E0FFFF>{0}: <color=\"white\">{1}", dialogues[index].name, dialogues[index].text);
        UpdateUI();
        loadRoutine = null;
        voiceAudioSource.clip = null;
        NextDialogue();
    }

    private void UpdateUI()
    {
        textUI.text = currentText;
    }

    public void Whatever()
    {
        CancelInvoke();
        if (loadRoutine == null) return;
        StopCoroutine(loadRoutine);
        currentText = String.Format("<color=#E0FFFF>Sean: <color=\"white\"> Whatever...");
        UpdateUI();
        loadRoutine = null;
        voiceAudioSource.clip = whatever;
        voiceAudioSource.Play();
        currentText = "";
        Invoke("UpdateUI", whatever.length);
    }
}
