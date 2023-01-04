
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Receiver_A_Async_RCP_POC
{
    public class Program
    {
               
        static void Main(string[] args)
        {
            RpcClient _rpcClient = new RpcClient();

            _rpcClient.LogMessage += (m) =>
            {
                // Implementar com um Mediator ou um EventHandler service, hostedService
                //Application.Current.Dispatcher.Invoke(() => LogMessages.Add(m));
            };

            _rpcClient.InitializeAndRun();

            Console.WriteLine("Hello, World!");
        }

        public ObservableCollection<string> LogMessages { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}