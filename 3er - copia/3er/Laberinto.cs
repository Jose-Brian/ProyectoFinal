using System;
using System.Threading;

namespace _3er
{
    public class Laberinto
    {
        int nivelesCompletados;
        private string[,] mapa;
        private Jugador jugador;
        private MyStack<(int, int)> movimientosAnteriores;
        private ColaEnemigos colaEnemigos;
        private ColaRecompensas colaRecompensas;
        public HashtableObjetos inventario;
        public ArbolAVL ArbolPuntuaciones;
        private Random random = new Random();

        private bool objetoEscudoRecolectado = false; // Cambiado de S a escudo

        public Laberinto(int filas, int columnas, Jugador jugador)
        {
            mapa = new string[filas, columnas];
            this.jugador = jugador;
            movimientosAnteriores = new MyStack<(int, int)>();
            colaEnemigos = new ColaEnemigos();
            colaRecompensas = new ColaRecompensas();
            inventario = new HashtableObjetos();
            ArbolPuntuaciones = new ArbolAVL();

            GenerarMapa();
            ColocarObjetosUnicos();
        }

        private void ColocarObjetosUnicos()
        {
            if (!objetoEscudoRecolectado)
            {
                int posX = random.Next(0, mapa.GetLength(0));
                int posY = random.Next(0, mapa.GetLength(1));
                if (mapa[posX, posY] == ".")
                {
                    mapa[posX, posY] = "S"; // Colocamos un escudo en S
                }
                objetoEscudoRecolectado = true;
            }
        }

        public void ReiniciarLaberinto()
        {
            nivelesCompletados++;
            GenerarMapa();

            jugador.PosX = 0;
            jugador.PosY = 0;

            movimientosAnteriores.ClearM();

            Console.WriteLine("¡Has alcanzado la salida! El laberinto se ha reiniciado.");
            Console.WriteLine($"Niveles completados: {nivelesCompletados}");
        }

        private void GenerarMapa()
        {
            Random rand = new Random();
            for (int i = 0; i < mapa.GetLength(0); i++)
            {
                for (int j = 0; j < mapa.GetLength(1); j++)
                {
                    int chance = rand.Next(0, 100);
                    mapa[i, j] = chance < 8 ? "V" : chance < 23 ? "X" : ".";
                }
            }
            mapa[mapa.GetLength(0) - 1, mapa.GetLength(1) - 1] = "E"; // La salida se mantiene en E

            for (int i = 0; i < 10; i++)
            {
                int ex = rand.Next(0, mapa.GetLength(0));
                int ey = rand.Next(0, mapa.GetLength(1));
                if (mapa[ex, ey] == ".")
                {
                    mapa[ex, ey] = "M";
                    colaEnemigos.AgregarEnemigo($"Enemigo en {ex},{ey}");
                }
            }
        }

        public void MostrarMapa()
        {
            Console.Clear();
            for (int i = 0; i < mapa.GetLength(0); i++)
            {
                for (int j = 0; j < mapa.GetLength(1); j++)
                {
                    string celda;

                    if (i == jugador.PosX && j == jugador.PosY)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        celda = "J";
                    }
                    else if (mapa[i, j] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        celda = "X";
                    }
                    else if (mapa[i, j] == "S") // Celda de escudo
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        celda = "S";
                    }
                    else if (mapa[i, j] == "A")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        celda = "A";
                    }
                    else if (mapa[i, j] == "E") // Celda de salida
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        celda = "E";
                    }
                    else if (mapa[i, j] == "M")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        celda = "M";
                    }
                    else if (mapa[i, j] == "V")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        celda = "V";
                    }
                    else
                    {
                        celda = ".";
                    }
                    Console.Write(celda.PadRight(2));
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public void MoverJugador(string direccion)
        {
            int anteriorX = jugador.PosX;
            int anteriorY = jugador.PosY;

            // Movimiento del jugador
            if (direccion == "Arriba" && jugador.PosX > 0 && mapa[jugador.PosX - 1, jugador.PosY] != "X")
            {
                jugador.PosX--;
            }
            else if (direccion == "Abajo" && jugador.PosX < mapa.GetLength(0) - 1 && mapa[jugador.PosX + 1, jugador.PosY] != "X")
            {
                jugador.PosX++;
            }
            else if (direccion == "Izquierda" && jugador.PosY > 0 && mapa[jugador.PosX, jugador.PosY - 1] != "X")
            {
                jugador.PosY--;
            }
            else if (direccion == "Derecha" && jugador.PosY < mapa.GetLength(1) - 1 && mapa[jugador.PosX, jugador.PosY + 1] != "X")
            {
                jugador.PosY++;
            }
            else
            {
                Console.WriteLine("Movimiento no válido o hay un obstáculo.");
                Thread.Sleep(1000);
                return;
            }

            movimientosAnteriores.Push((anteriorX, anteriorY));

            // Ataque de enemigos
            if (colaEnemigos.HayEnemigos())
            {
                string enemigoActual = colaEnemigos.ProximoEnemigo();
                Console.WriteLine($"{enemigoActual} te ataca.");
                AplicarDañoEnemigo();
            }

            // Chequeo de casillas especiales
            if (mapa[jugador.PosX, jugador.PosY] == "S") // Escudo en S
            {
                jugador.TieneEscudo = true;
                Console.WriteLine("¡Has recogido un escudo (S)! Daño reducido en los próximos ataques.");
                mapa[jugador.PosX, jugador.PosY] = ".";
            }
            else if (mapa[jugador.PosX, jugador.PosY] == "A")
            {
                inventario.AgregarObjeto("Objeto de reducción de daño", 1);
                jugador.ReduceDaño = true;
                Console.WriteLine("¡Has recogido el objeto de reducción de daño (A)! Los enemigos te harán solo 5 de daño.");
                mapa[jugador.PosX, jugador.PosY] = ".";
            }
            else if (mapa[jugador.PosX, jugador.PosY] == "V")
            {
                int vidaExtra = colaRecompensas.ObtenerRecompensa();
                jugador.Salud = Math.Min(jugador.Salud + vidaExtra, 100);
                Console.WriteLine($"¡Has recogido una vida (V)! +{vidaExtra} de vida. Vida actual: {jugador.Salud}");
                inventario.AgregarObjeto("Vida", 1);
                mapa[jugador.PosX, jugador.PosY] = ".";
            }
            else if (mapa[jugador.PosX, jugador.PosY] == "M")
            {
                int danio = jugador.TieneEscudo ? 5 : 10;
                jugador.Salud -= danio;
                Console.WriteLine($"¡Has pisado una mina (M)! -{danio} de vida. Vida actual: {jugador.Salud}");
                if (jugador.Salud <= 0)
                {
                    Console.WriteLine("¡Te has quedado sin vida! El juego ha terminado.");
                    Thread.Sleep(1000);
                }
            }
            else if (mapa[jugador.PosX, jugador.PosY] == "E") // Salida en E
            {
                ReiniciarLaberinto();
            }
        }

        public void AplicarDañoEnemigo()
        {
            int danio = jugador.ReduceDaño ? 5 : 10;
            jugador.Salud -= danio;
            Console.WriteLine($"¡Un enemigo te atacó y te hizo {danio} de daño! Vida actual: {jugador.Salud}");
        }

        public bool VerificarVictoria()
        {
            if (jugador.Salud <= 0)
            {
                Console.WriteLine("¡Has perdido toda tu salud! ¡Derrota!");
                inventario.MostrarInventario();
                return true;
            }
            return false;
        }

        public void InsertarPuntuacion(int puntuacion)
        {
            ArbolPuntuaciones.Insertar(puntuacion);
        }

        public void DeshacerMovimiento()
        {
            if (!movimientosAnteriores.IsEmpty())
            {
                var ultimaPosicion = movimientosAnteriores.Pop();
                jugador.PosX = ultimaPosicion.Item1;
                jugador.PosY = ultimaPosicion.Item2;
            }
            else
            {
                Console.WriteLine("No hay movimientos para deshacer.");
            }
        }
    }
}
