using System.Collections.Generic;
using UnityEngine;
using System;

public class Perception : MonoBehaviour
{
    private readonly Dictionary<GameObject, MemoryRecord> memoryMap = new();

    //For debugging
    public GameObject[] sensedObjects;
    public MemoryRecord[] sensedRecord;

    /// <summary>
    /// Clears all the current FoVs
    /// </summary>
    public void ClearFoV()
    {
        foreach(KeyValuePair<GameObject, MemoryRecord> memory in memoryMap)
        {
            memory.Value.withinFoV = false;
        }
    }

    public void AddMemory(GameObject target)
    {
        //Create a new memory record
        MemoryRecord record = new MemoryRecord(DateTime.Now, target.transform.position, true);

        //Check if we already have a previous memory record for this target
        if(memoryMap.ContainsKey(target))
        {
            //Overwrite the previous record instead of adding a new one
            memoryMap[target] = record;
        }
        else
        {
            //Otherwise add the new record
            memoryMap.Add(target, record);
        }
    }

    /// <summary>
    /// Can remove this whole update. It's just here for debugging
    /// </summary>
    private void Update()
    {
        //Just expose the values to inspector here so we can see if it's working
        sensedObjects = new GameObject[memoryMap.Keys.Count];
        sensedRecord = new MemoryRecord[memoryMap.Values.Count];
        memoryMap.Keys.CopyTo(sensedObjects, 0);
        memoryMap.Values.CopyTo(sensedRecord, 0);
    }
}

[Serializable]
public class MemoryRecord
{
    /// <summary>
    /// The time the target was last sensed
    /// </summary>
    [SerializeField]
    public DateTime timeLastSensed;

    /// <summary>
    /// The position the target was last sensed
    /// </summary>
    [SerializeField]
    public Vector3 lastSensedPosition;

    /// <summary>
    /// Whether the target is currently within the FoV
    /// </summary>
    [SerializeField]
    public bool withinFoV;

    /// <summary>
    /// To help with debugging, we convert DateTime to string from so can serialize in inspector
    /// </summary>
    [SerializeField]
    public string timeLastSenseStr;

    public MemoryRecord()
    {
        timeLastSensed = DateTime.MinValue;
        lastSensedPosition = Vector3.zero;
        withinFoV = false;
    }

    public MemoryRecord(DateTime TTime, Vector3 TPos, bool TFoV)
    {
        timeLastSensed = TTime;
        lastSensedPosition = TPos;
        withinFoV = TFoV;
        timeLastSenseStr = TTime.ToLongTimeString();
    }

}
