using UnityEngine;
using Unity.Cinemachine;
public class CinemachineShake : MonoBehaviour
{
    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;
    private CinemachineCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        _startingIntensity = intensity;
        _shakeTimer = time;
        _shakeTimerTotal = time;
    }
    private void Update()
    {
        if (_shakeTimer > 0) 
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0) 
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.AmplitudeGain = 0;
                Mathf.Lerp(_shakeTimerTotal, .0f, 
                    _shakeTimer / _shakeTimerTotal);
                
            }
        }
    }

}
