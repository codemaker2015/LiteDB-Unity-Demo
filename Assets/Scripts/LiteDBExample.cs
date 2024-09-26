using UnityEngine;
using LiteDB;
using System.IO;

public class LiteDBExample : MonoBehaviour
{
    private void Start()
    {
        // Create or open a LiteDB database
        using (var db = new LiteDatabase(Application.persistentDataPath + "/MyDatabase.db"))
        {
            // Get a collection (or create, if it doesn't exist)
            var collection = db.GetCollection<MyData>("myCollection");

            // Create a new record
            var newData = new MyData
            {
                Id = ObjectId.NewObjectId(), // Unique identifier
                Name = "TestName",
                Value = 42
            };

            // Insert the record into the collection
            collection.Insert(newData);

            // Query the collection
            var results = collection.FindAll();

            foreach (var data in results)
            {
                Debug.Log($"ID: {data.Id}, Name: {data.Name}, Value: {data.Value}");
            }
        }
    }
}

// Define a class for your data model
public class MyData
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}
