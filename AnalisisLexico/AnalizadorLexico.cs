using Compilador_22023.cache;
using Compilador_22023.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.AnalisisLexico
{
    public class AnalizadorLexico
    {
        private string contenidoLineaActual = "";
        private int numeroLineaActual = 0;
        private int puntero = 0;
        private string caracterActual= "";
        private string lexema = "";
        private string categoria = "";
        private string estadoActual = "q0";
        private int posicionInicial = 0;
        private int posicionFinal = 0;
        private bool continuarAnalisis = false;

        public AnalizadorLexico()
        {
            CargarNuevaLinea();
        }

        private void CargarNuevaLinea()
        {
            if (!"@EOF@".Equals(contenidoLineaActual))
            {
                numeroLineaActual = numeroLineaActual +1;
                contenidoLineaActual = DataCache.ObtenerLinea(numeroLineaActual).Contenido;
                numeroLineaActual = DataCache.ObtenerLinea(numeroLineaActual).NumeroLinea;
                puntero = 1;
            }
        
        }
        private void LeerSiguienteCaracter()
        {

            if("@EOF@".Equals(contenidoLineaActual)) {
                caracterActual = "@EOF@";
            }else if(puntero > contenidoLineaActual.Length)
            {
                caracterActual = "@FL@";
            }
            else
            {
                caracterActual = contenidoLineaActual.Substring(puntero - 1, 1);
                puntero = puntero + 1;
            }

        }
        private void DevolverPuntero()
        {
            if (!"@FL@".Equals(caracterActual))
            {
                puntero = puntero - 1;
            }

        }
        private void Concatenar()
        {

            lexema = lexema + caracterActual;

        }
        private void Resetear()
        {

            estadoActual = "q0";
            lexema = "";
            categoria = "";
            posicionInicial = 0;
            posicionFinal = 0;
            continuarAnalisis = true;
        }
        public void DevolverSiguienteComponente()
        {

            Resetear();

            while (continuarAnalisis)
            {
                if ("q0".Equals(estadoActual))
                {
                    ProcesarEstado0();

                }
                else if ("q1".Equals(estadoActual))
                {
                    ProcesarEstado1();
                }
                else if ("q2".Equals(estadoActual))
                {
                    ProcesarEstado2();
                }
                else if ("q3".Equals(estadoActual))
                {
                    ProcesarEstado3();
                }
                else if ("q4".Equals(estadoActual))
                {
                    ProcesarEstado4();
                }
                else if ("q12".Equals(estadoActual))
                {
                    ProcesarEstado12();
                }
                else if ("q13".Equals(estadoActual))
                {
                    ProcesarEstado13();
                }
                else if ("q14".Equals(estadoActual))
                {
                    ProcesarEstado14();
                }
                else if ("q15".Equals(estadoActual))
                {
                    ProcesarEstado15();
                }
                else if ("q16".Equals(estadoActual))
                {
                    ProcesarEstado16();
                }
                else if ("q17".Equals(estadoActual))
                {
                    ProcesarEstado17();
                }
                else if ("q18".Equals(estadoActual))
                {
                    ProcesarEstado18();
                }
            }
            //Temporal
            while (!UtilTexto.EsEOF(caracterActual))
            {
                DevolverSiguienteComponente();
            }

        }

        private void ProcesarEstado0()
        {
            DevorarEspaciosEnBlanco();

            if (UtilTexto.EsLetra(caracterActual) || UtilTexto.EsSignoPesos(caracterActual) || UtilTexto.EsGuionBajo(caracterActual))
            {
                estadoActual = "q4";
            }
            else if((UtilTexto.EsEOF(caracterActual)))
            {
                estadoActual = "q12";
            }
            else if ((UtilTexto.EsFL(caracterActual)))
            {
                estadoActual = "q13";
            }
            else if ((UtilTexto.EsDigito(caracterActual)))
            {
                estadoActual = "q1";
            }
            else
            {
                estadoActual = "q18";
            }
        }
        private void ProcesarEstado1()
        {
            Concatenar();
            LeerSiguienteCaracter();

            if (UtilTexto.EsDigito(caracterActual))
            {
                estadoActual = "q1";
            }else if (UtilTexto.EsComa(caracterActual))
            {
                estadoActual = "q2";
            }
            else
            {
                estadoActual = "q14";
            }

        }

        private void ProcesarEstado2()
        {
            Concatenar();
            LeerSiguienteCaracter();

            if (UtilTexto.EsLetraDigito(caracterActual))
            {
                estadoActual = "q3";
            }
            else
            {
                estadoActual = "q17";
            }
        }
        private void ProcesarEstado3()
        {
            Concatenar();
            LeerSiguienteCaracter();

            if (UtilTexto.EsLetraDigito(caracterActual))
            {
                estadoActual = "q3";
            }
            else
            {
                estadoActual = "q15";
            }
        }
        private void ProcesarEstado4()
        {
            Concatenar();
            LeerSiguienteCaracter();
            if (UtilTexto.EsLetra(caracterActual) || UtilTexto.EsLetraDigito(caracterActual) || UtilTexto.EsSignoPesos(caracterActual) || UtilTexto.EsGuionBajo(caracterActual))
            {
                estadoActual = "q4";
            }
            else
            {
                estadoActual = "q16";
            }

        }
        private void ProcesarEstado12()
        {
            categoria = "Fin De Archivo";
            FormarComponenteLexico();
            continuarAnalisis = false;

        }
        private void ProcesarEstado13()
        {
            CargarNuevaLinea();
            Resetear();
        }
        private void ProcesarEstado14()
        {
            DevolverPuntero();
            categoria = "Número Entero";
            FormarComponenteLexico();
            continuarAnalisis = false;

        }
        private void ProcesarEstado15()
        {
            DevolverPuntero();
            categoria = "Decimal";
            FormarComponenteLexico();
            continuarAnalisis = false;

        }

        private void ProcesarEstado16()
        {
            DevolverPuntero();
            categoria = "Identificador";
            FormarComponenteLexico();
            continuarAnalisis = false;
        }
        private void ProcesarEstado17()
        {
            DevolverPuntero();
            categoria = "Error Decimal No Válido";
            FormarComponenteLexico();
            continuarAnalisis = false;
        }
        private void ProcesarEstado18()
        {
            DevolverPuntero();
            categoria = "Error Símbolo No Válido";
            FormarComponenteLexico();
            throw new Exception("Símbolo no reconocido dentro del lenguaje");
        }

        private void FormarComponenteLexico()
        {
            posicionInicial = puntero - lexema.Length;
            posicionFinal = puntero - 1;

            Console.WriteLine("Categoría: " + categoria);
            Console.WriteLine("Lexema: " + lexema);
            Console.WriteLine("Línea: " + numeroLineaActual);
            Console.WriteLine("Pos. Inicial: " + posicionInicial);
            Console.WriteLine("Pos. Final: " + posicionFinal);
        }

        private void DevorarEspaciosEnBlanco()
        {
            LeerSiguienteCaracter();
            while ("".Equals(caracterActual.Trim()) || "\t".Equals(estadoActual))
            {
                LeerSiguienteCaracter();
            }
        }
    }
}
