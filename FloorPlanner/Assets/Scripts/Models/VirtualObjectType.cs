using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpRequestSingleton;

public interface IVirtualObjectType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public IVirtualObjectType VirtualObjectTypeFromJson(string json);
    public string VirtualObjectTypeToJson();

}
[System.Serializable]
public class VirtualObjectType : IVirtualObjectType
{
    public int id;
    public string name;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public IVirtualObjectType VirtualObjectTypeFromJson(string json)
    {
        return JsonUtility.FromJson<VirtualObjectType>(json);
    }

    public string VirtualObjectTypeToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


public interface IVirtualObjectTypeList
{
    public delegate void VirtualObjectTypesResult(IVirtualObjectTypeList virtualObjectTypeModelList);

    event VirtualObjectTypesResult onGetAllVirtualObjectTypesResult;

    public List<VirtualObjectType> VirtualObjectTypes { get; set; }

    public IVirtualObjectTypeList VirtualObjectTypeListFromJson(string json);

    public void GetAllVirtualObjectTypes();

}
public class VirtualObjectTypeList : IVirtualObjectTypeList
{
    private event Result onGetAllVirtualObjectTypes;

    public List<VirtualObjectType> virtualObjectTypes;

    public List<VirtualObjectType> VirtualObjectTypes
    {
        get { return virtualObjectTypes; }
        set { virtualObjectTypes = value; }
    }

    public event IVirtualObjectTypeList.VirtualObjectTypesResult onGetAllVirtualObjectTypesResult;


    public void GetAllVirtualObjectTypes()
    {
        onGetAllVirtualObjectTypes += onResultGetAllVirtualObjectTypes;
        HttpRequestSingleton.Instance.CallGet("/virtualobjecttypes", onGetAllVirtualObjectTypes);
    }
    private void onResultGetAllVirtualObjectTypes(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        virtualObjectTypes = VirtualObjectTypeListFromJson(targetsJson).VirtualObjectTypes;
        onGetAllVirtualObjectTypesResult(this);
    }

    public IVirtualObjectTypeList VirtualObjectTypeListFromJson(string json)
    {
        return JsonUtility.FromJson<VirtualObjectTypeList>(json);
    }
}
