using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace La_Subasta
{
    public class FileWriter
    {
        private string mensaje;
        public void Escribir(string mensaje)
        {
            string fileName = "Ganador.txt";
            this.mensaje = mensaje;
            // Datos que se escribirán en el archivo
            string[] lines =  {mensaje//
            //"Línea 1: Esto es un ejemplo de escritura de archivo con buffer.",
            //"Línea 2: Usando un buffer para mejorar el rendimiento.",
            //"Línea 3: ¡Hola, mundo!"
        };

            // Tamaño del búfer (en bytes)
            int bufferSize = 1024;

            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create)) //En realidad se podría setear el tamaños del buffer aqui directamente
                using (BufferedStream bufferedStream = new BufferedStream(fileStream, bufferSize))
                using (StreamWriter writer = new StreamWriter(bufferedStream))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                        Console.WriteLine(line);
                    }

                }

                Console.WriteLine("Se ha escrito en el archivo con éxito.");
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("Error al escribir en el archivo: " + e.Message);
            }
        }
    }
}
