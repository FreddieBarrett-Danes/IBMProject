using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab, new Vector3(prefab.transform.position.x + 2.0f, prefab.transform.position.y, prefab.transform.position.z), Quaternion.identity);
        //int j = 2;
        //Instantiate(prefab, new Vector3(j * 2.0f, 0, 0), Quaternion.identity);
        //for (var i = 0; i < 3; i++)
        //{
        //    Debug.Log("Test " + i);
        //    Instantiate(prefab, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        //}
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}