using UnityEngine;

public class LevelTimeBank : MonoBehaviour
{
    private LevelTimer levelTimer;
    public bool completedLevel = false;
    // Start is called before the first frame update
    void Start()
    {
        levelTimer = GameObject.FindGameObjectWithTag("Level Timer").GetComponent<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!completedLevel)
        {
            //playing level
        }
        else
        {

        }

    }
}
