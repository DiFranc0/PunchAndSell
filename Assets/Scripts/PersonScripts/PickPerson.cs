using System;
using UnityEngine;

public class PickPerson : MonoBehaviour, IPickable
{

    Animator personAnimator;
    public static event Action<GameObject> OnPickedUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        personAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        OnPickedUp?.Invoke(this.gameObject);
        personAnimator.Play("Dead");
    }
}
