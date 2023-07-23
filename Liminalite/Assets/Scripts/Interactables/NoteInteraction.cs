using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteInteraction : MonoBehaviour, IExaminable
{
    public Note note;
    public TextMeshProUGUI targetUI;
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI authorUI;
    [SerializeField] PlayerController player; 
    [SerializeField] private Vector3 originalPos;
    [SerializeField] private Vector3 originalRotation;
    
    void Awake()
    {
        targetUI.text = note.targetName;
        textUI.text = note.text;
        authorUI.text = note.authorName;
        originalPos = transform.position;
        originalRotation = transform.eulerAngles;
    }

    public void Examine() {
        Vector3 socket = Camera.main.transform.position + player.playerCamera.transform.TransformDirection(Vector3.forward);
        Vector3 angles = player.playerCamera.transform.eulerAngles;
        LeanTween.move(gameObject, socket, .5f).setEaseOutCubic();
        LeanTween.rotate(gameObject, angles, .5f).setEaseOutCubic();
    }

    public void Leave()
    {
        LeanTween.move(gameObject, originalPos, .5f).setEaseOutCubic();
        LeanTween.rotate(gameObject, originalRotation, .5f).setEaseOutCubic().setOnComplete(player.LeaveExamining);
    }
}
