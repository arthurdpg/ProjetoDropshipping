using Projeto.Configuration;

namespace Projeto
{
    public static class ImagemHelper
    {
        public static string GetImagemProduto(FilesConfig config, int codigoProduto)
        {
            return string.Format(@"{0}{1}\produto.jpg", config.ImagensProduto, codigoProduto);
        }
    }
}
