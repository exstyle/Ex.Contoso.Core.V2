namespace ContosoUniversityCore.Features.Student
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain;
    using Infrastructure;
    using MediatR;
    using ChartJSCore.Models;

    public class Details
    {
        public class Query : IRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Model
        {
            public int ID { get; set; }
            [Display(Name = "First Name")]
            public string FirstMidName { get; set; }
            public string LastName { get; set; }
            public DateTime EnrollmentDate { get; set; }
            public List<Enrollment> Enrollments { get; set; }

            public List<int> Notes { get; set; }

            public Chart chartNote { get; set; }

            public class Enrollment
            {
                public string CourseTitle { get; set; }
                public Grade? Grade { get; set; }
            }
        }

        public class Handler : IAsyncRequestHandler<Query, Model>
        {
            private readonly SchoolContext _db;

            public Handler(SchoolContext db)
            {
                _db = db;
            }

            public async Task<Model> Handle(Query message)
            {
                var result = await _db.Students.Where(s => s.Id == message.Id).ProjectToSingleOrDefaultAsync<Model>();
                result.Notes = new List<int>() { { 1 }, { 1 }, { 5 } };

                Chart chart = new Chart();

                chart.Type = "line";

                ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
                data.Labels = new List<string>() { "January", "February", "March", "April", "May", "June", "July" };

                LineDataset dataset = new LineDataset()
                {
                    Label = "My First dataset",
                    Data = new List<double>() { 65, 59, 80, 81, 56, 55, 40 },
                    Fill = false,
                    LineTension = 0.1,
                    BackgroundColor = "rgba(75, 192, 192, 0.4)",
                    BorderColor = "rgba(75,192,192,1)",
                    BorderCapStyle = "butt",
                    BorderDash = new List<int> { },
                    BorderDashOffset = 0.0,
                    BorderJoinStyle = "miter",
                    PointBorderColor = new List<string>() { "rgba(75,192,192,1)" },
                    PointBackgroundColor = new List<string>() { "#fff" },
                    PointBorderWidth = new List<int> { 1 },
                    PointHoverRadius = new List<int> { 5 },
                    PointHoverBackgroundColor = new List<string>() { "rgba(75,192,192,1)" },
                    PointHoverBorderColor = new List<string>() { "rgba(220,220,220,1)" },
                    PointHoverBorderWidth = new List<int> { 2 },
                    PointRadius = new List<int> { 1 },
                    PointHitRadius = new List<int> { 10 },
                    SpanGaps = false
                };

                data.Datasets = new List<Dataset>();
                data.Datasets.Add(dataset);

                chart.Data = data;
                result.chartNote = chart;

                return result;

            }
        }
    }
}