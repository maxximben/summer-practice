using System.Collections.Generic;
using System.Linq;
using task02;
using Xunit;

public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    public StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new Student { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new Student { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new Student { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
        };
        _service = new StudentService(_testStudents);
    }

    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.Equal(2, result.Count);
        Assert.True(result.All(s => s.Faculty == "ФИТ"));
    }

    [Fact]
    public void GetStudentsWithMinAverageGrade_ReturnsStudentsAboveThreshold()
    {
        var result = _service.GetStudentsWithMinAverageGrade(4.5).ToList();
        Assert.Equal(2, result.Count);
        Assert.Contains(result, s => s.Name == "Иван"); // Средний балл: (5+4+5)/3 = 4.67
        Assert.Contains(result, s => s.Name == "Петр"); // Средний балл: (5+5+5)/3 = 5
        Assert.DoesNotContain(result, s => s.Name == "Анна"); // Средний балл: (3+4+3)/3 = 3.33
    }

    [Fact]
    public void GetStudentsOrderedByName_ReturnsStudentsInAlphabeticalOrder()
    {
        var result = _service.GetStudentsOrderedByName().ToList();
        Assert.Equal(3, result.Count);
        Assert.Equal("Анна", result[0].Name);
        Assert.Equal("Иван", result[1].Name);
        Assert.Equal("Петр", result[2].Name);
    }

    [Fact]
    public void GroupStudentsByFaculty_ReturnsCorrectGroups()
    {
        var result = _service.GroupStudentsByFaculty();
        Assert.Equal(2, result.Count); // Два факультета: ФИТ и Экономика
        Assert.Equal(2, result["ФИТ"].Count());
        Assert.Equal(1, result["Экономика"].Count());
        Assert.Contains(result["ФИТ"], s => s.Name == "Иван");
        Assert.Contains(result["ФИТ"], s => s.Name == "Анна");
        Assert.Contains(result["Экономика"], s => s.Name == "Петр");
    }

    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.Equal("Экономика", result); // Средний балл Экономики: 5, ФИТ: (4.67 + 3.33)/2 = 4
    }
}