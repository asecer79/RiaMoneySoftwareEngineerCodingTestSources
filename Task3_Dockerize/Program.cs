using System.Text.Json;
using System.Text.Json.Serialization;


/// <summary>
/// Minimal API to manage customer records.
/// Customers are stored in a JSON file ("customers.json").
/// Provides GET and POST endpoints for listing and adding customers.
/// New customers are validated before being added.
/// </summary>
/// 
var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();



const string filePath = "customers.json";

var customers = LoadCustomers();

/// <summary>
/// Retrieves the list of all customers.
/// </summary>
app.MapGet("/customers", () => customers);

/// <summary>
/// Adds new customers to the list with the following validations:
/// - First name, last name, age, and ID must be provided.
/// - Age must be 18 or older.
/// - IDs must be unique.
/// Customers are inserted in sorted order (by last name, then first name).
/// Errors are collected and reported back in the response.
/// </summary>
app.MapPost("/customers", async (List<Customer> newCustomers) =>
{
    List<string> errors = new();
    foreach (var c in newCustomers)
    {
        if (string.IsNullOrWhiteSpace(c.FirstName) ||
            string.IsNullOrWhiteSpace(c.LastName) ||
            c.Age == 0 || c.Id == 0)
        {
            errors.Add($"Invalid data: {JsonSerializer.Serialize(c)}");
            continue;
        }

        if (c.Age < 18)
        {
            errors.Add($"Customer under 18: {c.FirstName} {c.LastName}");
            continue;
        }

        if (customers.Any(x => x.Id == c.Id))
        {
            errors.Add($"Duplicate ID: {c.Id}");
            continue;
        }

        InsertSorted(customers, c);
    }

    SaveCustomers();
    return Results.Ok(new { added = customers.Count, errors });
});

app.Run();

/// <summary>
/// Inserts a customer into the list in sorted order (by last name, then first name).
/// </summary>
/// <param name="list">The current list of customers.</param>
/// <param name="customer">The new customer to insert.</param>
static void InsertSorted(List<Customer> list, Customer customer)
{
    int index = 0;
    while (index < list.Count)
    {
        var cmpLast = string.Compare(customer.LastName, list[index].LastName, StringComparison.Ordinal);
        if (cmpLast < 0) break;
        if (cmpLast == 0 && string.Compare(customer.FirstName, list[index].FirstName, StringComparison.Ordinal) < 0) break;
        index++;
    }
    list.Insert(index, customer);
}

/// <summary>
/// Loads the list of customers from the JSON file.
/// If the file does not exist, returns an empty list.
/// </summary>
/// <returns>List of customers loaded from the file.</returns>
static List<Customer> LoadCustomers()
{
    if (!File.Exists(filePath)) return new List<Customer>();
    var json = File.ReadAllText(filePath);
    return JsonSerializer.Deserialize<List<Customer>>(json) ?? new();
}

/// <summary>
/// Saves the current list of customers to the JSON file, formatted with indentation.
/// </summary>
void SaveCustomers()
{
    var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(filePath, json);
}

/// <summary>
/// Represents a customer entity.
/// </summary>
record Customer
{
    /// <summary>Customer's first name.</summary>
    [JsonPropertyName("firstName")] public string FirstName { get; set; }

    /// <summary>Customer's last name.</summary>
    [JsonPropertyName("lastName")] public string LastName { get; set; }

    /// <summary>Customer's age (must be 18 or older).</summary>
    [JsonPropertyName("age")] public int Age { get; set; }

    /// <summary>Unique customer ID.</summary>
    [JsonPropertyName("id")] public int Id { get; set; }
}
