using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText; // Assign in Inspector
    [SerializeField] private Button nextButton; // Assign in Inspector

    private Queue<string> dialogueQueue = new Queue<string>(); // Queue for dialogue

    public void ShowDialogue(DialogueAsset dialogueAsset)
    {
        if (dialogueAsset == null || dialogueAsset.dialogue.Length == 0)
        {
            Debug.LogError("DialogueAsset is empty or null!");
            return;
        }

        gameObject.SetActive(true); // Show the panel
        Time.timeScale = 0; // Pause game

        dialogueQueue.Clear();
        foreach (string line in dialogueAsset.dialogue)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
        //nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    private void DisplayNextLine()
    {
        if (dialogueQueue.Count > 0)
        {
            dialogueText.text = dialogueQueue.Dequeue();
        }
        else
        {
            ClosePanel();
        }
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1; // Resume game
    }
}
