using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSocket : MonoBehaviour
{
    private Moveable storedObject = null;
    private FixedJoint joint = null;

    // Start is called before the first frame update
    private void Awake()
    {
        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    public void Attach(Moveable newObject)
    {
        if (storedObject)
            return;

        storedObject = newObject;

        storedObject.transform.position = transform.position;
        storedObject.transform.rotation = transform.rotation;

        Rigidbody targetBody = storedObject.gameObject.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;
    }

    public void Detach()
    {
        if (!storedObject)
            return;

        joint.connectedBody = null;
        storedObject = null;
    }

    public Moveable GetStoredObject()
    {
        return storedObject;
    }
}
