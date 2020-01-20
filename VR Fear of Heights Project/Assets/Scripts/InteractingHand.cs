using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InteractingHand : MonoBehaviour
{
    private ItemSocket socket = null;
    private SteamVR_Behaviour_Pose pose = null;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean paintAction;
    public SteamVR_Input_Sources handType;

    public List<Interactable> contactInteractables = new List<Interactable>();

    private bool enablePainting = false;

    private void Awake()
    {
        socket = GetComponent<ItemSocket>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AddInteractable(other.gameObject);
    }

    private void AddInteractable(GameObject newObject)
    {
        Interactable newInteractable = newObject.GetComponent<Interactable>();
        contactInteractables.Add(newInteractable);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveInteractable(other.gameObject);
    }

    private void RemoveInteractable(GameObject newObject)
    {
        Interactable existingInteractable = newObject.GetComponent<Interactable>();
        contactInteractables.Remove(existingInteractable);
    }

    public void TryInteraction()
    {
        if (NearestInteraction())
            return;

        HeldInteraction();
    }

    private bool NearestInteraction()
    {
        contactInteractables.Remove(socket.GetStoredObject());
        Interactable nearestObject = Utility.GetNearestInteractable(transform.position, contactInteractables);

        if (nearestObject)
            nearestObject.StartInteraction(this);

        return nearestObject;
    }

    private void HeldInteraction()
    {
        if (!HasHeldObject())
            return;

        Moveable heldObject = socket.GetStoredObject();
        heldObject.Interaction(this);
    }

    private void StopInteraction()
    {
        if (!HasHeldObject())
            return;

        Moveable heldObject = socket.GetStoredObject();
        heldObject.EndInteraction(this);
    }

    public void Pickup(Moveable moveable)
    {
        moveable.AttachNewSocket(socket);
        if (moveable.GetComponent<PaintCan>() != null)
            enablePainting = true;
    }

    public Moveable Drop()
    {
        if (!HasHeldObject())
            return null;

        Moveable detachedObject = socket.GetStoredObject();
        if (detachedObject.GetComponent<PaintCan>() != null)
            enablePainting = false;
        detachedObject.ReleaseOldSocket();

        Rigidbody rigidbody = detachedObject.gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = pose.GetVelocity();
        rigidbody.angularVelocity = pose.GetAngularVelocity();

        return detachedObject;
    }

    public bool HasHeldObject()
    {
        return socket.GetStoredObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (paintAction.GetLastStateDown(handType) && enablePainting)
        {
            PaintManager manager = GameManager.GetInstance().GetPaintManager();
            for (int i = 0; i < 14; ++i)
            {
                RaycastHit hit;
                Moveable objectInHand = socket.GetStoredObject();
                if (Physics.Raycast(objectInHand.GetComponent<PaintCan>().paintPoint.transform.position, objectInHand.GetComponent<PaintCan>().paintPoint.transform.forward, out hit))
                {
                    if (hit.collider is MeshCollider)
                    {
                        PaintCanvas script = hit.collider.gameObject.GetComponent<PaintCanvas>();
                        if (null != script)
                        {
                            script.PaintOnColored(hit.textureCoord, manager.GetRandomProjectileSplash(), objectInHand.GetComponent<PaintCan>().paintColor);
                        }
                    }
                }
            }
        }
        // 1
        if (grabAction.GetLastStateDown(handType))
        {
            TryInteraction();
        }

        // 2
        if (grabAction.GetLastStateUp(handType))
        {
            StopInteraction();
        }
    }
}
