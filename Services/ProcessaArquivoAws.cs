using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System;
using System.Diagnostics;
namespace Services
{
    public class ProcessaArquivoAws : IProcessaArquivoAws
    {
        private AmazonS3Client AwsClient;
        private string BucketName;
        private string ObjectKey;
        private Stopwatch Temporizador;

        private TimeSpan TempoSort;

        private TimeSpan TempoRetornaArquivo;

        public ProcessaArquivoAws() {
            this.Temporizador = new Stopwatch();
        }

        public async Task<string> Iniciar(string bucketName, string objectKey, string region) {
            this.BucketName = bucketName;
            this.ObjectKey = objectKey;
            this.AwsClient = new AmazonS3Client(Amazon.RegionEndpoint.GetBySystemName(region));
            this.IniciarTimer();
            string texto =await this.RetornaArquivoTexto();
            this.PararTimer(out this.TempoRetornaArquivo);
            this.ReiniciarTimer();
            string[] linhas = texto.Split(new string[] { System.Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(linhas);
            this.PararTimer(out this.TempoSort);
            Console.WriteLine("Tempos,{0},{1}",this.TempoRetornaArquivo,this.TempoSort);
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

        private void IniciarTimer() {
            this.Temporizador.Start();
        }

        private void PararTimer(out TimeSpan tempoArmazenado) {
            this.Temporizador.Stop();
            tempoArmazenado = this.Temporizador.Elapsed;
        } 

        private void ReiniciarTimer() {
            this.Temporizador.Reset();
            this.Temporizador.Start();
        } 

    }
}
