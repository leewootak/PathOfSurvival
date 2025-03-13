using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // 0 ~ 1 사이의 값으로 하루 시간 표현
    [Range(0.0f, 1.0f)]
    public float time;
    public float dayLength = 120f; // 하루 길이 (예: 120초)
    public float startTime = 0.4f; // 시작 시간
    private float timeRate; // 시간 증가 속도
    public Vector3 noon = new Vector3(0, 90, 0); // 정오(낮) 방향 벡터 설정

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

    [Header("---")]
    public GameObject warningTxt;

    private void Start()
    {
        // 하루 길이에 따라 시간 증가 속도 설정 (1초에 1/dayLength 만큼 증가)
        timeRate = 1.0f / dayLength;
        time = startTime; // 시작 시간 초기화

        // AnimationCurve 기본값 세팅 (Inspector에서 수정 가능)
        // [Sun Intensity]
        // 낮: 0.15~0.35 구간에서 최고치를 찍고, 0.75부터 0으로 자연스럽게 감쇠
        if (sunIntensity == null || sunIntensity.length == 0)
        {
            sunIntensity = new AnimationCurve(
                new Keyframe(0f, 0f),
                new Keyframe(0.15f, 0.5f),
                new Keyframe(0.25f, 1.3f),
                new Keyframe(0.35f, 1.0f),
                new Keyframe(0.5f, 0.6f),
                new Keyframe(0.65f, 0.2f),
                new Keyframe(0.75f, 0f),
                new Keyframe(1f, 0f)
            );
        }

        // [Moon Intensity]
        // 달은 낮에 보이지 않다가 0.75부터 등장하여 밤에는 일정한 강도를 유지
        if (moonIntensity == null || moonIntensity.length == 0)
        {
            moonIntensity = new AnimationCurve(
                new Keyframe(0f, 0f),
                new Keyframe(0.5f, 0f),
                new Keyframe(0.75f, 1f),
                new Keyframe(1f, 1f)
            );
        }

        // [Ambient Lighting Intensity]
        // 낮에는 밝게, 밤에는 어둡게
        if (lightingIntensityMultiplier == null || lightingIntensityMultiplier.length == 0)
        {
            lightingIntensityMultiplier = new AnimationCurve(
                new Keyframe(0f, 0.5f),
                new Keyframe(0.25f, 1.0f),
                new Keyframe(0.75f, 0.5f),
                new Keyframe(1f, 0.3f)
            );
        }

        // [Reflection Intensity]
        // Ambient와 비슷하게 조절
        if (reflectionIntensityMultiplier == null || reflectionIntensityMultiplier.length == 0)
        {
            reflectionIntensityMultiplier = new AnimationCurve(
                new Keyframe(0f, 0.5f),
                new Keyframe(0.25f, 1.0f),
                new Keyframe(0.75f, 0.5f),
                new Keyframe(1f, 0.3f)
            );
        }
    }

    private void Update()
    {
        // 매 프레임마다 시간 증가, 1 초과 시 0부터 다시 시작
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        // 햇빛, 달빛을 각각 업데이트
        UpdateLighting(sun, sunColor, sunIntensity, 0.25f); // 해는 0.25 오프셋
        UpdateLighting(moon, moonColor, moonIntensity, 0.75f); // 달은 0.75 오프셋

        // 환경 조명과 반사 강도를 현재 시간에 따라 설정
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

        // 경고 텍스트 활성화 (예시: 특정 시간대에만)
        if (time > 0.75f && time <= 0.85f)
        {
            warningTxt.SetActive(true);
        }
        else
        {
            warningTxt.SetActive(false);
        }
    }

    /// <summary>
    /// 광원의 색상, 강도, 방향 등을 업데이트
    /// </summary>
    /// <param name="lightSource">업데이트 할 광원</param>
    /// <param name="colorGradiant">시간에 따른 변화 색상</param>
    /// <param name="intensityCurve">시간에 따른 변화 강도</param>
    /// <param name="offset">광원의 위치 오프셋 (해:0.25, 달:0.75)</param>
    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve, float offset)
    {
        float intensity = intensityCurve.Evaluate(time); // 현재 시간에 따른 강도 계산

        // 광원 방향 설정: time에 offset을 빼고 noon 벡터(예: (0, 90, 0))에 4를 곱해 회전 각도 산출
        lightSource.transform.eulerAngles = (time - offset) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time); // 현재 시간에 따른 색상 설정
        lightSource.intensity = intensity; // 계산한 강도 적용

        // 강도가 0이면 해당 광원 GameObject 비활성화하여 불필요한 연산 줄이기
        GameObject go = lightSource.gameObject;
        if (Mathf.Approximately(lightSource.intensity, 0f) && go.activeInHierarchy)
            go.SetActive(false);
        else if (!Mathf.Approximately(lightSource.intensity, 0f) && !go.activeInHierarchy)
            go.SetActive(true);
    }
}
