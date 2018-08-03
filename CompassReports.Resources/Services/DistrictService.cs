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
    public interface IDistrictService
    {
         Task<List<DistrictModel>> GetAll();
    }

    public class DistrictService : IDistrictService
    {
        private readonly DatabaseContext _db;

        public DistrictService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<DistrictModel>> GetAll()
        {
            return await _db.SchoolDimensions.Select(x => new DistrictModel
            {
                Id = x.LocalEducationAgencyKey,
                DistrictName = x.LEANameOfInstitution
            })
            .OrderBy(x => x.DistrictName)
            .Distinct()
            .ToListAsync();
        }
    }}
