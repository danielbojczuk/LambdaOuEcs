using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace Services
{
    public interface IProcessaArquivoAws
    {
        Task<string> Iniciar(string bucketName, string objectKey, string region);
    }
}
