using System.Collections;  // Required for coroutines
using UnityEngine;

public class TriggerParticleEffect : MonoBehaviour
{
    public ParticleSystem particleEffect;  // The particle system to play
    public AudioSource blastSound;         // The audio source for the blast sound
    public GameObject objectToActivate;    // The GameObject to activate after 3 seconds
    public float restartDelay = 2f;        // Delay before restarting particle effects
    public float activationDelay = 3f;     // Delay before activating the GameObject

    Animator animator;

    // This will be called when the player enters the trigger collider

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // If the particle effect is not already playing, start the process
            if (!particleEffect.isPlaying)
            {
                animator.SetBool("IsFlag", true);
                StartCoroutine(RestartParticleEffect());
            }

            // Play the blast sound if not already playing
            if (!blastSound.isPlaying)
            {
                blastSound.Play(); // Start the sound effect
            }

            // Start the coroutine to activate the GameObject
            StartCoroutine(ActivateObjectAfterDelay());
        }
    }

    // Coroutine to restart particle effects every 2 seconds
    private IEnumerator RestartParticleEffect()
    {
        while (true)
        {
            // Play the particle system
            particleEffect.Play();

            // Play the blast sound if not already playing
            if (!blastSound.isPlaying)
            {
                blastSound.Play(); // Start the sound effect
            }

            // Wait for 2 seconds before restarting
            yield return new WaitForSeconds(restartDelay);

            // Stop the particle system before restarting it
            particleEffect.Stop();
        }
    }

    // Coroutine to activate the specified GameObject after a delay
    private IEnumerator ActivateObjectAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(activationDelay);

        // Activate the GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
