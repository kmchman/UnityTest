using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoleObject : MonoBehaviour
{
    [SerializeField] private Sprite moleSpr;

    private Coroutine appearCoroutine;
    public void AppearMole()
    {
        gameObject.SetActive(true);
        if (appearCoroutine != null)
            StopCoroutine(appearCoroutine);
        appearCoroutine = StartCoroutine(AppearCoroutine());
    }

    IEnumerator AppearCoroutine()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }

}
