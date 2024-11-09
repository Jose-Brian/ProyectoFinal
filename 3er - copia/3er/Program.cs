using _3er;
using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        Ordenamiento ordenamiento = new Ordenamiento();
        ArbolAVL arbol = new ArbolAVL();
        bool jugarDeNuevo = true;

        while (jugarDeNuevo)
        {
            Console.WriteLine("¡Bienvenido a Dungeon Explorer!");
            Console.Write("Ingresa tu nombre: ");
            string nombreJugador = Console.ReadLine();
            Jugador jugador = new Jugador(nombreJugador);
            Laberinto laberinto = new Laberinto(15, 15, jugador);

            bool jugando = true;
            while (jugando)
            {
                laberinto.MostrarMapa();
                Console.WriteLine($"Salud: {jugador.Salud}");
                Console.WriteLine("Opciones:");
                Console.WriteLine("1. Mover Arriba");
                Console.WriteLine("2. Mover Abajo");
                Console.WriteLine("3. Mover Izquierda");
                Console.WriteLine("4. Mover Derecha");
                Console.WriteLine("5. Deshacer movimiento");
                Console.WriteLine("6. Salir");
                Console.Write("Selecciona una opción (1-6): ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        laberinto.MoverJugador("Arriba");
                        break;
                    case "2":
                        laberinto.MoverJugador("Abajo");
                        break;
                    case "3":
                        laberinto.MoverJugador("Izquierda");
                        break;
                    case "4":
                        laberinto.MoverJugador("Derecha");
                        break;
                    case "5":
                        laberinto.DeshacerMovimiento();
                        break;
                    case "6":
                        jugando = false;
                        jugador.Salud = 0;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (laberinto.VerificarVictoria())
                {
                    jugando = false;
                }
            }

            laberinto.InsertarPuntuacion(jugador.Salud);
            laberinto.inventario.MostrarInventario();

            Console.WriteLine("¿Quieres jugar de nuevo? (s/n): ");
            jugarDeNuevo = Console.ReadLine().ToLower() == "s";
            arbol.Insertar(jugador.Salud);
        }

        Console.WriteLine("Historial de jugadores:");
        ordenamiento.MostrarPuntuacionesDescendente(arbol.Raiz);
        Console.WriteLine("¡Gracias por jugar Dungeon Explorer!");
    }
}
