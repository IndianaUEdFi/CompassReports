using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The School resource endpoint.
    /// </summary>

    [RoutePrefix("api/district")]
    public class DistrictController : ApiController
    {
        private readonly IDistrictService _districtService;
        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var districts = await _districtService.GetAll();
            return Ok(districts);
        }
    }
}
