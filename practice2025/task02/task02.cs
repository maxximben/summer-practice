using System;
using System.Collections.Generic;
using System.Linq;

namespace task02
{
    public class Student
    {
        public string Name { get; set; }
        public string Faculty { get; set; }
        public List<int> Grades { get; set; }
    }

    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService(List<Student> students) => _students = students;

        // 1. Возвращает студентов указанного факультета
        public IEnumerable<Student> GetStudentsByFaculty(string faculty)
        {
            var students = from student in _students where student.Faculty == faculty select student;
            return students;
        }

        // 2. Возвращает студентов со средним баллом >= minAverageGrade
        public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade)
        {
            var students = from student in _students where student.Grades.Average() >= minAverageGrade select student;
            return students;
        }

        // 3. Возвращает студентов, отсортированных по имени (A-Z)
        public IEnumerable<Student> GetStudentsOrderedByName()
        {
            var students = from student in _students orderby student.Name select student;
            return students;
        }


        // 4. Группировка по факультету
        public ILookup<string, Student> GroupStudentsByFaculty()
        {
            ILookup<string, Student> students = _students.ToLookup(student => student.Faculty);
            return students;
        }

        // 5. Находит факультет с максимальным средним баллом
        public string GetFacultyWithHighestAverageGrade()
            => _students
                .GroupBy(s => s.Faculty)
                .Select(g => new { Faculty = g.Key, Average = g.Where(s => s.Grades.Any()).Average(s => s.Grades.Average()) })
                .OrderByDescending(g => g.Average)
                .Select(g => g.Faculty)
                .FirstOrDefault();
    }
}





