using System.Text.Json;
using System.Text.Json.Serialization;
using task13;

namespace task13tests;

public class task13tests
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DataTimeConverter() }
    };

    private readonly Student studentAllData = new Student
    {
        FirstName = "Ivan",
        LastName = "Ivanov",
        BirthDate = new DateTime(2015, 01, 01),
        Grades = new List<Subject>
        {
            new Subject { Name = "Mathematics", Grade = 5 },
            new Subject { Name = "Russian", Grade = 3 },
            new Subject { Name = "Literature", Grade = 4 },
        }
    };

    private readonly Student studentNull = new Student
    {
        FirstName = null,
        LastName = null,
        BirthDate = null,
        Grades = null
    };

    [Fact]
    public void Serialize_StudentWithAllData_SerializesCorrectly()
    {
        var expectedJson = """
                           {
                             "FirstName": "Ivan",
                             "LastName": "Ivanov",
                             "BirthDate": "01-01-2015",
                             "Grades": [
                               {
                                 "Name": "Mathematics",
                                 "Grade": 5
                               },
                               {
                                 "Name": "Russian",
                                 "Grade": 3
                               },
                               {
                                 "Name": "Literature",
                                 "Grade": 4
                               }
                             ]
                           }
                           """;

        var json = JsonSerializer.Serialize(studentAllData, options);
        Assert.Equal(expectedJson.Replace("\r\n", "\n"), json.Replace("\r\n", "\n"));
    }

    [Fact]
    public void Serialize_StudentWithNullValues_SerializesCorrectly()
    {
        var expectedJson = "{}";
        var json = JsonSerializer.Serialize(studentNull, options);
        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public void Deserialize_StudentWithAllData_DeserializesCorrectly()
    {
        var json = """
                   {
                     "FirstName": "Ivan",
                     "LastName": "Ivanov",
                     "BirthDate": "01-01-2015",
                     "Grades": [
                       {
                         "Name": "Mathematics",
                         "Grade": 5
                       },
                       {
                         "Name": "Russian",
                         "Grade": 3
                       },
                       {
                         "Name": "Literature",
                         "Grade": 4
                       }
                     ]
                   }
                   """;

        var deserializedStudent = JsonSerializer.Deserialize<Student>(json, options);
        Assert.NotNull(deserializedStudent);
        Assert.Equal(studentAllData.FirstName, deserializedStudent.FirstName);
        Assert.Equal(studentAllData.LastName, deserializedStudent.LastName);
        Assert.Equal(studentAllData.BirthDate, deserializedStudent.BirthDate);
        Assert.NotNull(deserializedStudent.Grades);
        Assert.Equal(studentAllData.Grades.Count, deserializedStudent.Grades.Count);
        for (int i = 0; i < studentAllData.Grades.Count; i++)
        {
            Assert.Equal(studentAllData.Grades[i].Name, deserializedStudent.Grades[i].Name);
            Assert.Equal(studentAllData.Grades[i].Grade, deserializedStudent.Grades[i].Grade);
        }
    }

    [Fact]
    public void Deserialize_StudentWithNullValues_DeserializesCorrectly()
    {
        var json = "{}";
        var deserializedStudent = JsonSerializer.Deserialize<Student>(json, options);
        Assert.NotNull(deserializedStudent);
        Assert.Null(deserializedStudent.FirstName);
        Assert.Null(deserializedStudent.LastName);
        Assert.Null(deserializedStudent.BirthDate);
        Assert.Null(deserializedStudent.Grades);
    }

    [Fact]
    public void DataTimeConverter_SerializesDateTime_CorrectFormat()
    {
        var date = new DateTime(2015, 01, 01);
        var expected = "\"01-01-2015\"";
        var json = JsonSerializer.Serialize(date, options);
        Assert.Equal(expected, json);
    }

    [Fact]
    public void DataTimeConverter_DeserializesDateTime_CorrectFormat()
    {
        var json = "\"01-01-2015\"";
        var expected = new DateTime(2015, 01, 01);
        var date = JsonSerializer.Deserialize<DateTime>(json, options);
        Assert.Equal(expected, date);
    }

    [Fact]
    public void Validate_ReturnsTrue()
    {
        Assert.True(studentAllData.Validate());
    }
    
    [Fact]
    public void Validate_ReturnsFalse()
    {
        Assert.False(studentNull.Validate());
    }
}    
