using System.Collections.Generic;
using UnityEngine;
using System;

public class Perception : MonoBehaviour
{
    private readonly Dictionary<GameObject, MemoryRecord> memoryMap = new();
    
    public GameObject[] sensedObjects;
    public MemoryRecord[] sensedRecord;

    public void ClearFoV()
    {
        foreach(KeyValuePair<GameObject, MemoryRecord> memory in memoryMap)
        {
            memory.Value.withinFoV = false;
        }
    }

    public void AddMemory(GameObject target)
    {
        MemoryRecord record = new(DateTime.Now, target.transform.position, true);
        if(memoryMap.ContainsKey(target))
            memoryMap[target] = record;
        else
            memoryMap.Add(target, record);
    }
    
    private void Update()
    {
        sensedObjects = new GameObject[memoryMap.Keys.Count];
        sensedRecord = new MemoryRecord[memoryMap.Values.Count];
        memoryMap.Keys.CopyTo(sensedObjects, 0);
        memoryMap.Values.CopyTo(sensedRecord, 0);
    }
}
[Serializable]
public class MemoryRecord
{
    public DateTime timeLastSensed;
    [SerializeField]
    public Vector3 lastSensedPosition;
    [SerializeField]
    public bool withinFoV;
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