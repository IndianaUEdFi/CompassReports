using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data.Context;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface ISchoolService
    {
         Task<List<SchoolModel>> GetAll();
    }

    public class SchoolService : ISchoolService
    {
        private readonly DatabaseContext _db;

        public SchoolService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<SchoolModel>> GetAll()
        {
            return await _db.SchoolDimensions.Select(x => new SchoolModel
            {
                Id = x.SchoolKey,
                SchoolName = x.NameOfInstitution,
                DistrictId = x.LocalEducationAgencyKey,
                DistrictName = x.LEANameOfInstitution
            }).OrderBy(x => x.SchoolName).ToListAsync();
        }
    }}
