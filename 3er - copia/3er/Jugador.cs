using System;

namespace _3er
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Salud { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool ReduceDaño { get; set; }
        public bool TieneEscudo { get; set; } // Escudo para reducir daño

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Salud = 100;
            PosX = 0;
            PosY = 0;
            ReduceDaño = false;
            TieneEscudo = false; // Inicializamos sin escudo
        }
    }
}
