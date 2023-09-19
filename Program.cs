using Compilador_22023.AnalisisLexico;
using Compilador_22023.cache;

namespace Compilador_22023
{
    class Program
    {
        static void Main(string[] args)
        {
            DataCache.AgregarLinea("");
            DataCache.AgregarLinea("Segunda Línea");
            DataCache.AgregarLinea("5 + 3 + 2 + 1");

            AnalizadorLexico analex = new AnalizadorLexico();
            ComponenteLexico componente = analex.DevolverSiguienteComponente();

            do
            {
                Console.WriteLine(componente.ToString());
                componente = analex.DevolverSiguienteComponente();

            } while (!CategoriaGramatical.FIN_DE_ARCHIVO.Equals(componente.Categoria));
            Console.WriteLine(componente.ToString());

        }
    }
}