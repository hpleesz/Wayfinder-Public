using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VirtualObjectType
{
    public int id;
    public string name;

    public static VirtualObjectType VirtualObjectTypeFromJson(string json)
    {
        return JsonUtility.FromJson<VirtualObjectType>(json);
    }

    public string VirtualObjectTypeToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


public class VirtualObjectTypeList
{

    public List<VirtualObjectType> virtualObjectTypes;
    public static VirtualObjectTypeList VirtualObjectTypeListFromJson(string json)
    {
        return JsonUtility.FromJson<VirtualObjectTypeList>(json);
    }
}
