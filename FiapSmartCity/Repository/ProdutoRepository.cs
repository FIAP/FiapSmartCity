using FiapSmartCity.Models;
using FiapSmartCity.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiapSmartCity.Repository
{
    public class ProdutoRepository
    {

        // Propriedade que terá a instância do DataBaseContext
        private readonly DataBaseContext context;

        public ProdutoRepository()
        {
            // Criando um instância da classe de contexto do EntityFramework
            context = new DataBaseContext();
        }


        public IList<Produto> Listar()
        {
            return context.Produto.Include(t => t.Tipo).ToList();
        }


        public Produto Consultar(int id)
        {
            var prod = context.Produto
                .Include(t => t.Tipo)
                .FirstOrDefault(p => p.IdProduto == id);
            return prod;
        }

        public IList<Produto> ConsultarProdutosPorTipo(int idTipo)
        {
            // Consulta a lista de produtos de um determinado tipo.
            var tipoProduto =
                context.TipoProduto
                    .Include(t => t.Produtos)
                    .FirstOrDefault(t => t.IdTipo == idTipo);

            return tipoProduto.Produtos;
        }


        public void Inserir(Produto produto)
        {
            context.Produto.Add(produto);
            context.SaveChanges();
        }

        public void Alterar(Produto produto)
        {
            // Informa o contexto que um objeto foi alterado
            context.Produto.Update(produto);

            // Salva as alterações
            context.SaveChanges();
        }

        public void Excluir(int id)
        {
            // Criar um tipo produto apenas com o Id
            var prod = new Produto()
            {
                IdProduto = id
            };

            context.Produto.Remove(prod);
            context.SaveChanges();
        }

    }
}
