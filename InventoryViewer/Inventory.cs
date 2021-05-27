using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace InventoryViewer
{
    class Inventory
    {
        /// <summary>
        /// Represents an inventory item
        /// </summary>
        public struct InventoryItem
        {
            /// <summary>
            /// Tigerpaw item id
            /// </summary>
            public string itemID { get; set; }
            /// <summary>
            /// Item description
            /// </summary>
            public string itemDescription { get; set; }
            /// <summary>
            /// Quantity on hand
            /// </summary>
            public int onHand { get; set; }
            /// Quantity on order; exists in a PO
            /// </summary>
            public int onOrder { get; set; }
            /// <summary>
            /// Quantity reserved; on an SO
            /// </summary>
            public int numReserved { get; set; }
        }

        // Contains the connection string we'll use to connect to the database
        // Built at class creation
        private string dbConnectionString;
        // Database connection
        private SqlConnection dbConnection;

        // List of itemIDs we're looking up in database
        public List<string> itemIDs { get; set; }
        // List of inventory items output from database
        public ObservableCollection<InventoryItem> itemsList;

        /// <summary>
        /// Creates a new inventory object.
        /// </summary>
        /// <param name="dataServer">IP address or hostname of database server</param>
        /// <param name="database">Name of Tigerpaw database</param>
        /// <param name="username">Credentials username</param>
        /// <param name="password">Credentials password</param>
        public Inventory(string dataServer, string database, string username, string password)
        {
            // Construct the database connection string
            dbConnectionString = $"Data Source={dataServer};Initial Catalog={database};User ID={username};Password={password}";
            // Initialize the database connection
            dbConnection = new SqlConnection(dbConnectionString);

            //itemsList = new List<InventoryItem>(); // Initialize items list
            itemsList = new ObservableCollection<InventoryItem>();
            itemIDs = new List<string>(); // Initialize the itemIDs list
        }

        /// <summary>
        /// Update the inventory list from DB
        /// </summary>
        public void UpdateInventory()
        {
            itemsList.Clear(); // Clear the current list; we'll be getting a new set from db

            // Open the database connection and run our query
            dbConnection.Open();

            // Build the query string and command
            string sql = $"SELECT ItemID, ItemDescription, QuantityOnHand, QuantityOnOrder, QuantityReserved FROM dbo.tblPriceBook WHERE {BuildLookupQuery()};";
            SqlCommand cmd = new SqlCommand(sql, dbConnection);
            // Initialize a new data reader and execute the command
            SqlDataReader reader = cmd.ExecuteReader();
            // Parse query data
            while (reader.Read()) // Read the row
            {
                string id = reader.GetString("ItemID");
                string desc = reader.GetString("ItemDescription");
                int onhand = reader.GetInt32("QuantityOnHand");
                int onorder = reader.GetInt32("QuantityOnOrder");
                int reserved = reader.GetInt32("QuantityReserved");
                // Add a new inventory item to the list
                itemsList.Add(new InventoryItem { itemID = id, itemDescription = desc, onHand = onhand, onOrder = onorder, numReserved = reserved });
            }
            // Clean up our reader and command and close the DB connection
            reader.Close();
            cmd.Dispose();
            dbConnection.Close();
        }

        private string BuildLookupQuery()
        {
            List<string> queries = new List<string>(); // List of where queries to be joined by 'or'
            foreach(string id in itemIDs) // Loop through the IDs and build a where query for each. Add it to the list
            {
                queries.Add($"ItemID = '{id}'");
            }
            // Join the list together with 'or's and return the new string
            return string.Join(" OR ", queries);
        }
    }
}
