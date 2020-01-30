
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WellCare.AzureApi.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly Process _funcHostProcess;
        public CloudQueue OrdersQueue;
        public CloudBlobContainer VipOrdersContainer;
        public CloudBlobContainer NormalOrdersContainer;

        public HttpClient Client;
       
        public static readonly int Port = FindFreePort();
        public readonly string BaseUrl = $"http://localhost:{Port}";

        public static int FindFreePort()
        {
            int port = 0;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
                socket.Bind(localEP);
                localEP = (IPEndPoint)socket.LocalEndPoint;
                port = localEP.Port;
            }
            finally
            {
                socket.Close();
            }
            return port;
        }

        public TestFixture()
        {
            var dotnetExePath = Environment.ExpandEnvironmentVariables(ConfigurationHelper.Settings.DotNetExecutablePath);
            var functionHostPath = Environment.ExpandEnvironmentVariables(ConfigurationHelper.Settings.FunctionHostPath);
            var functionAppFolder = Path.GetRelativePath(Directory.GetCurrentDirectory(), ConfigurationHelper.Settings.FunctionApplicationPath);

            _funcHostProcess = new Process
            {
                StartInfo =
                {
                    FileName = dotnetExePath,
                    Arguments = $"\"{functionHostPath}\" start -p {Port} --pause-on-error",
                    WorkingDirectory = functionAppFolder,
                    UseShellExecute = true,
                    CreateNoWindow = false
                }
            };
            var success = _funcHostProcess.Start();
            if (!success)
            {
                throw new InvalidOperationException("Could not start Azure Functions host.");
            }

            InitializeHttpClient();

        }

        public bool InitializeHttpClient()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUrl);
            return true;
        }



        public virtual void Dispose()
        {
            try
            {
                _funcHostProcess.Kill();
            }
            catch
            {

            }
            finally
            {
                _funcHostProcess.Dispose();
            }
        }
    }
}

