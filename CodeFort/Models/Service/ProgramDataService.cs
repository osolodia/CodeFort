using CodeFort.DataAccess;
using CodeFort.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFort.Models.Service
{
    public class ProgramDataService
    {
        private readonly ApplicationDbContext dbContext;
        public ProgramDataService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(ProgramData data)
        {
            dbContext.ProgramsData.Add(data);
            dbContext.SaveChanges();
        }
        public List<ProgramData> GetAll()
        {
            return dbContext.ProgramsData.ToList();
        }
        public ProgramData? GetById(int id)
        {
            return dbContext.ProgramsData.FirstOrDefault(i => i.Id == id);
        }
        public void RemoveAll()
        {
            var list = GetAll();
            foreach (var item in list)
            {
                dbContext.ProgramsData.Remove(item);
            }
            dbContext.SaveChanges();
        }
        public ProgramData? Remove(ProgramData data)
        {
            if (data != null)
            {
                dbContext.ProgramsData.Remove(data);
                dbContext.SaveChanges();
            }
            return data;
        }
        public ProgramData? UpdateById(int id, ProgramData data)
        {
            ProgramData? updated = GetById(id);
            if (updated != null)
            {
                updated.Name = data.Name;
                updated.Login = data.Login;
                updated.Password = data.Password;

                dbContext.ProgramsData.Update(updated);
                dbContext.SaveChanges();
            }
            return updated;
        }
    }
}
