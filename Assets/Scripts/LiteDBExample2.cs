using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteDB;

public class LiteDBExample2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Open database (or create if doesn't exist)
        using(var db = new LiteDatabase(@"MyDatabase.db"))
        {
            // Get a collection (or create, if doesn't exist)
            var col = db.GetCollection<Customer>("customers");

            // Create your new customer instance
            var customer = new Customer
            { 
                Name = "Vishu", 
                Phones = new string[] { "8000-0000", "9000-0000" }, 
                IsActive = true
            };
            
            // Insert new customer document (Id will be auto-incremented)
            col.Insert(customer);
            
            // Update a document inside a collection
            customer.Name = "Vishu Sivan";
            
            col.Update(customer);
            
            // Index document using document Name property
            col.EnsureIndex(x => x.Name);
            
            // Use LINQ to query documents
            var results = col.Find(x => x.Name.StartsWith("Vi"));

            // Let's create an index in phone numbers (using expression). It's a multikey index
            // col.EnsureIndex(x => x.Phones, "$.Phones[*]"); 

            // and now we can query phones
            var r = col.FindOne(x => x.Phones.Any(p => p == "9000-0000"));

            if (r != null)
            {
                Debug.Log("Customer found: " + r.Name);
            }
            else
            {
                Debug.Log("No customer found with the specified phone number.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// Create your POCO class entity
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string[] Phones { get; set; }
    public bool IsActive { get; set; }
}