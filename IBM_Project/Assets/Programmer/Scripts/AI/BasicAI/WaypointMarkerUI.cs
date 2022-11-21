using UnityEngine;
public class WaypointMarkerUI : MonoBehaviour
{
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}