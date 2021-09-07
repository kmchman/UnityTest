using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cubeObject;

    public void OnClickBtnMoveCube()
    {
        foreach (var item in cubeObject)
        {
            item.transform.SetPositionAndRotation( item.transform.position + new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)), transform.rotation);
        }
    }
}
