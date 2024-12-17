using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UrgentPhrases : MonoBehaviour
{
    public Text textDisplay; // Drag the Text UI element here
    public string[] phrases = { "Friedrich!", "Where are you?", "Salome is calling!", "Deranged again...?" }; // Phrases to display
    public float delayBetweenPhrases = 1.5f; // Time between phrases
    public Color urgentColor = Color.red; // Main urgent color
    public Color darkUrgentColor = new Color(0.5f, 0, 0); // Darker red for shading effect

    public AudioClip urgentAudio; // Drag your audio clip here
    private AudioSource audioSource;

    void Start()
    {
        // Font size and alignment
        textDisplay.fontSize = 100; // Larger, more urgent text
        textDisplay.alignment = TextAnchor.MiddleCenter; // Center the text
        textDisplay.fontStyle = FontStyle.Bold;

        // Audio setup
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = urgentAudio;
        audioSource.loop = false;

        StartCoroutine(ShowPhrases());
    }

    IEnumerator ShowPhrases()
    {
        for (int i = 0; i < phrases.Length; i++)
        {
            // Update the phrase and play audio
            textDisplay.text = phrases[i];
            StartCoroutine(FlashUrgentColors());
            PlayUrgentAudio();

            yield return new WaitForSeconds(delayBetweenPhrases); // Delay for next phrase
        }

        // Final phrase stays on screen
        textDisplay.text = "Deranged again...?";
        textDisplay.color = urgentColor; // Keep urgent red
        PlayUrgentAudio(); // Play one last time
    }

    IEnumerator FlashUrgentColors()
    {
        // Flash between red and dark red for added urgency
        for (int i = 0; i < 6; i++) // Flash 6 times
        {
            textDisplay.color = urgentColor;
            yield return new WaitForSeconds(0.15f);
            textDisplay.color = darkUrgentColor;
            yield return new WaitForSeconds(0.15f);
        }

        textDisplay.color = urgentColor; // End on red
    }

    void PlayUrgentAudio()
    {
        // Play the audio with a 2-second interval
        if (urgentAudio != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Invoke("StopAudio", urgentAudio.length); // Stop audio after it plays
        }
    }

    void StopAudio()
    {
        audioSource.Stop();
    }
}
