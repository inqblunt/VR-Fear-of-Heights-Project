using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Belt : MonoBehaviour
{
    [Range(0.5f, 0.75f)]
    public float height = 0.5f;

    private Transform head = null;

    public Camera mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        head = mainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PositionUnderHead();
        RotateWithHead();
    }

    private void PositionUnderHead()
    {
        Vector3 adjustedHeight = head.localPosition;
        adjustedHeight.y = Mathf.Lerp(0.0f, adjustedHeight.y, height);

        transform.localPosition = adjustedHeight;
    }

    private void RotateWithHead()
    {
        Vector3 adjustedRotation = head.localEulerAngles;
        adjustedRotation.x = 0;
        adjustedRotation.z = 0;

        transform.localEulerAngles = adjustedRotation;
    }
}
