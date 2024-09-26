using UnityEngine;
using LiteDB;
using System.IO;

public class LiteDBExample3 : MonoBehaviour
{
    private void Start()
    {
        // Create or open a LiteDB database
        using (var db = new LiteDatabase(Application.persistentDataPath + "/MyDatabase.db"))
        {
            string fileId = "my-photo-id";
            // Upload a file from file system to database
            db.FileStorage.Upload(fileId, @"test.jpg");

            // And download later
            using (var stream = new FileStream(@"test22.jpg", FileMode.Create, FileAccess.Write))
            {
                // Download the file from LiteDB's FileStorage
                db.FileStorage.Download(fileId, stream);
            }

            if (db.FileStorage.Delete(fileId))
            {
                Debug.Log("File deleted successfully.");
            }
            else
            {
                Debug.Log("File not found.");
            }
        }
    }
}
