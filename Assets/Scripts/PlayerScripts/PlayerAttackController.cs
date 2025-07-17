using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    Animator playerAnimator;
    AudioSource playerAudioSource;
    Animator currentPersonAnimator;
    PlayerInput playerInput;

    [SerializeField] string attackAnimationTrigger = "isPunching";
    [SerializeField] string personTag = "Person";
    [SerializeField] float punchForce = 40f; // Force applied to the person when attacked
    


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a person
        if (other.CompareTag(personTag))
        {
            // Call the method to handle the attack on the person
            AttackOnPerson(other.gameObject);
        }
    }

    private void AttackOnPerson(GameObject person)
    {
        currentPersonAnimator = person.GetComponent<Animator>();
        person.GetComponent<Collider>().enabled = false; // Disable the collider to prevent further interactions during the attack

        playerInput.DeactivateInput();
        transform.LookAt(person.transform);

        playerAnimator.SetTrigger(attackAnimationTrigger);

     


    }

    public void PlayPunchAudio()
    {
        if (playerAudioSource != null)
        {
            playerAudioSource.Play(); // Play the punch sound
        }

        
    }

    public void PlayOuchSound()
    {
        currentPersonAnimator.GetComponent<AudioSource>()?.Play(); // Play the person's hit sound
    }

    private void ActivatePersonRagdoll()
    {
        currentPersonAnimator.enabled = false;
        Rigidbody[] personRigidbodies = currentPersonAnimator.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in personRigidbodies)
        {
            rb.isKinematic = false; // Enable physics for the ragdoll
            rb.useGravity = true; // Enable gravity for the ragdoll

            rb.AddForce(transform.forward * punchForce, ForceMode.Impulse);
            
        }
       
        playerInput.ActivateInput();

        currentPersonAnimator.gameObject.GetComponent<IPickable>()?.PickUp(); // Call the PickUp method if the person implements IPickable

    }

    
}
