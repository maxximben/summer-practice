using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using task13;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        Student student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = new DateTime(2015,01,01),
            Grades = new List<Subject>
            {
                new Subject {Name = "Mathematics", Grade = 5}, 
                new Subject {Name = "Russian", Grade = 3}, 
                new Subject {Name = "Literature", Grade = 4}, 
            }
        };
        
        JsonSerializerOptions? options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = {new DataTimeConverter()}
        };

        string json = JsonSerializer.Serialize(student, options);
        Console.WriteLine(json);
        File.WriteAllText("student.json", json);
        
        Student? deserialized = JsonSerializer.Deserialize<Student>(File.ReadAllText("student.json"), options);
        
        var firstName = deserialized.FirstName;
        var lastName = deserialized.LastName;
        var birthDate = deserialized.BirthDate;
        var grades = deserialized.Grades;

        Console.WriteLine($"Имя: {firstName} {lastName}\nДата рождения: {birthDate}");
        
        Console.WriteLine("Оценки: ");
        foreach (var grade in grades)
        {
            Console.WriteLine($"Предмет: {grade.Name}; Оценка: {grade.Grade};");
        }
    }
}
