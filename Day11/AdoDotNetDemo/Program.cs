using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;



// string connectionString = 
// "Data Source=localhost\\SQLEXPRESS;Database=CRMApplicationDB;Integrated Security=True;TrustServerCertificate=True;";

// SqlConnection connection = new SqlConnection(connectionString);

// // create console application builder
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

// Connection
// var connectionString = builder.GetConnectionString("CrmDbConnection");
var connectionString = builder.Build().GetConnectionString("CrmDb");

// for disposing connection object
//using (var connection = new SqlConnection(connectionString))
//{
//}

using var connection = new SqlConnection(connectionString);

try
{
    connection.Open();
    Console.WriteLine("Connection opened successfully");

    // SqlCommand command = new SqlCommand("SELECT * FROM CUSTOMER", connection);

    // SqlDataReader reader = command.ExecuteReader();

    // Execute Reader
    //ExecuteReader(connection);

    //Execute Scalar
    //ExecuteScalar(connection);

     // Execute NonQuery
    //ExecuteNonQuery(connection);

    // Execute DataAdapter
    //SqlDataAdapeterDemo(connection);

    // Insert Customer Demo
    //InsertCollegeDemo(connection);

    // SQL Injection Demo
    //SqlInjectionDemo(connection);

    // Parameterized Query Demo
    ParameterizedQueryDemo(connection);

    // while(reader.Read())
    // {
    //     int id= reader.GetInt32(0);
    //     int age= reader.GetInt32(1);
    //     string name= reader.GetString(2);

    //     Console.WriteLine($"{id}\t{name}\t{age}");
  
    // }
    
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return;
}

finally
{
    connection.Close();
}


void ExecuteScalar (SqlConnection connection)
{
    var query = "SELECT COUNT(*) FROM College ";
    using var command = new SqlCommand(query,connection);
    var count = (int)command.ExecuteScalar();
    Console.WriteLine($" Total College:{count}");
}


void ExecuteNonQuery(SqlConnection connection)
{
    var query = "INSERT INTO College(Name, Age,Department) VALUES ('Sam',40,'DS')";
    using var command = new SqlCommand(query,connection);
    var rowsAffected = command.ExecuteNonQuery();
    Console.WriteLine($"Rows affected :{rowsAffected}");
}

void ExecuteReader(SqlConnection connection)
{
    var query = "SELECT * FROM College WHERE Age > 25";
    using var command = new SqlCommand(query, connection);
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]},Department: {reader["Department"]}");
    }
}

void SqlDataAdapeterDemo(SqlConnection connection)
{
    var query = "SELECT * FROM College";
    SqlCommand sqlCommand = new(query, connection);
    using var selectAllCustomersCommand = sqlCommand;
    using var adapter = new SqlDataAdapter(selectAllCustomersCommand);
    var customerDataTable = new DataTable();

    adapter.Fill(customerDataTable);

    foreach (DataRow row in customerDataTable.Rows)
    {
        Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]},Department: {row["Department"]}");
    }
}

void InsertCollegeDemo(SqlConnection connection)
{
    var dataSet = new DataSet();
    var selectQuery = "SELECT * FROM College";
    using var selectCommand = new SqlCommand(selectQuery, connection);
    using var adapter = new SqlDataAdapter(selectCommand);
    adapter.Fill(dataSet, "College");

    var dataTable = dataSet.Tables["College"];

    var newRow = dataTable.NewRow();
    newRow["Name"] = "New Student";
    newRow["Age"] = 28;
    newRow["Department"] ="ETC";


    dataTable.Rows.Add(newRow);

    adapter.InsertCommand = new SqlCommand("INSERT INTO College (Name, Age,Department) VALUES (@Name, @Age,@Department)", connection)
    {
        CommandType = CommandType.Text
    };

    adapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
    adapter.InsertCommand.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
    adapter.InsertCommand.Parameters.Add("@Department", SqlDbType.NVarChar, 50, "Department");

    adapter.Update(dataSet, "College");

    dataSet.AcceptChanges();

    Console.WriteLine("Inserted Successfully");
}

void SqlInjectionDemo(SqlConnection connection)
{
    // Query: SELECT * FROM Customers WHERE Id = 1 or 1 = 1
    var userInput = "1 or 1 = 1";
    // var userInput = "1; DROP TABLE Customers; ";
    // var userInput = "3";
    var query = $"SELECT * FROM College WHERE Id = {userInput}";

    using var command = new SqlCommand(query, connection);
    try
    {
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]},Department: {reader["Department"]}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error executing query: {ex.Message}");
    }
}

void ParameterizedQueryDemo(SqlConnection connection)
{
    using (SqlCommand command = new SqlCommand(
        "SELECT * FROM College WHERE Name LIKE @Name",
        connection))

    {
        // var id = "3";
        // var id = "3 or 1 = 1";
        // var id = "3 or 1 = 1";
        // Add parameters - database treats them as DATA, never as SQL code
        var name = "John or 1 = 1";
        command.Parameters.AddWithValue("@Name", name);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]},Department: {reader["Department"]}");
        }
        else
        {
            Console.WriteLine("No name found with the specified Id.");
        }
    }
}







