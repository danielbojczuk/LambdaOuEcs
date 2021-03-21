using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace DotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArquivoAwsController : ControllerBase {
        private readonly ILogger<ArquivoAwsController> Logger;
        private IProcessaArquivoAws ProcessadorArquivo;

        public ArquivoAwsController(ILogger<ArquivoAwsController> logger, IProcessaArquivoAws processadorArquivo)        {
            this.Logger = logger;
            this.ProcessadorArquivo = processadorArquivo;
        }

        [HttpGet]
        public Task<string> Get() {
            return this.ProcessadorArquivo.Iniciar("bojczuk-misc-bucket","20k.txt","sa-east-1");
        }
        
    }
}