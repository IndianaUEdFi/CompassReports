using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data.Context;

namespace CompassReports.Resources.Services
{
    public interface ISchoolService
    {
         void Get();
    }

    public class SchoolService : ISchoolService
    {
        private readonly DatabaseContext _db;

        public SchoolService(DatabaseContext db)
        {
            _db = db;
        }

        public void Get()
        {
            var model = _db.SchoolDimensions.ToList();
            var temp = model;
        }
    }}
