using System;
using System.Collections;

namespace _3er
{
    public class HashtableObjetos
    {
        private Hashtable objetos;

        public HashtableObjetos()
        {
            objetos = new Hashtable(); // Inicialización correcta aquí
        }

        public void AgregarObjeto(string nombre, int cantidad)
        {
            if (objetos.ContainsKey(nombre))
            {
                objetos[nombre] = (int)objetos[nombre] + cantidad;
            }
            else
            {
                objetos.Add(nombre, cantidad);
            }
        }

        public void MostrarInventario()
        {
            Console.WriteLine("Inventario del jugador:");
            if (objetos.Count == 0)
            {
                Console.WriteLine("El inventario está vacío.");
                return;
            }
            foreach (DictionaryEntry entry in objetos)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }
}
