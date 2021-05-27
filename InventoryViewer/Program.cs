using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Threading;

namespace InventoryViewer
{
    static class Program
    {
        // Struct to hold database login information
        private struct DatabaseInfo
        {
            public string host { get; set; }
            public string database { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int countLowThreshold { get; set; }
        }

        /// <summary>
        /// Database inventory information
        /// </summary>
        public static Inventory inventory;
        // Timer to update the database every minute
        private static DispatcherTimer timer;
        // Returns the low threshold
        public static int CountLowThreshold { get; private set; }
        private static void UpdateItemIDs()
        {
            // Clear existing IDs since we're about to read them all again
            inventory.itemIDs.Clear();
            // Read all lines from the ItemIDs text file
            string[] lines = System.IO.File.ReadAllLines("ItemIDs.txt");
            foreach(string line in lines) // For each line
            {
                inventory.itemIDs.Add(line); // Add it to the itemIDs that we're looking up in the database
            }
        }

        // Read the hostname, database name and credentials from config file
        // TODO: Find a more secure way to handle credentials - log in dialog at program start?
        private static DatabaseInfo ReadDBInfo()
        {
            string json = System.IO.File.ReadAllText("database.cfg"); // Read config settings from json file
            return JsonSerializer.Deserialize<DatabaseInfo>(json); // Deserialize into config settings struct
        }

        // Update the ItemIDs and poll the database.
        // In a function because we do this in several places.
        private static void UpdateData()
        {
            UpdateItemIDs(); // Update the list of items we're pulling data about
            inventory.UpdateInventory(); // Pull data from the database
        }

        /// <summary>
        ///  Initializes system and starts polling data.
        ///  Must be called before any other calls!
        /// </summary>
        public static void Start()
        {
            DatabaseInfo dbi = ReadDBInfo(); // Read the database config from file
            inventory = new Inventory(dbi.host, dbi.database, dbi.username, dbi.password); // Create a new database data connection
            CountLowThreshold = dbi.countLowThreshold;
            UpdateData(); // Update the data

            // Update the data again every ten seconds
            timer = new DispatcherTimer();
            timer.Tick += (object sender, EventArgs e) => UpdateData();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
        }
    }
}
