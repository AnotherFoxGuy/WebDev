using DeepStreamNet;
using DeepStreamNet.Contracts;
using System;
using System.Threading.Tasks;

namespace Tres_poker_management_application
{
    public class DeepStreamConnector
    {
        private DeepStreamClient _client;

        public DeepStreamClient Client => _client;
        public IDeepStreamEvents Events => _client.Events;
        public IDeepStreamRecords Records => _client.Records;
        
        
        public IDeepStreamRecord GetRecord(string recname)
        {
            var task = Task.Run(async () => await _client.Records.GetRecordAsync(recname));
            task.Wait();
            return task.Result;
        }
        
        public void UpdateRecord(string recname, string keyname, string data)
        {
            var task = Task.Run(async () => await _client.Records.GetRecordAsync(recname));
            task.Wait();
            task.Result[keyname] = data;
        }


        #region Singleton

        private static readonly Lazy<DeepStreamConnector> LazyDeepStreamConnector =
                new Lazy<DeepStreamConnector>(() => new DeepStreamConnector());

        public static DeepStreamConnector Instance => LazyDeepStreamConnector.Value;

        private DeepStreamConnector()
        {
            _client = new DeepStreamClient("136.144.231.71", 8181);

            var task = Task.Run(async () => await _client.LoginAsync());
            task.Wait();
        }

        #endregion

   
    }
}