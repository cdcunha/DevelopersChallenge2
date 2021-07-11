using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        [HttpPost]
        public ActionResult ReconcileBankStatement(List<string> ofxFiles)
        {
            if (ofxFiles == null || ofxFiles.Count < 2)
            {
                return BadRequest("At least 2 files are required to do the bank reconciliation");
            }



            throw new NotImplementedException();
        }
    }
}
