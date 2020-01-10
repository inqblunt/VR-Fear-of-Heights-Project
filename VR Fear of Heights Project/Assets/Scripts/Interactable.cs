using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isAvailable = true;

    public virtual void StartInteraction(InteractingHand hand)
    {
        print("Start");
    }
    public virtual void Interaction(InteractingHand hand)
    {
        print("Interaction");
    }
    public virtual void EndInteraction(InteractingHand hand)
    {
        print("End");
    }

    public bool GetAvailability()
    {
        return isAvailable;
    }
}
