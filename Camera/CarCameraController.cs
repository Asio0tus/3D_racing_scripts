using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private new Camera camera;
    
    [SerializeField] private CarCameraFollow cameraFollow;
    [SerializeField] private CarCameraFovCorrector fovCorrector;    

    private void Awake()
    {
        cameraFollow.SetProperties(car, camera);
        fovCorrector.SetProperties(car, camera);
    }
}
