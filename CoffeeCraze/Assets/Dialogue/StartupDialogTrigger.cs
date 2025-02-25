using UnityEngine;

public class StartupDialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueAsset startupDialogue;
    [SerializeField] private DialoguePanel dialoguePanel;
    
    private void Start()
    {
        // Small delay to ensure everything is loaded
        Invoke("TriggerStartupDialogue", 0.1f);
    }
    
    private void TriggerStartupDialogue()
    {
        if (dialoguePanel != null && startupDialogue != null)
        {
            dialoguePanel.ShowDialogue(startupDialogue);
        }
        else
        {
            Debug.LogError("Missing DialoguePanel or DialogueAsset reference!");
        }
    }
}
