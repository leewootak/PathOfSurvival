using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curinteractable;
    private ResourcePlayerCanGet resourcePlayerCanGet;
    private PostItHave postItHave;

    public TextMeshProUGUI promptText;
    private Camera camera;


    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if(Time.time - lastCheckTime > checkRate) // 현재시간-마지막체크시간 이 0.05보다 크다면
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    if (hit.collider.TryGetComponent<IInteractable>(out curinteractable))
                    {
                        SetPromptText();
                    }
                    hit.collider.TryGetComponent<ResourcePlayerCanGet>(out resourcePlayerCanGet);
                    hit.collider.TryGetComponent<PostItHave>(out postItHave);
                }
            }
            else
            {
                curInteractGameObject = null;
                curinteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curinteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curinteractable != null)
        {
            curinteractable.OnInteract();
            curInteractGameObject = null;
            curinteractable = null;
            promptText.gameObject.SetActive(false);
        }
        else if (context.phase == InputActionPhase.Started && resourcePlayerCanGet != null)
        {
            resourcePlayerCanGet.GetResource();
            curInteractGameObject = null;
            curinteractable = null;
        }
        else if (context.phase == InputActionPhase.Started && postItHave != null)
        {
            postItHave.OnInteraction();
            curInteractGameObject = null;
            curinteractable = null;
        }
    }
}
