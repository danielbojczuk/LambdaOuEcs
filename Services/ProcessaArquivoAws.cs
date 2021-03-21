using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System;
namespace Services
{
    public class ProcessaArquivoAws : IProcessaArquivoAws
    {
        private AmazonS3Client AwsClient;
        private string BucketName;
        private string ObjectKey;

        public async Task<string> Iniciar(string bucketName, string objectKey, string region) {
            this.BucketName = bucketName;
            this.ObjectKey = objectKey;
            this.AwsClient = new AmazonS3Client(Amazon.RegionEndpoint.GetBySystemName(region));
            string texto =await this.RetornaArquivoTexto();
            string[] linhas = texto.Split(new string[] { System.Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(linhas);
            return string.Join(Environment.NewLine,linhas);
        }

        private async Task<string> RetornaArquivoTexto() {
            GetObjectRequest solicitacao = new GetObjectRequest
            {
                BucketName = this.BucketName,
                Key = this.ObjectKey
            };

            GetObjectResponse retornoAws = await this.AwsClient.GetObjectAsync(solicitacao);

            string arquivoRetorno;
            using (StreamReader sr = new StreamReader(retornoAws.ResponseStream)) {
                arquivoRetorno = sr.ReadToEnd();
            }
            return arquivoRetorno;
        } 
    }
}
