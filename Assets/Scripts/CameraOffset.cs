using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{

    private Vector2 screenCenter;
    private void Start()
    {
        screenCenter = new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight)/2;
    }

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxCameraOffset;
    [SerializeField] private float maxMouseOffset;
    private void Update()
    {
        Vector2 mouseOffset = (Vector2)Input.mousePosition - screenCenter;

        float curOffset = mouseOffset.magnitude / maxMouseOffset;
        Vector3 newCamPos = Mathf.Clamp01(curOffset) * maxCameraOffset * mouseOffset.normalized;
        newCamPos.z = -10;

        Debug.Log("current mouse offset: " + mouseOffset.ToString() + "\ncurrent camera pos: " + newCamPos.ToString());

        mainCamera.transform.localPosition = newCamPos;
    }

}
