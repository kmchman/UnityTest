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
        var randPos = new Vector3(pos.x, Random.Range(-3, 3), pos.y) + transform.position;

        //float posY = ;
        RaycastHit hit;
        Physics.Raycast(randPos, Vector3.down, out hit, 100f, 1 << LayerMask.NameToLayer("Ground"));
        if (hit.collider == null)
        {
            randPos.y = 5f;
        }
        return randPos;
    }
}
