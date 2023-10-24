using Compilador_22023.AnalisisLexico;
using Compilador_22023.GestorErrores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.AnalisisSintactico
{
    internal class AnalizadorSintactico
    {

        private AnalizadorLexico Analex = new AnalizadorLexico();
        private ComponenteLexico Componente;
        private string falla = "";
        private string causa = "";
        private string solucion = "";

        public string Analizar()
        {
            string resultado = "";
            DevolverSiguienteComponenteLexico();
            Expresion();

            if (ManejadorErrores.ObtenerManejadorDeErrores().HayErroresAnalisis())
            {
                resultado = "El proceso de compilación terminó con errores.\r\n";
            }
            else if (!CategoriaGramatical.FIN_DE_ARCHIVO.Equals(Componente.Categoria))
            {
                resultado = "Aunque el programa no tiene errores, faltaron componentes por evaluar.\r\n";
            }
            else
            {
                resultado = "El programa se encuentra bien escrito.\r\n";
            }

            return resultado;
        }

        private void DevolverSiguienteComponenteLexico()
        {
            Componente = Analex.DevolverSiguienteComponente();
        }
        private void Expresion()
        {
            Termino();
            ExpresionPrima();
        }
        private void ExpresionPrima()
        {
            if (EsCategoriaValida(CategoriaGramatical.SUMA))
            {
                DevolverSiguienteComponenteLexico();
                Expresion();
            }
            else if (EsCategoriaValida(CategoriaGramatical.RESTA))
            {
                DevolverSiguienteComponenteLexico();
                Expresion();
            }
        }
        private void Termino()
        {
            Factor();
            TerminoPrima();

        }
        private void TerminoPrima()
        {
            if(EsCategoriaValida(CategoriaGramatical.MULTIPLICACION))
            {
                DevolverSiguienteComponenteLexico();
                Termino();
            }
            else if(EsCategoriaValida(CategoriaGramatical.DIVISION))
            {
                DevolverSiguienteComponenteLexico();
                Termino();
            }

        }
        private void Factor()
        {
            if(EsCategoriaValida(CategoriaGramatical.NUMERO_ENTERO))
            {
                DevolverSiguienteComponenteLexico();
            }
            else if(EsCategoriaValida(CategoriaGramatical.NUMERO_DECIMAL))
            {
                DevolverSiguienteComponenteLexico();
            }
            else if (EsCategoriaValida(CategoriaGramatical.PARENTESIS_ABRE))
            {
                DevolverSiguienteComponenteLexico();
                Expresion();
                if (EsCategoriaValida(CategoriaGramatical.PARENTESIS_CIERRA))
                {
                    DevolverSiguienteComponenteLexico();
                }
                else
                {
                    falla = "Categoria gramatical inválida";
                    causa = "Se esperaba PARENTESIS_CIERRA y se recibió " + Componente.Categoria;
                    solucion = "Asegurese que en la posición esperada se encuentre un PARENTESIS_CIERRA";
                    ReportarErrorSintacticoStopper();
                }
            }
            else
            {
                falla = "Categoria gramatical inválida";
                causa = "Se esperaba NUMERO_ENTERO, NUMERO_DECIMAL o PARENTESIS_ABRE y se recibió " + Componente.Categoria;
                solucion = "Asegurese que en la posición esperada se encuentre un NUMERO_ENTERO, NUMERO_DECIMAL o PARENTESIS_ABRE";
                ReportarErrorSintacticoStopper();
            }
        }
        private bool EsCategoriaValida(CategoriaGramatical categoria)
        {
            return categoria.Equals(Componente.Categoria);
        }
        private void ReportarErrorSintacticoStopper()
        {
            Error error = Error.CrearErrorSintacticoStopper(Componente.NumeroLinea, Componente.PosicionInicial, 
                Componente.Lexema, falla, causa, solucion);
            ManejadorErrores.ObtenerManejadorDeErrores().ReportarError(error);
        }
    }
}
