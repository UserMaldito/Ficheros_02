using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficheros_02
{   /*Generar un programa para el tratamiento de números existentes en un fichero que contenga las siguientes opciones en un menú:
       1. Crear el fichero: si el fichero no existe, lo crea, y si existe, deberá preguntar si lo queremos sobreescribir.
       2. Introducir valores numéricos en el fichero: se pedirán por teclado y se incluirán al final del fichero, uno por línea.
       3. Calcular cuál es el máximo de los valores almacenados en el fichero.
       4. Calcular cuál es el mínimo de los valores almacenados en el fichero.
       5. Calcular cuál es la media de los valores almacenados en el fichero.*/
	class Program
	{
		static void Main(string[] args)
		{
			bool salida = false;
			do
			{
				Mostrar.Menu();
				salida = Mostrar.Opciones();
			} while (!salida);

			Despedida();
		}
		static void Despedida()
		{
			Console.WriteLine("\n\nPulse una tecla para finalizar...");
			Console.ReadKey();
		}
	}
	class Fichero
	{
		public const string fichi = "fichero.txt";
		public static void Crear(string nombreFichero)
		{
			Console.Clear();
			bool crear = true;

			bool encontrado = File.Exists(nombreFichero);
			if (encontrado)
			{
				Console.WriteLine("Fichero Existente. ¿Lo sobreescribimos? (\"Y\" para confirmar, otra tecla para abortar): ");
				ConsoleKeyInfo tecla = Console.ReadKey(true);
				if (char.ToUpper(tecla.KeyChar) != 'Y')
				{
					crear = false;
					Continuar("");
				}
			}

			if (crear)
			{
				Console.Clear();
				try
				{
					StreamWriter SWrite = File.CreateText(nombreFichero);

					SWrite.Close();

					Console.WriteLine("\t¡Creación Exitosa!");
					Continuar("");
				}
				catch (Exception ex)
				{
					Console.WriteLine("\tCreación Fallida");
					crear = false;
					Continuar(ex.Message);
				}
			}
		}
		static int PedirNumeroEntero(string mensaje)
		{
			int numero = 0;

			bool continuarBucle = true;
			do
			{
				Console.WriteLine(mensaje);
				if (Int32.TryParse(Console.ReadLine(), out numero))
				{
					continuarBucle = false;
				}
				else
				{
					Continuar("Caracter no válido. Debe ser un NÚMERO ENTERO");
				}
			} while (continuarBucle);

			Console.Clear();

			return numero;
		}
		public static void IngresarDatosFichero()
		{
			Console.Clear();
			int numero = PedirNumeroEntero("\nIngrese un Número Entero: ");

			try
			{
				StreamWriter escribir = new StreamWriter(fichi, true);

				escribir.WriteLine(numero);

				escribir.Close();
				Console.WriteLine("\n\t¡Introducido Correctamente!");
				Continuar("");
			}
			catch (Exception exc)
			{
				Continuar(exc.Message);
			}
		}
		public static List<int> ExtraerDatosFichero()
		{
			Console.Clear();
			List<int> numeros = new List<int>();
			bool errorLectura = false;

			try
			{
				StreamReader leyendo = new StreamReader(fichi);

				int contadorLectura = 0;
				while (contadorLectura < fichi.Length)
				{
					if ((!leyendo.EndOfStream) || (!errorLectura))
					{
						string lectura = leyendo.ReadLine().Trim();

						if (Int32.TryParse(lectura, out int numeroValido))
						{
							numeros.Add(numeroValido);
						}
						else
						{
							Continuar("Se ha Detectado un Caracter Prohibido");
							errorLectura = true;
						}
					}
					contadorLectura++;
				}

				leyendo.Close();
			}
			catch (Exception e)
			{
				Continuar(e.Message);
				errorLectura = true;
			}

			if (errorLectura)
			{
				numeros = null;
			}
			else
			{
				Console.WriteLine("\n\t¡Lectura Exitosa!");
			}
			Continuar("");
			return numeros;
		}
		public static int CalcularMaximo(List<int> datos)
		{
			int maximo = 0;

			if (datos.Count > 0)
			{
				maximo = datos.Max();
			}

			return maximo;
		}
		public static int CalcularMinimo(List<int> datos)
		{
			int minimo = 0;

			if (datos.Count > 0)
			{
				minimo = datos.Min();
			}

			return minimo;
		}
		public static double CalcularMedia(List<int> datos)
		{
			double media = 0;

			if (datos.Count > 0)
			{
				media = datos.Average();
			}

			return media;
		}


		public static void Continuar(string mensajeError)
		{
			if (mensajeError != "")
			{
				Console.WriteLine("\nERROR: " + mensajeError + "...");
			}
			Console.WriteLine("\n\nPulse una tecla para continuar...");
			Console.ReadKey(true);
			Console.Clear();
		}
	}

	class Mostrar
	{
		static void PantallaNumero(string cadena, string numero)
		{
			Console.WriteLine(cadena + " es --->" + numero + "!");
		}
		public static void Menu()
		{
			Console.WriteLine("***************************************");
			Console.WriteLine("\t\t¿Qué desea hacer?");
			Console.WriteLine("***************************************\n");
			Console.WriteLine("\t1. Crear Fichero");
			Console.WriteLine("\t2. Ingresar Datos");
			Console.WriteLine("\t3. Calcular Máximo Números");
			Console.WriteLine("\t4. Calcular Mínimo Números");
			Console.WriteLine("\t5. Calcular Media Números");
			Console.WriteLine("\t6. Salir del Programa");
			Console.WriteLine("\n***************************************\n\n");
		}
		public static bool Opciones()
		{
			bool salida = false;

			ConsoleKeyInfo tecla = Console.ReadKey(true);
			switch (tecla.KeyChar)
			{
				case '1': Fichero.Crear(Fichero.fichi); break;

				case '2': Fichero.IngresarDatosFichero(); break;

				case '3':
					List<int> datosMaximo = Fichero.ExtraerDatosFichero();
					if (datosMaximo != null)
					{
						int numMaximo = Fichero.CalcularMaximo(datosMaximo);
						PantallaNumero("El Número Máximo", numMaximo.ToString());
						Fichero.Continuar("");
					}
					break;

				case '4':
					List<int> datosMinimo = Fichero.ExtraerDatosFichero();
					if (datosMinimo != null)
					{
						int numMinimo = Fichero.CalcularMinimo(datosMinimo);
						PantallaNumero("El Número Mínimo", numMinimo.ToString());
						Fichero.Continuar("");
					}
					break;

				case '5':
					List<int> datosMedia = Fichero.ExtraerDatosFichero();
					if (datosMedia != null)
					{
						double numMedia = Fichero.CalcularMedia(datosMedia);
						PantallaNumero("La Media de Números", numMedia.ToString());
						Fichero.Continuar("");
					}
					break;

				case '6': salida = true; break;

				default: Fichero.Continuar("Opción No Válida"); break;
			}

			return salida;
		}

	}
}
