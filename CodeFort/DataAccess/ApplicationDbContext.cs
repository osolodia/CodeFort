using CodeFort.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFort.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProgramData> ProgramsData { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string pathDbSqlite = string.Empty;
            string nameDb = "PasswordFortress.db";
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                pathDbSqlite = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                pathDbSqlite = Path.Combine("Filename =", pathDbSqlite, nameDb);
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                pathDbSqlite = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                pathDbSqlite = Path.Combine("Filename =", pathDbSqlite, "..", "Library", nameDb);
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                pathDbSqlite = $"Data Source={nameDb}";
            }
            optionsBuilder.UseSqlite(pathDbSqlite);
        }
    }
}
