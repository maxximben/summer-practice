using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13;

public class Subject
{
    public string? Name {get; set; }
    public int Grade {get; set; }
}

public class Student
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public List<Subject>? Grades { get; set; }

    public bool Validate()
    {
        if (FirstName == null || LastName == null)
        {
            return false;
        }

        if (Grades == null)
        {
            return false;
        }
        
        if (BirthDate == null) 
        {
            return false;
        }
        
        return true;
    }
}

public class DataTimeConverter : JsonConverter<DateTime>
{
    private const string DATETIME_FORMAT = "dd-MM-yyyy";
    
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString()!, DATETIME_FORMAT, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DATETIME_FORMAT));
    }
}
