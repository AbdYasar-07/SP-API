using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP_API.Model;
using SP_API.Response;
using SP_API.SpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SP_API.Controllers
{
    [ApiController]
    public class GreenLineController : ControllerBase
    {

        private readonly SharingServiceInterface serviceLayer;

        public GreenLineController(SharingServiceInterface serviceInterface)
        {
            serviceLayer = serviceInterface;
        }


        [HttpPost]
        [Route("project/regsiter/add")]
        public IActionResult gettingInsertedValue(List<ProjectSite> projectSite)
        {
            try
            {
                var getRecordAndPush = serviceLayer.gettingConfiguration(projectSite);
                if (getRecordAndPush != null && getRecordAndPush.HttpStatus == 200)
                    return Ok(getRecordAndPush);
                else
                    return BadRequest();
            }
            catch(Exception)
            {
                return NoContent();
            }
        }
    }
}
