using CrudApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Students
{
    public static class StudentsEndpoints
    {

        public static void AddStudentsEndpoints(this WebApplication app)
        {
            var studentsEndpoints = app.MapGroup("students");

            //Create an user
            studentsEndpoints.MapPost("", handler: async (AddStudentRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var alreadyExists = await context.Students
                    .AnyAsync(student => student.Name == request.Name, ct);

                if (alreadyExists) return Results.Conflict(error: "This name has already been taken!");

                var newStudent = new Student(request.Name);
                await context.Students.AddAsync(newStudent, ct);
                await context.SaveChangesAsync(ct);

                var studentReturn = new StudentDto(newStudent.Id, newStudent.Name);

                return Results.Ok(studentReturn);
            });

            //Return all active users
            studentsEndpoints.MapGet("", handler: async (AppDbContext context, CancellationToken ct) => 
                await context.Students
                .Where(student => student.Active)
                .Select(student => new StudentDto(student.Id, student.Name))
                .ToListAsync(ct));

            //Update the user's name
            studentsEndpoints.MapPut("{id:guid}",
                async (Guid id, UpdateStudentRequest request, AppDbContext context, CancellationToken ct) =>
                {
                    var student = await context.Students
                        .SingleOrDefaultAsync(student => student.Id == id, ct);

                    if (student == null) return Results.NotFound();

                    student.UpdateName(request.Name);
                    await context.SaveChangesAsync(ct);

                    return Results.Ok(new StudentDto(student.Id, student.Name));
                });

            //Delete: Soft Delete turning the active stat to false (zero)
            studentsEndpoints.MapDelete("{id}",
                    async (Guid id, AppDbContext context, CancellationToken ct) =>
                {
                    var student = await context.Students
                        .SingleOrDefaultAsync(student => student.Id == id, ct);

                    if (student == null) return Results.NotFound();

                    student.Deactivate();
                    await context.SaveChangesAsync(ct);

                    return Results.Ok();
                });
        }
    }
}
