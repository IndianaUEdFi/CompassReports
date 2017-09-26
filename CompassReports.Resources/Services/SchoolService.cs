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
        
    }

    public class SchoolService : ISchoolService
    {
        private readonly DatabaseContext _db;

        public SchoolService(DatabaseContext db)
        {
            _db = db;
        }
    }}
