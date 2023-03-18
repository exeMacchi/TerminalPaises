using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantillaPais;
using Funciones;

namespace TerminalPaises
{
    class Program
    {
        static void Main()
        {
            string usuario = Funciones.Registro.PedirNombreUsuario();
            int cantPaises = 1;
            byte opcion = 0;
            bool salida = false;
            // En este ejemplo solo se pueden agregar hasta 100 países.
            const int tamMax = 100;
            PlantillaPais.RegistroPaises[] paises = new RegistroPaises[tamMax];
            Funciones.Registro.InstanciarDatos(paises, tamMax);
            // Se introduce manualmente un dato de prueba para el usuario.
            Funciones.Registro.RellenarDatos(paises, cantPaises);

            while (!salida)
            {
                switch (opcion)
                {
                    //  ---------- Menú principal ----------//
                    case 0:
                        opcion = Funciones.Terminal.MostrarMenuPrincipal(usuario); 
                        break;

                    // ---------- Buscar país ----------//
                    case 1:
                        opcion = Funciones.Terminal.BuscarPais(paises, cantPaises);
                        break;
                    // ---------- Modificar país ----------//
                    case 2:
                        opcion = Funciones.Terminal.ModificarPaises(paises, cantPaises);
                        break;

                    // ---------- Agregar país ---------- //
                    case 3:
                        Funciones.Terminal.AgregarNuevoPais(paises, ref cantPaises);
                        Console.Clear();
                        opcion = 0;
                        break;
                    // ---------- Salida ---------- //
                    case 4:
                        Console.Clear();
                        salida = true;
                        break;
                    // ---------------------------- //
                    default:
                        Console.Clear();
                        opcion = 0;
                        break;
                }
            }
            Console.WriteLine($"Hasta la próxima, {usuario}!\n");
        }
    }
}
