using DevelopersChallenge2.Entities;
using DevelopersChallenge2.Helper;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    /// <summary>
    /// Controller responsible for all imports
    /// </summary>
    public class ImportController : ControllerBase
    {
        [HttpPost]
        /// <summary>
        /// Make the Reconcile Bank Statement using the OFX file content. 
        /// Each item of the list must be the content of one file
        /// </summary>
        /// <param name="files">List of the content of the OFX files</param>
        public async Task<ActionResult<IList<BankTransaction>>> ReconcileBankStatement([FromForm] IEnumerable<IFormFile> files)
        {
            List<BankTransaction> bankTransactions = new();

            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            
            if (files == null || files.Count() < 2)
            {
                return BadRequest("At least 2 files are required to do the bank reconciliation");
            }

            foreach (var file in files)
            {
                try
                {
                    var stream = file.OpenReadStream();
                    StreamReader reader = new(stream);
                    string fileContent = reader.ReadToEnd();

                    var bankTransactionsPerFile = OfxHelper.OfxToBankTransactions(fileContent);

                    //If the list is empty then add all transactions converted from the OFX Text
                    if (bankTransactions.Count == 0)
                    {
                        bankTransactions.AddRange(bankTransactionsPerFile);
                    }
                    else
                    {
                        //If the list is NOT empty then check item by item
                        foreach (var item in bankTransactionsPerFile)
                        {
                            //If the transaction don't exist in the list, then the transaction will be added in the list 
                            if (!bankTransactions.Contains(item, new BankTransactionEqualityComparer()))
                            {
                                bankTransactions.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Error during import");
                }
            }

            return new CreatedResult(resourcePath.ToString(), bankTransactions);

            //Rerturn list with all transactions
            //return Ok(bankTransactions);
        }
    }
}
