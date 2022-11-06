using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animation")]
    public float startDelay = 1f;
    public float duration = .5f;
    public float delay = .1f;
    public Ease ease = Ease.OutBack;

    private void Awake()
    {
        HideButtons();
        Invoke(nameof(ShowButtons), startDelay);
    }

    private void Start()
    {
        PauseManager.Instance.Unpause();
    }

    private void HideButtons()
    {
        foreach(var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }

    private void ShowButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i * delay);
        }
    }
}
