using Cinemachine;
using UnityEngine;

public class CineMachineFreelookDynamic : MonoBehaviour
{
    [SerializeField]
    private Transform focusObjectTransform;

    private CinemachineFreeLook cinemachineFreeLook;

    private void Awake()
    {
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }
        brain.m_DefaultBlend.m_Time = 1;
        brain.m_ShowDebugText = true;

        cinemachineFreeLook = gameObject.AddComponent<CinemachineFreeLook>();

        cinemachineFreeLook.Follow = focusObjectTransform;
        cinemachineFreeLook.LookAt = focusObjectTransform;
        cinemachineFreeLook.Priority = 2;

        cinemachineFreeLook.m_SplineCurvature = 3;

        CinemachineVirtualCamera toprig = cinemachineFreeLook.GetRig(0);
        CinemachineVirtualCamera midrig = cinemachineFreeLook.GetRig(1);
        CinemachineVirtualCamera botrig = cinemachineFreeLook.GetRig(2);

        toprig.AddCinemachineComponent<CinemachineTransposer>();

        CinemachineBasicMultiChannelPerlin noise = toprig.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 0; //Can add stuff
        noise.m_FrequencyGain = 0; // can add stuff

        CinemachineCollider cinemachineCollider = gameObject.AddComponent<CinemachineCollider>();

    }
}
