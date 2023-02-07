using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFovCorrector : MonoBehaviour
{
    [SerializeField] private Car carPlayer;
    [SerializeField] private new Camera camera;

    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldOfView;

    private float defaultFov;

    private void Start()
    {
        camera.fieldOfView = defaultFov;
    }

    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, carPlayer.NormilizeLinearVelocity);
    }
}
