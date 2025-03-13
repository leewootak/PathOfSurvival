using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        // 데미지 받을 때 효과를 PlayerCondition의 데미지 Action에 추가
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    public void Flash() // Flash 호출 될때마다
    {
        // 실행중인 코루틴 있다면 정지. 피해받는 빨간 화면이 사라져야함
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }
    // 이해 하기론 인스펙터 창에선 일단꺼두고 위의 코드에선 트루로 설정하되 코루틴으로 대기를 시키는듯한?
    private IEnumerator FadeAway() // 데미지 받는 빨간화면이 서서히 사라지는 함수
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a-= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f/ 255f,100f/255f, a);
            yield return null;
        }

        image.enabled = false;
    }

}
