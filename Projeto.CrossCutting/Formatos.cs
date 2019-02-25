using System.Globalization;

namespace Projeto.CrossCutting
{
    public static class Formatos
    {
        private static CultureInfo _cultureInfoPtBr;

        public static CultureInfo CulturePrBr
        {
            get
            {
                if (_cultureInfoPtBr == null)
                    _cultureInfoPtBr = CultureInfo.GetCultureInfo("pt-BR");

                return _cultureInfoPtBr;
            }
        }

        public static string FormatarValor(string valor, string formato)
        {
            return string.Format(CulturePrBr, formato, valor);
        }

        public const string FormatoDataPtBr = "dd/MM/yyyy";
        public const string FormatoMoeda = "R$ {0:C}";
    }
}
