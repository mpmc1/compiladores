using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.Util
{
    public class UtilTexto
    {

        public static bool EsLetra(string caracter)
        {

            return Char.IsLetter(caracter, 0);

        }
        public static bool EsDigito(string caracter)
        {

            return Char.IsDigit(caracter, 0);

        }
        public static bool EsGuionBajo(string caracter)
        {

            return "_".Equals(caracter);

        }
        public static bool EsSignoPesos(string caracter)
        {

            return "$".Equals(caracter);

        }

        public static bool EsLetraDigito(string caracter)
        {

            return Char.IsLetterOrDigit(caracter, 0);

        }
        public static bool EsComa(string caracter)
        {
            return ",".Equals(caracter);
        }
        public static bool EsEOF(string caracter)
        {
            return "@EOF@".Equals(caracter);
        }
        public static bool EsFL(string caracter)
        {
            return "@FL@".Equals(caracter);
        }

    }
}
