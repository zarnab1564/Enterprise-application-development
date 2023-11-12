using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
using (var context = new MyContext())
{
    // Create and save a new Students
    Console.WriteLine("Adding new students");

    var student = new Student
    {
        FirstMidName = "Atyia",
        LastName = "Alam",
        Gender = "Female",
        Age = 21,
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students.Add(student);

    var student1 = new Student
    {
        FirstMidName = "Ali",
        LastName = "Ahmed",
        Gender = "Male",  
        Age = 18,
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students.Add(student1);
    context.SaveChanges();

    // Display all Students from the database
    var students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1} , Gender:{2}, Age:{3}", stdnt.ID, name , stdnt.Gender , stdnt.Age);
    }

    Console.WriteLine("Updating student information");
    var studentToUpdate = context.Students.FirstOrDefault(s => s.ID == 5);

    if (studentToUpdate != null)
    {
        Console.WriteLine("Updating student information");

        studentToUpdate.Gender = "Femaleeeeeee";
        studentToUpdate.Age = 50;
        context.SaveChanges();
    }
    // Display all Students from the database after updating
    var studentss = (from s in context.Students
                     orderby s.FirstMidName
                     select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in studentss)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, Gender:{2}, Age:{3}", stdnt.ID, name, stdnt.Gender, stdnt.Age);
    }

    Console.WriteLine("Deleting a student");
    var studentToDelete = context.Students.FirstOrDefault(s => s.ID == 7);

    if (studentToDelete != null)
    {
        context.Students.Remove(studentToDelete);
        context.SaveChanges();

    }

    // Display all Students from the database after deleting
    var studentsss = (from s in context.Students
                      orderby s.FirstMidName
                      select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in studentsss)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, Gender:{2}, Age:{3}", stdnt.ID, name, stdnt.Gender, stdnt.Age);
    }



    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
public enum Grade
{
    A, B, C, D, F
}
public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course? Course { get; set; }
    public virtual Student? Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string? LastName { get; set; }
    public string? FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public string? Gender { get; set; }  // Added Gender field
    public int? Age { get; set; }  // Added Age field
    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}
public class Course
{
    public int CourseID { get; set; }
    public string? Title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class MyContext : DbContext
{
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyContextDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

}