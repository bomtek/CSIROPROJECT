using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CSIRO_PROJECT.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CSIRO_PROJECT.ViewModels;

namespace CSIRO_PROJECT.Models
{


    public class ApplicationDBContext : IdentityDbContext
    {
        public DbSet<UserModel> users { get; set; }
        public DbSet<UniversityModel> universities { get; set; }

        public DbSet<CourseModel> courses { get; set; } 

        public DbSet<UniCourse> uniCourses { get; set; }




        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}





















