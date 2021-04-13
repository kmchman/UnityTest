using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoleObject : MonoBehaviour
{
    public enum STATE
    { 
        IDLE,
        APPEAR,
        CATCH,
    }

    [SerializeField] private Sprite disappearSpr;
    [SerializeField] private Sprite appearSpr;
    [SerializeField] private Sprite catchSpr;
    [SerializeField] private Sprite moleSpr;
    [SerializeField] private SpriteRenderer sprRenderer;

    private Coroutine appearCoroutine;
    private STATE state;

    private void Awake()
    {
        SetState(STATE.IDLE);
    }

    public void SetState(STATE _state)
    {
        state = _state;
        switch (state)
        {
            case STATE.IDLE:
                sprRenderer.sprite = disappearSpr;
                break;
            case STATE.APPEAR:
                sprRenderer.sprite = appearSpr;
                break;
            case STATE.CATCH:
                sprRenderer.sprite = catchSpr;
                break;
        }
    }
    public void AppearMole()
    {
        SetState(STATE.APPEAR);
        if (appearCoroutine != null)
            StopCoroutine(appearCoroutine);
        appearCoroutine = StartCoroutine(AppearCoroutine());
    }

    IEnumerator AppearCoroutine()
    {
        yield return new WaitForSeconds(3);
        SetState(STATE.IDLE);
    }

    private void OnMouseDown()
    {
        if (state == STATE.APPEAR)
        {
            SetState(STATE.CATCH);
            MoleGameManager.instance.CatchMole();
        }
        
    }

}
