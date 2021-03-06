using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DotnetLambda
{
    public class Function
    {
        
        IProcessaArquivoAws ProcessaArquivoAws;
        public Function() {
            this.ProcessaArquivoAws = new ProcessaArquivoAws();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<string> FunctionHandler(ILambdaContext context)
        {
            return this.ProcessaArquivoAws.Iniciar("bojczuk-misc-bucket","20k.txt","sa-east-1");
        }
    }
}
