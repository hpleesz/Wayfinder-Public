using Dummiesman;

using System.IO;
using System.Text;
using UnityEngine;

public class ObjFromStream : MonoBehaviour {

    //MongoClient client = new MongoClient("mongodb+srv://mbUser:mbPassword@modelbuilder.boglt.mongodb.net/<dbname>?retryWrites=true&w=majority");
    //IMongoDatabase database;
    //IMongoCollection<BsonDocument> collection;

    void Start() {

        //database = client.GetDatabase("ModelBuilderDB");
        //collection = database.GetCollection<BsonDocument>("ModelBuilderCollection");


        //make www
        var www = new WWW("https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj");
        while (!www.isDone)
            System.Threading.Thread.Sleep(1);

        //create stream and load
        string text = System.IO.File.ReadAllText(@"C:\Users\plees\Documents\Unity\3D Model Builder\Assets\Resources\HOUSE\3070b.obj");

        //var textStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        //var loadedObj = new OBJLoader().Load(textStream);

        //SaveObjToDatabase(text);
        GetObjFromDatabase("");
    }

    public void GetObjFromDatabase(string objectId)
    {
        //var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId("5fd5e73e30ac9b63b0c99c7e"));
        //var brick = collection.Find(filter).FirstOrDefault();
        //Debug.Log(brick.ToString());


        //ObjModel objModel = ObjModel.CreateFromJSON(brick.ToString());
        //Debug.Log(objModel._id);
        //Debug.Log(objModel.objname);
        //Debug.Log(brick.GetValue("obj"));


        /*
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(brick.GetValue("obj").ToString()));
        var loadedObj = new OBJLoader().Load(textStream);
        */
    }



    [System.Serializable]
    public class ObjModel
    {
        public string _id;
        public string objname;
        public string obj;

        public static ObjModel CreateFromJSON(string jsonString)
        {
            return UnityEngine.JsonUtility.FromJson<ObjModel>(jsonString);
        }

        // Given JSON input:
        // {"name":"Dr Charles","lives":3,"health":0.8}
        // this example will return a PlayerInfo object with
        // name == "Dr Charles", lives == 3, and health == 0.8f.
    }

    public class ObjModels
    {
        public System.Collections.Generic.List<ObjModel> bricks;

        public static ObjModels CreateFromJSON(string jsonString)
        {
            return UnityEngine.JsonUtility.FromJson<ObjModels>(jsonString);
        }

        public static string CreateToJson(ObjModels bricks)
        {
            return UnityEngine.JsonUtility.ToJson(bricks, true);
        }
    }

}
