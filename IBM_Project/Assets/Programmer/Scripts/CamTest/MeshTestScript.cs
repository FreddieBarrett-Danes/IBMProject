using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshTestScript : MonoBehaviour
{
    //public MeshRenderer mesh;
    public GameObject target;
    public float length;
    public int resolution;
    public float rad;
    public float mum;

    public Vector3 dir;
    //public RaycastHit hit;

    public LineRenderer circleRenderer;
    public List<Vector3> vertices = new List<Vector3>();

    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();   

    }

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * length, Color.green);

        vertices.Clear();

        //circleRenderer.transform.position = transform.localPosition;

        DrawCircle(resolution, rad);

        RaycastHit hit;

        for (int i = 0; i < resolution; i++)
        {
            if (Physics.Raycast(transform.position, vertices[i] - this.transform.position, out hit, length))
            {
                //Debug.DrawRay(transform.position, (target.transform.position - transform.position), Color.green);
                Debug.DrawRay(transform.position, (vertices[i] - this.transform.position) * length, Color.green);
                vertices.Add(hit.point);
            }
            else
            {
                Debug.DrawRay(transform.position, (vertices[i] - this.transform.position) * length, Color.red);
            }
        }

        /*
        for (int i = 0; i < resolution; i++)
        {
            float x = Mathf.Sin(mum);
            float y = Mathf.Cos(mum);
            mum += 2 * Mathf.PI / resolution;

            Vector3 dir = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            RaycastHit hit;
            Debug.DrawLine(transform.position, dir, Color.red);
            if (Physics.Raycast(transform.position, dir, out hit))
            {

                //here is how to do your cool stuff ;)
            }
        }*/
    }
    void DrawCircle(int steps, float radius)
    {
        circleRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, 0, y);

            circleRenderer.SetPosition(currentStep, currentPosition + transform.position);

            vertices.Add(circleRenderer.GetPosition(currentStep));
        }
    }
}
