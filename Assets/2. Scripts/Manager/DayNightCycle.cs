using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // 0 ~ 1 사이의 값으로 하루 시간 표현
    [Range(0.0f, 1.0f)]
    public float time;
    public float dayLength; // 하루의 길이 (초)
    public float startTime = 0.4f; // 시작 시간 (0~1)
    private float timeRate; // 시간 증가 속도
    public Vector3 noon; // 정오(낮) 방향 벡터 설정

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor; // 시간에 따른 햇빛 색상 변화
    public AnimationCurve sunIntensity; // 시간에 따른 햇빛량 변화

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor; // 시간에 따른 달빛 색상 변화
    public AnimationCurve moonIntensity; // 시간에 따른 달빛량 변화

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier; // 환경 조명 강도 변화
    public AnimationCurve reflectionIntensityMultiplier; // 반사 강도 변화

    [Header("Skybox Settings (Procedural)")]
    public AnimationCurve skyboxAtmosphereThickness; // 노을 효과 적용

    [Header("---")]
    public GameObject warningTxt;
    bool isSpawn = false;

    private void Start()
    {
        // 기존 Skybox 에셋을 복제해서 사용 (원본 에셋 변경 방지)
        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox = new Material(RenderSettings.skybox);
        }

        // 하루 길이에 따라 시간 증가 속도 설정 (1초에 1/dayLength 만큼 증가)
        timeRate = 1.0f / dayLength;
        time = startTime; // 시작 시간 초기화
    }

    private void Update()
    {
        // 시간 업데이트: 0 ~ 1 사이의 값으로 순환
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        // 햇빛과 달빛 업데이트 (각각 0.25, 0.75의 시간 오프셋 사용)
        UpdateLighting(sun, sunColor, sunIntensity, 0.25f);
        UpdateLighting(moon, moonColor, moonIntensity, 0.75f);

        // 환경 조명 및 반사 강도 업데이트
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

        // Procedural Skybox 업데이트 (Material이 할당되어 있을 경우)
        if (RenderSettings.skybox != null)
        {
            Material skyboxMat = RenderSettings.skybox;
            skyboxMat.SetFloat("_AtmosphereThickness", skyboxAtmosphereThickness.Evaluate(time)); // 시간에 따른 노을 적용
        }

        // 경고 텍스트 표시
        if (time > 0.75f && time <= 0.85f)
        {
            warningTxt.SetActive(true);
            if (!isSpawn)
            {
                MapManager.Instance.Resource.resourceSpawn.SpawnResource();
                isSpawn = true;
            }
        }
        else
        {
            warningTxt.SetActive(false);
            isSpawn = false;
        }
    }

    /// <summary>
    /// 주어진 광원의 색상, 강도, 방향 등을 업데이트합니다.
    /// </summary>
    /// <param name="lightSource">업데이트할 광원</param>
    /// <param name="colorGradient">시간에 따른 색상 변화</param>
    /// <param name="intensityCurve">시간에 따른 강도 변화</param>
    /// <param name="timeOffset">광원 위치를 위한 시간 오프셋 (예: 태양 0.25, 달 0.75)</param>
    void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve, float timeOffset)
    {
        float intensity = intensityCurve.Evaluate(time); // 현재 시간에 따른 강도 계산

        // 광원 방향 설정: (time - offset) * noon 벡터에 4.0을 곱해 회전값 계산
        lightSource.transform.eulerAngles = (time - timeOffset) * noon * 4.0f;
        lightSource.color = colorGradient.Evaluate(time); // 현재 시간에 따른 색상 적용
        lightSource.intensity = intensity;

        // 강도가 0이면 해당 오브젝트 비활성화, 그 외엔 활성화
        GameObject go = lightSource.gameObject;
        if (Mathf.Approximately(lightSource.intensity, 0f) && go.activeInHierarchy)
            go.SetActive(false);
        else if (!go.activeInHierarchy && lightSource.intensity > 0f)
            go.SetActive(true);
    }
}
