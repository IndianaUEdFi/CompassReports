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
         Task<List<SchoolModel>> GetAll(int[] districtId = null);
    }

    public class SchoolService : ISchoolService
    {
        private readonly DatabaseContext _db;

        public SchoolService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<SchoolModel>> GetAll(int[] districtId = null)
        {
            var query = _db.SchoolDimensions.AsQueryable();
            if (districtId != null && districtId.Length > 0)
                query = query.Where(x => districtId.Contains(x.LocalEducationAgencyKey));

            return await query.Select(x => new SchoolModel
            {
                Id = x.SchoolKey,
                SchoolName = x.NameOfInstitution,
                DistrictId = x.LocalEducationAgencyKey,
                DistrictName = x.LEANameOfInstitution
            }).OrderBy(x => x.SchoolName).ToListAsync();
        }
    }}
