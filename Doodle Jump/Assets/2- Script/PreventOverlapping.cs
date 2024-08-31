using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventOverlapping : MonoBehaviour
{
    public float scanRadius = 3f;
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    } 
}
