namespace ContosoUniversityCore.Features.Course
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using MediatR;
    using Domain;
    using Infrastructure;
    using FluentValidation;

    public class Create
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(m => m.Number).NotEqual(0);
                RuleFor(m => m.Title).NotNull().Length(3, 50);
                RuleFor(m => m.Credits).InclusiveBetween(0,5);
                RuleFor(m => m.Department).NotNull();
            }
        }

        public class Command : IRequest
        {
            [IgnoreMap]
            public int Number { get; set; }
            public string Title { get; set; }
            public int Credits { get; set; }
            public Department Department { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly SchoolContext _db;

            public Handler(SchoolContext db)
            {
                _db = db;
            }

            public void Handle(Command message)
            {
                var course = Mapper.Map<Command, Course>(message);
                course.Id = message.Number;

                _db.Courses.Add(course);
            }
        }
    }
}