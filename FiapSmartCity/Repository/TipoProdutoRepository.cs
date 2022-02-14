using FiapSmartCity.Models;
using FiapSmartCity.Repository.Context;
using System.Collections.Generic;
using System.Linq;


namespace FiapSmartCity.Repository
{
    public class TipoProdutoRepository
    {
        // Propriedade que terá a instância do DataBaseContext
        private readonly DataBaseContext context;

        public TipoProdutoRepository()
        {
            // Criando um instância da classe de contexto do EntityFramework
            context = new DataBaseContext();
        }


        public IList<TipoProduto> Listar()
        {
            return context.TipoProduto.ToList();
        }


        public TipoProduto Consultar(int id)
        {
            return context.TipoProduto.Find(id);
        }


        public void Inserir(TipoProduto tipoProduto)
        {
            context.TipoProduto.Add(tipoProduto);
            context.SaveChanges();
        }

        public void Alterar(TipoProduto tipoProduto)
        {
            // Informa o contexto que um objeto foi alterado
            context.TipoProduto.Update(tipoProduto);

            // Salva as alterações
            context.SaveChanges();
        }

        public void Excluir(int id)
        {
            // Criar um tipo produto apenas com o Id
            var tipoProduto = new TipoProduto()
            {
                IdTipo = id
            };

            context.TipoProduto.Remove(tipoProduto);
            context.SaveChanges();
        }




        public IList<TipoProduto> ListarOrdenadoPorNome()
        {
            var lista =
                context.TipoProduto.OrderBy(t => t.DescricaoTipo).ToList<TipoProduto>();

            return lista;
        }


        public IList<TipoProduto> ListarOrdenadoPorNomeDescendente()
        {
            var lista =
                context.TipoProduto.OrderByDescending(t => t.DescricaoTipo).ToList<TipoProduto>();

            return lista;
        }


        public IList<TipoProduto> ListarTiposComercializados()
        {
            // Filtro com Where
            var lista =
                context.TipoProduto.Where(t => t.Comercializado == '0')
                        .OrderByDescending(t => t.DescricaoTipo).ToList<TipoProduto>();

            return lista;
        }


        public IList<TipoProduto> ListarTiposComercializadosCritedio(char selecao)
        {
            // Filtro com Where
            var lista =
                context.TipoProduto.Where(t => t.Comercializado == selecao)
                        .OrderByDescending(t => t.DescricaoTipo).ToList<TipoProduto>();

            return lista;
        }



        public IList<TipoProduto> ListarTiposComercializados(char selecao)
        {
            // Filtro com Where
            var lista =
                context.TipoProduto.Where(t => t.Comercializado == selecao && t.IdTipo > 2)
                        .OrderByDescending(t => t.DescricaoTipo).ToList<TipoProduto>();

            return lista;
        }



        public TipoProduto ConsultaPorDescricao(string descricao)
        {
            // Retorno único
            TipoProduto tipo =
                context.TipoProduto.Where(t => t.DescricaoTipo == descricao)
                    .FirstOrDefault<TipoProduto>();

            return tipo;
        }



        public IList<TipoProduto> ListarTiposParteDescricao(string parteDescricao)
        {
            // Filtro com Where e Contains
            var lista =
                context.TipoProduto.Where(t => t.DescricaoTipo.Contains(parteDescricao))
                        .ToList<TipoProduto>();

            return lista;
        }



    }
}
