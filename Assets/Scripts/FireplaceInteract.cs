using UnityEngine;
using FMODUnity;

public class FireplaceInteract : MonoBehaviour, IInteractable
{
    [Header("Ognisko")] 
    [SerializeField] GameObject ognisko;
    [Header("Dźwięki")] 
    [SerializeField] private EventReference fireplaceStart;
    [SerializeField] private EventReference fireplaceStop;
    [Header("Stan")]
    [SerializeField] private bool isActive = true;
    public void Interact()
    {
        isActive = !isActive;
        if (ognisko != null)
        {
            ognisko.SetActive(isActive);
            PlayInteractSound();
        }
    }
    private void PlayInteractSound()
    {
        if (isActive)
        {
            RuntimeManager.PlayOneShot(fireplaceStart);
        }
        else
        {
            RuntimeManager.PlayOneShot(fireplaceStop);
        }
    }
    
}
