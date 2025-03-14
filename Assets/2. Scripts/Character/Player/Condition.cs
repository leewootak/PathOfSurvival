using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; // 게임진행중 현재 ui 값
    public float startValue; // 게임 시작시 기본 값
    public float maxValue; // 최댓값 오버하지 않도록 지정
    public float passiveValue; // 시간에 따라 줄어드는 값 배고픔 수분 감염도 
    public Image uiBar;

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;

    }

    // Update is called once per frame
    void Update()
    {
        uiBar.fillAmount = GetPercentage();// .fillAmount 이미지가 부분적으로 차오르거나 비워지는 효과
    }

    float GetPercentage()
    {
        return curValue / maxValue; // 현재값을 맥스값으로 나누기
    }

    public void Add(float value) // curValue 증가시키고 싶을때
    {
        curValue = Mathf.Min(curValue + value, maxValue); // curValue 값이 최대치를 넘어가지 않게
        // 더해진 값과 최댓값을 비교후 작은값을 가져온다
    }

    public void Subtract(float value) // curValue 감소 시키고 싶을때
    {
        curValue = Mathf.Max(curValue - value, 0); // 위와 반대
    }
}
