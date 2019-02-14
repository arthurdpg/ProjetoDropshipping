namespace Projeto.Data
{
    public class CrudData<T> where T : class
    {
        public CrudData(ProjetoContext context)
        {
            Context = context;
        }

        protected ProjetoContext Context { get; }

        public virtual T Obter(int codigo)
        {
            return Context.Find<T>(codigo);
        }

        public virtual void Excluir(int codigo)
        {
            Context.Remove(Obter(codigo));
            Context.SaveChanges();
        }

        public virtual void Salvar(T registro)
        {
            Context.Update(registro);
            Context.SaveChanges();
        }
    }
}
