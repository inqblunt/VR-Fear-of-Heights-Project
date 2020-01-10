using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : Interactable
{
    private ItemSocket socket = null;

    private void Awake()
    {
        socket = GetComponent<ItemSocket>();
    }

    public override void StartInteraction(InteractingHand hand)
    {
        if (hand.HasHeldObject())
        {
            TryStore(hand);
        }
        else
        {
            TryRetrieve(hand);
        }
    }

    private void TryStore(InteractingHand hand)
    {
        if (socket.GetStoredObject())
            return;

        Moveable objectToStore = hand.Drop();
        objectToStore.AttachNewSocket(socket);
    }

    private void TryRetrieve(InteractingHand hand)
    {
        if (!socket.GetStoredObject())
            return;

        Moveable objectToRetrieve = socket.GetStoredObject();
        hand.Pickup(objectToRetrieve);
    }
}
