using DevelopersChallenge2.Entities;
using DevelopersChallenge2.Helper;
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
        public ActionResult ReconcileBankStatement(List<string> ofxTxtFiles)
        {
            if (ofxTxtFiles == null || ofxTxtFiles.Count < 2)
            {
                return BadRequest("At least 2 files are required to do the bank reconciliation");
            }

            List<BankTransaction> bankTransactions = new ();

            foreach (string ofxTxtFile in ofxTxtFiles)
            {
                var bankTransactionsPerFile = OfxHelper.OfxToBankTransactions(ofxTxtFile);
                if (bankTransactions.Count == 0)
                {
                    bankTransactions.AddRange(bankTransactionsPerFile);
                }
                else
                {
                    foreach (var item in bankTransactionsPerFile)
                    {
                        if (bankTransactions.Any(e => e.Equals(item)))
                        {
                            bankTransactions.Add(item);
                        }
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
