using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace La_Subasta
{
    public class RedDeSubasta
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private NetworkStream[] clientStreams;
        private byte[][] buffers;
        private Client cliente = new Client();
        private object lockObj = new object();
        private Subasta subasta= new Subasta();
        private List<Thread> participantes = new List<Thread>();

        public RedDeSubasta(List<Thread> participantes)
        {
            // Utiliza la información de Subasta en el constructor
            this.participantes = participantes;
            //this.subasta = subasta;
            clientStreams = new NetworkStream[/*subasta.*/participantes.Count];
            buffers = new byte[/*subasta.*/participantes.Count][];

        }
        public void IniciarServidor()
        {
            server = new TcpListener(IPAddress.Any, 7777);
            server.Start();


            Console.WriteLine("Esperando conexiones de participantes...");

            //Profeee!!! no se logro establecer la coneccion.
            for (int i = 0; i < clientStreams.Length; i++)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);

                clientStreams[i] = client.GetStream();
                buffers[i] = new byte[1024]; // Tamaño del búfer
            }

            Console.WriteLine("Todos los participantes han sido conectados.");

            // Inicia un hilo para procesar mensajes de participantes
            Thread mensajeThread = new Thread(ProcesoMensajes);
            mensajeThread.Start();
        }

        public void EnviarMensaje(string mensaje, int clienteIndex)
        {
            lock (lockObj)
            {
                byte[] data = Encoding.UTF8.GetBytes(mensaje);
                clientStreams[clienteIndex].Write(data, 0, data.Length);
            }
        }

        public string RecibirMensaje(int clienteIndex)
        {
            lock (lockObj)
            {
                int bytesRead = clientStreams[clienteIndex].Read(buffers[clienteIndex], 0, buffers[clienteIndex].Length);
                return Encoding.UTF8.GetString(buffers[clienteIndex], 0, bytesRead);
            }
        }

        public void CerrarConexion()
        {
            foreach (var client in clients)
            {
                client.Close();
            }
            server.Stop();
        }

        private void ProcesoMensajes()
        {
            while (true)
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    string mensaje = RecibirMensaje(i);
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        Console.WriteLine($"Mensaje del participante {i}: {mensaje}");

                        string[] partes = mensaje.Split(':');
                        if (partes.Length == 2 && partes[0] == "OFERTA")
                        {
                            string nombre = partes[1].Split(',')[0];

                            subasta.Subastas(nombre);
                        }
                    }
                }
            }
        }
    }
}

