using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabObject : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean paintAction;
    private GameObject collidingObject; // 1
    private GameObject objectInHand; // 2
    private bool enablePainting = false;


    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        if (collidingObject.GetComponent<PaintCan>() != null)
            enablePainting = true;       
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (enablePainting)
            enablePainting = false;
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();

        }
        // 4
        objectInHand = null;
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
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // 2
        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
