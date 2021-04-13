using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGameManager : MonoBehaviour
{
    [SerializeField] private MoleObject[] moleObjects;

    private Coroutine gameCoroutine;
    private void Start()
    {
        foreach (var item in moleObjects)
            item.gameObject.SetActive(false);

        if (gameCoroutine != null)
            StopCoroutine(gameCoroutine);
        StartCoroutine(GameCoroutine());
    }

    IEnumerator GameCoroutine()
    {
        while (true)
        {
            int rand = Random.Range(0, moleObjects.Length);
            moleObjects[rand].AppearMole();
            yield return new WaitForSeconds(2);
        }
    }
}
