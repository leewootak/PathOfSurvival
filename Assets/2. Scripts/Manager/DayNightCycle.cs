using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // 0 ~ 1 사이의 값으로 하루 시간 표현
    [Range(0.0f, 1.0f)]
    public float time;
    public float dayLength; // 하루의 길이
    public float startTime = 0.4f; // 시작 시간
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

    private void Start()
    {
        timeRate = 1.0f / dayLength; // 하루 길이에 따라 시간 증가 속도 설정 (1초에 1/dayLength 만큼 증가)
        time = startTime; // 시작 시간 초기화
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f; // 매 프레임마다 시간 증가, 1 초과 시 0부터 다시 시작

        // 햇빛, 달빛을 각각 업데이트
        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        // 환경 조명과 반사 강도를 현재 시간에 따라 설정
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

    }

    /// <summary>
    /// 광원의 색상, 강도, 방향 등을 업데이트
    /// </summary>
    /// <param name="lightSource">업데이트 할 광원</param>
    /// <param name="colorGradiant">변화 색상</param>
    /// <param name="intensityCurve">시간에 따른 변화 강도</param>
    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time); // 현재 시간에 따른 강도 계산

        // 광원 방향 설정 (해는 0.25, 달은 0.75의 오프셋을 주어 반대편에 위치)
        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time); // 현재 시간에 따른 색상 설정
        lightSource.intensity = intensity; // 계산한 강도 적용

        GameObject go = lightSource.gameObject; // lightSource가 속한 게임 오브젝트 참조

        if (lightSource.intensity == 0 && go.activeInHierarchy) // 강도가 0이면 비활성화
            go.SetActive(false);
        else if (lightSource.intensity > 0 && !go.activeInHierarchy) // 강도가 0보다 크면 활성화
            go.SetActive(true);
    }
}
