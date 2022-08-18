using System;
using SuperSimpleTcp;
using System.Text;

namespace workmateServerLinux{
    public partial class Server{

        SimpleTcpServer server = new SimpleTcpServer("0.0.0.0:16460");
        List<string> clients = new List<string>();
        public static int Main(){
            bool exit=false;
            var program = new Server();
            
            program.server.Events.ClientConnected += ClientConnected;
            program.server.Events.ClientDisconnected += ClientDisconnected;
            program.server.Events.DataReceived += DataReceived;
        
            program.server.Start();
            Console.WriteLine("Server started");
            while(!exit){
                if(Console.ReadLine()=="stop")
                    exit=true;
            }
            return 0;
        }
        private static void DataReceived(object? sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            var program = new Server();
            /*this.Invoke((MethodInvoker)delegate
            {*/
                Console.WriteLine($"{DateTime.Now} - {e.IpPort}: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}");

                if (Encoding.UTF8.GetString(e.Data) == "Hello, world!")
                {
                    program.server.Send($"{e.IpPort}", "Hello from server");
                }
                else if (Encoding.UTF8.GetString(e.Data) == "Magazzino aggiornato")
                {
                    for (int i = 0; i < program.clients.Count; i++)
                        if (program.clients[i] != e.IpPort)
                            program.server.Send($"{program.clients[i]}", "Aggiornare magazzino");
                }
                else if (Encoding.UTF8.GetString(e.Data) == "Prodotti aggiornati")
                {
                    for (int i = 0; i < program.clients.Count; i++)
                        if (program.clients[i] != e.IpPort)
                            program.server.Send($"{program.clients[i]}", "Aggiornare prodotti");
                }
                else if (Encoding.UTF8.GetString(e.Data) == "Ordini aggiornati")
                {
                    for (int i = 0; i < program.clients.Count; i++)
                        if (program.clients[i] != e.IpPort)
                            program.server.Send($"{program.clients[i]}", "Aggiornare ordini");
                }
                else if (Encoding.UTF8.GetString(e.Data) == "Clienti aggiornati")
                {
                    for (int i = 0; i < program.clients.Count; i++)
                        if (program.clients[i] != e.IpPort)
                            program.server.Send($"{program.clients[i]}", "Aggiornare clienti");
                }
                else if (Encoding.UTF8.GetString(e.Data) == "Acquisti aggiornati")
                {
                    for (int i = 0; i < program.clients.Count; i++)
                        if (program.clients[i] != e.IpPort)
                            program.server.Send($"{program.clients[i]}", "Aggiornare acquisti");
                }
            //});
        }
        private static void ClientDisconnected(object? sender, SuperSimpleTcp.ConnectionEventArgs e)
        {
            var program = new Server();
            //this.Invoke((MethodInvoker)delegate
            //{
                Console.WriteLine($"{DateTime.Now} - {e.IpPort}: Disconnesso.{Environment.NewLine}");
                program.clients.Remove(e.IpPort);
            //});
        }
        private static void ClientConnected(object? sender, SuperSimpleTcp.ConnectionEventArgs e)
        {
            var program = new Server();
            //this.Invoke((MethodInvoker)delegate
            //{
                Console.WriteLine($"{DateTime.Now} - {e.IpPort}: Connesso.{Environment.NewLine}");
                program.clients.Add(e.IpPort);
            //});
        }
    }
}