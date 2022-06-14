using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, radius);
    }

    public Vector3 GetRandomPos()
    {
        var pos = Random.insideUnitCircle * radius;
        return new Vector3(pos.x, 0, pos.y) + transform.position;
    }
}
