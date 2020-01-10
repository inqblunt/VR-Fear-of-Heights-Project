using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : Interactable
{
    private ItemSocket activeSocket = null;

    public override void StartInteraction(InteractingHand hand)
    {
        hand.Pickup(this);
    }

    public override void Interaction(InteractingHand hand)
    {
        
    }

    public override void EndInteraction(InteractingHand hand)
    {
        hand.Drop();
    }

    public void AttachNewSocket(ItemSocket newSocket)
    {
        if (newSocket.GetStoredObject())
            return;

        ReleaseOldSocket();
        activeSocket = newSocket;

        activeSocket.Attach(this);
        isAvailable = false;
    }

    public void ReleaseOldSocket()
    {
        if (!activeSocket)
            return;

        activeSocket.Detach();

        activeSocket = null;
        isAvailable = true;
    }
}
