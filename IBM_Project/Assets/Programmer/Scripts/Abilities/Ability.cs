using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability :MonoBehaviour
{
    public GameObject host;
    public GameObject SetHost(GameObject parent)
    {
        return host = parent;
    }
    public virtual void Execute()
    {

    }

}
