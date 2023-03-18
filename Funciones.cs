using System;
using PlantillaPais;

namespace Funciones
{
    public class Registro
    {
        public static void InstanciarDatos(RegistroPaises[] paises, in int tamMax)
        {
            for (int i = 0; i < tamMax; i++)
            {
                paises[i] = new RegistroPaises();
            }
        }
        public static void RellenarDatos(RegistroPaises[] pais, in int cantPaises)
        {
            Console.WriteLine("Cargando datos... \n");

            for (int i = 0; i < cantPaises; i++)
            {
                Console.WriteLine($"---------------- País {i + 1} ----------------");
                Console.Write("Nombre: ");
                pais[i].Nombre = Console.ReadLine().ToUpper();

                Console.Write("Continente: ");
                pais[i].Continente = Console.ReadLine().ToUpper();

                Console.Write("Código telefónico internacional: +");
                pais[i].CodigoInternacional = short.Parse(Console.ReadLine());

                Console.Write("Población: ");
                pais[i].Poblacion = Console.ReadLine();

                Console.Write("Superficie: ");
                pais[i].Superficie = float.Parse(Console.ReadLine());
                Console.WriteLine();
            }

            Console.Clear();
        }
        public static string PedirNombreUsuario()
        {
            Console.WriteLine("Cargando datos... \n");
            Console.Write("¿Cuál es tu nombre?: ");
            string nombre = Console.ReadLine();
            Console.Clear();
            return nombre;
        }
    }

    public class Terminal
    {
        // ---------- Menú principal ---------- //
        public static byte MostrarMenuPrincipal(string usuario)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"     Bienvenido a la terminal de países, {usuario}");
            Console.WriteLine("1. Buscar país");
            Console.WriteLine("2. Modificar país");
            Console.WriteLine("3. Añadir nuevo país");
            Console.WriteLine("4. Salir de la terminal");
            Console.WriteLine("========================================================");
            Console.WriteLine();

            byte opcion = 0;

            try
            {
                do
                {
                    Console.Write("¿Qué quiere hacer?: ");
                    opcion = byte.Parse(Console.ReadLine());

                    if (opcion > 4 || opcion < 1)
                    {
                        Console.WriteLine("Error: opción no disponible.");
                    }

                } while (opcion > 4 || opcion < 1);
                Console.Clear();

                return opcion;
            }
            catch
            {
                Avisos.AvisoError();
                return opcion;
            }
        }

        // ---------- Buscar ---------- //
        public static byte BuscarPais(RegistroPaises[] paises, int cantPaises)
        {
            MostrarListaPaises(paises, cantPaises);

            byte opcionPais = 0;

            try
            {
                Console.Write("\nSeleccione un país: ");
                opcionPais = byte.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Avisos.AvisoError();
                return 0;
            };

            if (opcionPais <= cantPaises && opcionPais > 0)
            {
                MostrarDatosPais(paises[opcionPais - 1]);

                try
                {
                    Console.Write("¿Continuar buscando? (Y / N): ");
                    char respuesta = char.Parse(Console.ReadLine());
                    if (respuesta == 'y' || respuesta == 'Y')
                    {
                        Console.Clear();
                        return 1;
                    }
                    else if (respuesta == 'n' || respuesta == 'N')
                    {
                        Avisos.AvisoVolverAMenuPrincipal();
                        return 0;
                    }
                    else
                    {
                        Avisos.AvisoError();
                        return 0;
                    }
                }
                catch (FormatException)
                {
                    Avisos.AvisoError();
                    return 0;
                }
            }
            else if (opcionPais == 0)
            {
                Avisos.AvisoVolverAMenuPrincipal();
                return 0;
            }
            else
            {
                Avisos.AvisoError();
                return 0;
            }
        }
        public static void MostrarListaPaises(RegistroPaises[] paises, int cantPaises)
        {
            Console.WriteLine($"0. VOLVER AL MENÚ PRINCIPAL");
            for (int i = 0; i < cantPaises; i++)
            {
                Console.WriteLine($"{i + 1}. {paises[i].Nombre}");
            }
        }
        public static void MostrarDatosPais(RegistroPaises pais)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"Nombre: {pais.Nombre}");
            Console.WriteLine($"Continente: {pais.Continente}");
            Console.WriteLine($"Código telefónico internacional: +" +
                              $"{pais.CodigoInternacional}");
            Console.WriteLine($"Población: {pais.Poblacion}");
            Console.WriteLine($"Superficie: {pais.Superficie} km2");
            Console.WriteLine("=====================================");
        }

        // ---------- Modificar ---------- //
        public static byte ModificarPaises(RegistroPaises[] paises, int cantPaises)
        {
            MostrarListaPaises(paises, cantPaises);

            byte opcionPais = 0;
            try
            {
                Console.Write("\nSeleccione un país: ");
                opcionPais = byte.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Avisos.AvisoError();
                return 0;
            };

            if (opcionPais <= cantPaises && opcionPais > 0)
            {
                ModificarDatosPais(paises, opcionPais);
                return 0;                
            }
            else if (opcionPais == 0)
            {
                Avisos.AvisoVolverAMenuPrincipal();
                return 0;
            }
            else
            {
                Avisos.AvisoError();
                return 0;
            }
        }
        public static void ModificarDatosPais(RegistroPaises[] pais, byte opcionPais)
        {
            bool salida = false;

            while (!salida)
            {
                MostrarListaModificaciones(pais[opcionPais - 1]);
                
                byte opcionModificar = 0;
                try
                {
                    Console.Write("\nSeleccione un país: ");
                    opcionModificar = byte.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Avisos.AvisoError();
                    return;
                };

                if (opcionModificar > 0 && opcionModificar < 6)
                {
                    switch (opcionModificar)
                    {
                        // NOMBRE
                        case 1:
                            ModificarNombre(pais[opcionPais - 1]);
                            break;

                        // CONTINENTE
                        case 2:
                            ModificarContinente(pais[opcionPais - 1]); 
                            break;

                        // NUMERO INTERNACIONAL
                        case 3:
                            ModificarCodigo(pais[opcionPais - 1]);
                            break;

                        // POBLACION
                        case 4:
                            ModificarPoblacion(pais[opcionPais - 1]);
                            break;

                        // SUPERFICIE
                        case 5:
                            ModificarSuperficie(pais[opcionPais - 1]);
                            break;
                    }

                    Console.Write("\n¿Continuar modificando? (Y / N): ");
                    char respuesta = char.Parse(Console.ReadLine());
                    
                    if (respuesta == 'n' || respuesta == 'N')
                    {
                        Avisos.AvisoVolverAMenuPrincipal();
                        salida = true;
                    }
                    else if (respuesta != 'y' && respuesta != 'Y')
                    {
                        Avisos.AvisoError();
                        salida = true;
                    }
                }
                else if (opcionModificar == 0)
                {
                    Avisos.AvisoVolverAMenuPrincipal();
                    salida = true;
                }
                else
                {
                    Avisos.AvisoError();
                    salida = true;
                }
            }
        }
        public static void MostrarListaModificaciones(RegistroPaises pais)
        {
            Console.Clear();
            Console.WriteLine($"Modificando: {pais.Nombre}");
            Console.WriteLine("0. Cancelar");
            Console.WriteLine($"1. Modificar nombre ({pais.Nombre})");
            Console.WriteLine($"2. Modificar contienente ({pais.Continente})");
            Console.WriteLine($"3. Modificar código telefónico internacional " +
                              $"({pais.CodigoInternacional})");
            Console.WriteLine($"4. Modificar población ({pais.Poblacion})");
            Console.WriteLine($"5. Modificar superficie ({pais.Superficie})");
        }
        public static void ModificarNombre(RegistroPaises pais)
        {
            Console.Write("Ingrese el nuevo nombre del país: ");
            string nombrePaisMod = Console.ReadLine().ToUpper();

            Console.Write($"¿Cambiar \"{pais.Nombre}\" a \"{nombrePaisMod}\"? " +
                          $"(Y / N): ");
            char respuestaMod = char.Parse(Console.ReadLine());

            if (respuestaMod == 'y' || respuestaMod == 'Y')
            {
                pais.Nombre = nombrePaisMod;
                Console.WriteLine("¡Datos cambiados con éxito!");
            }
            else if (respuestaMod == 'n' || respuestaMod == 'N')
            {
                Console.WriteLine("Cambios descartados.");
            }
            else
            {
                Console.WriteLine("Error: opción incorrecta. Cambios descartados.");
            }
        }
        public static void ModificarContinente(RegistroPaises pais)
        {
            Console.Write("Ingrese el nuevo continente: ");
            string continentePaisMod = Console.ReadLine().ToUpper();

            Console.Write($"¿Cambiar \"{pais.Continente}\" a \"{continentePaisMod}\"? " +
                          $"(Y / N): ");
            char respuestaMod = char.Parse(Console.ReadLine());

            if (respuestaMod == 'y' || respuestaMod == 'Y')
            {
                pais.Continente = continentePaisMod;
                Console.WriteLine("¡Datos cambiados con éxito!");
            }
            else if (respuestaMod == 'n' || respuestaMod == 'N')
            {
                Console.WriteLine("Cambios descartados.");                              
            }
            else
            {
                Console.WriteLine("Error: opción incorrecta. Cambios descartados");
            }
        }
        public static void ModificarCodigo(RegistroPaises pais)
        {
            Console.Write("Ingrese el nuevo código telefónico internacional: ");
            short codigoPaisMod = short.Parse(Console.ReadLine());

            Console.Write($"¿Cambiar \"{pais.CodigoInternacional}\" a \"{codigoPaisMod}\"? " +
                          $"(Y / N): ");
            char respuestaMod = char.Parse(Console.ReadLine());

            if (respuestaMod == 'y' || respuestaMod == 'Y')
            {
                pais.CodigoInternacional = codigoPaisMod;
                Console.WriteLine("¡Datos cambiados con éxito!");
            }
            else if (respuestaMod == 'n' || respuestaMod == 'N')
            {
                Console.WriteLine("Cambios descartados.");
            }
            else
            {
                Console.WriteLine("Error: opción incorrecta. Cambios descartados.");
            }
        }
        public static void ModificarPoblacion(RegistroPaises pais)
        {
            Console.Write("Ingrese el nuevo número de población: ");
            string poblacionPaisMod = Console.ReadLine().ToUpper();

            Console.Write($"¿Cambiar \"{pais.Poblacion}\" a \"{poblacionPaisMod}\"? " +
                          $"(Y / N): ");
            char respuestaMod = char.Parse(Console.ReadLine());

            if (respuestaMod == 'y' || respuestaMod == 'Y')
            {
                pais.Poblacion = poblacionPaisMod;
                Console.WriteLine("¡Datos cambiados con éxito!");
            }
            else if (respuestaMod == 'n' || respuestaMod == 'N')
            {
                Console.WriteLine("Cambios descartados.");
            }
            else
            {
                Console.WriteLine("Error: opción incorrecta. Cambios descartados");
            }
        }
        public static void ModificarSuperficie(RegistroPaises pais)
        {
            Console.Write("Ingrese el nuevo número de superficie: ");
            float superficiePaisMod = float.Parse(Console.ReadLine());

            Console.Write($"¿Cambiar \"{pais.Superficie}\" a \"{superficiePaisMod}\"? " +
                          $"(Y / N): ");
            char respuestaMod = char.Parse(Console.ReadLine());

            if (respuestaMod == 'y' || respuestaMod == 'Y')
            {
                pais.Superficie = superficiePaisMod;
                Console.WriteLine("¡Datos cambiados con éxito!");
            }
            else if (respuestaMod == 'n' || respuestaMod == 'N')
            {
                Console.WriteLine("Cambios descartados.");
            }
            else
            {
                Console.WriteLine("Error: opción incorrecta. Cambios descartados.");
            }
        }

        // ---------- Agregar ----------- //
        public static void AgregarNuevoPais(RegistroPaises[] pais, ref int cantPaises)
        {
            bool informacion = false;

            while (!informacion)
            {
                Console.Clear();

                Console.WriteLine("Agregado nuevo país en la base de datos...");
                Console.Write("Ingrese el nombre: ");
                string nuevoNombrePais = Console.ReadLine().ToUpper();
                Console.Write("Ingrese el continente: ");
                string nuevoContinentePais = Console.ReadLine().ToUpper();
                Console.Write("Ingrese el código telefónico internacional: +");
                short nuevoCodigoPais = short.Parse(Console.ReadLine());
                Console.Write("Ingrese el número de población: ");
                string nuevoPoblacionPais = Console.ReadLine();
                Console.Write("Ingrese el número de superficie: ");
                float nuevoSuperficiePais = float.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("=====================================");
                Console.WriteLine($"Nombre: {nuevoNombrePais}");
                Console.WriteLine($"Continente: {nuevoContinentePais}");
                Console.WriteLine($"Código telefónico internacional: +{nuevoCodigoPais}");
                Console.WriteLine($"Población: {nuevoPoblacionPais}");
                Console.WriteLine($"Superficie: {nuevoSuperficiePais}");
                Console.WriteLine("=====================================");

                Console.Write("\n¿La información es correcta? (Y / N): ");
                char respuestaInfo = char.Parse(Console.ReadLine());

                if (respuestaInfo == 'y' || respuestaInfo == 'Y')
                {
                    informacion = true;
                    Console.Write("¿Añadir la información a la base de datos? (Y / N): ");
                    char respuesta = char.Parse(Console.ReadLine());

                    if (respuesta == 'y' || respuesta == 'Y')
                    {
                        pais[cantPaises].Nombre = nuevoNombrePais;
                        pais[cantPaises].Continente = nuevoContinentePais;
                        pais[cantPaises].CodigoInternacional = nuevoCodigoPais;
                        pais[cantPaises].Poblacion = nuevoPoblacionPais;
                        pais[cantPaises].Superficie = nuevoSuperficiePais;
                        cantPaises++;

                        Console.WriteLine("\n¡Información añadida con éxito!");
                        Avisos.AvisoVolverAMenuPrincipal();
                    }
                    else if (respuesta == 'n' || respuesta == 'N')
                    {
                        Console.WriteLine("\nInformación descartada.");
                        Avisos.AvisoVolverAMenuPrincipal();
                    }
                    else
                    {
                        Avisos.AvisoError();
                    }
                }
                else
                {
                    Console.Write("¿Volver a intentarlo? (Y / N): ");
                    char respuesta = char.Parse(Console.ReadLine());

                    if (respuesta != 'y' && respuesta != 'Y')
                    {
                        Avisos.AvisoVolverAMenuPrincipal();
                        informacion = true;
                    }
                }
            }
        }
    }

    public class Avisos
    {
        public static void AvisoError()
        {
            Console.WriteLine("Error: opción incorrecta. Presiones 'Enter' para volver al " +
                              "menú principal.");
            Console.ReadKey();
            Console.Clear();
        }

        public static void AvisoVolverAMenuPrincipal()
        {
            Console.WriteLine("Volviendo al menú principal...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}