using UnityEngine;

public class MenuCheck : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("MenuController") == null)
        {
            Instantiate(menu);
        }
    }
}
