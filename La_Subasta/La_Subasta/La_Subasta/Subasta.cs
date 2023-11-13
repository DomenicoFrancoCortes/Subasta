using System;
using System.Collections.Generic;
using System.Threading;

namespace La_Subasta
{


    public class Subasta
    {
        public static string articulo = "";
        public static double valorInicial = 0;
        public static double valorActual = 0;
        public static int tiempoOferta = 3; // Tiempo en segundos para la oferta

        public static List<Thread> participantes = new List<Thread>();

        static object lockObj = new object();
        static string ganadorNombre = "";
        static double ganadorValor = 0;
        private static Subasta subasta;
        public string mensaje = "";
        public FileWriter escribirMensaje = new FileWriter();

        public void Jugadores()
        {

            Console.WriteLine("=====================================");
            Console.WriteLine("           Subasta en Curso           ");
            Console.WriteLine("=====================================");
            Console.Write("Ingrese el nombre del artículo: ");
            articulo = Console.ReadLine();

            Console.Write("Ingrese el valor inicial: ");
            valorInicial = Convert.ToDouble(Console.ReadLine());
            valorActual = valorInicial;

            Console.WriteLine("Ingrese los nombres de los participantes (escriba 'fin' para finalizar)");

            


            while (true)
            {
                string nombre = Console.ReadLine();
                if (nombre.ToLower() == "fin")
                    break;

                Thread participante = new Thread(() => Subastas(nombre));
                participantes.Add(participante);
            }
            RedDeSubasta redDeSubasta = new RedDeSubasta(participantes/*subasta*/);
            redDeSubasta.IniciarServidor();
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("          Comienza la Subasta         ");
            Console.WriteLine("=====================================");
            Console.WriteLine($"El articulo a subastar es: {articulo} y su valor comienza en: {valorInicial}");
            Thread.Sleep(1000); // Espera un segundo antes de iniciar la subasta

            foreach (var participante in participantes)
            {
                participante.Start();
            }

            foreach (var participante in participantes)
            {
                participante.Join();
            }



            Console.WriteLine($"=====================================");
            Console.WriteLine($"   La subasta ha terminado. Ganador: {ganadorNombre} - Oferta: ${ganadorValor}");
            Console.WriteLine($"=====================================");

        }

        public void Subastas(string nombre)
        {
            while (tiempoOferta > 0)
            {
                lock (lockObj)
                {
                    Console.WriteLine($"{nombre}, ¿desea ofertar por {articulo}? (Tiempo restante: {tiempoOferta}s)");
                    Console.WriteLine("1. Ofertar");
                    Console.WriteLine("2. No ofertar");

                    int opcion = int.Parse(Console.ReadLine());

                    if (opcion == 1)
                    {
                        double oferta;
                        Console.Write($"Ingrese su oferta por {articulo}: ");
                        oferta = Convert.ToDouble(Console.ReadLine());

                        if (oferta > valorActual)
                        {
                            valorActual = oferta;
                            ganadorNombre = nombre;
                            ganadorValor = valorActual;
                            // Console.WriteLine($"El valor ofertado por {nombre} es: {valorActual} ");
                        }
                    }
                    else if (opcion == 2)
                    {
                        continue;
                        //break;
                    }
                }

                Thread.Sleep(100); // Pequeño tiempo de espera entre ofertas para evitar conflictos

                lock (lockObj)
                {
                    Console.WriteLine($"Tiempo restante: {tiempoOferta}s");
                    tiempoOferta--;
                }

                Thread.Sleep(1000); // Espera para la siguiente ronda de ofertas
            }
             
            mensaje = $"La subasta ha terminado. El ganador es {ganadorNombre} con una oferta de ${ganadorValor}" ;
            escribirMensaje.Escribir(mensaje);

            Console.ReadLine();
        }
    }
}
