using Compilador_22023.AnalisisLexico;
using Compilador_22023.AnalisisSintactico;
using Compilador_22023.cache;
using Compilador_22023.GestorErrores;
using Compilador_22023.TablaComponentes;
using System.Windows;

namespace Compilador_22023
{
    class Program
    {
        static void Main(string[] args)
        {
            DataCache.AgregarLinea("");
            //DataCache.AgregarLinea("Segunda Línea");
            DataCache.AgregarLinea("5 +5 +3-4/20*100");
            //DataCache.AgregarLinea("5,a");

            RetoAnalizadorSintactico anasin = new RetoAnalizadorSintactico();
            try
            {
                string respuesta = anasin.Analizar();
                Console.WriteLine(respuesta);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ImprimirComponentes(TipoComponente.SIMBOLO);
            ImprimirComponentes(TipoComponente.LITERAL);
            ImprimirComponentes(TipoComponente.DUMMY);
            ImprimirComponentes(TipoComponente.PALABRA_RESERVADA);
            ImprimirErrores(TipoError.LEXICO);
            ImprimirErrores(TipoError.SEMANTICO);
            ImprimirErrores(TipoError.SINTACTICO);
            ImprimirErrores(TipoError.GENERADOR_CODIGO_INTERMEDIO);
            ImprimirErrores(TipoError.OPTIMIZACION);
            ImprimirErrores(TipoError.GENERALDOR_CODIGO_FINAL);




            Thread.Sleep(20000);
        }
        private static void ImprimirComponentes(TipoComponente tipo)
        {
            Console.WriteLine("+++++++++++++++ Inicio Componentes" + tipo.ToString() + " +++++++++++++++");
            List<ComponenteLexico> componentes = TablaMaestra.ObtenerTablaMaestra().ObtenerTodosSimbolos(tipo);
            foreach (ComponenteLexico componente in componentes)
            {
                Console.WriteLine(componente.ToString());
            }
            Console.WriteLine("+++++++++++++++ Fin Componentes    " + tipo.ToString() + "+++++++++++++++\r\n");
        }
        private static void ImprimirErrores(TipoError tipo)
        {
            Console.WriteLine("+++++++++++++++ Inicio Errores" + tipo.ToString() + " +++++++++++++++");
            List<Error> errores = ManejadorErrores.ObtenerManejadorDeErrores().ObtenerErrores(tipo);
            foreach (Error error in errores)
            {
                Console.WriteLine(error.ToString());
            }
            Console.WriteLine("+++++++++++++++ Fin Errores    " + tipo.ToString() + "+++++++++++++++\r\n");
        }
    }
}