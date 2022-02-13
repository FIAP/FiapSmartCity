using FiapSmartCity.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;

namespace FiapSmartCity.Repository
{
    public class TipoProdutoRepository
    {

        private readonly string connectionString;

        public TipoProdutoRepository()
        {
            connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("FiapSmartCityConnection");
        }


        public IList<TipoProduto> Listar()
        {
            IList<TipoProduto> lista = new List<TipoProduto>();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                String query =
                    "SELECT IDTIPO, DESCRICAOTIPO, COMERCIALIZADO FROM TIPOPRODUTO  ";

                OracleCommand command = new OracleCommand(query, connection);
                connection.Open();
                OracleDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    TipoProduto tipoProd = new TipoProduto();
                    tipoProd.IdTipo = Convert.ToInt32(dataReader["IDTIPO"]);
                    tipoProd.DescricaoTipo = dataReader["DESCRICAOTIPO"].ToString();
                    tipoProd.Comercializado = Convert.ToBoolean((dataReader["COMERCIALIZADO"] as string) == "1");


                    // Adiciona o modelo da lista
                    lista.Add(tipoProd);
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return lista;
        }

        public TipoProduto Consultar(int id)
        {

            TipoProduto tipoProd = new TipoProduto();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                String query =
                    "SELECT IDTIPO, DESCRICAOTIPO, COMERCIALIZADO FROM TIPOPRODUTO WHERE IDTIPO = :IDTIPO ";

                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add("IDTIPO", id);
                connection.Open();

                OracleDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    tipoProd.IdTipo = Convert.ToInt32(dataReader["IDTIPO"]);
                    tipoProd.DescricaoTipo = dataReader["DESCRICAOTIPO"].ToString();
                    tipoProd.Comercializado = Convert.ToBoolean((dataReader["COMERCIALIZADO"] as string) == "1");
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return tipoProd;
        }

        public void Inserir(TipoProduto tipoProduto)
        {
            
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                String query =
                    "INSERT INTO TIPOPRODUTO ( DESCRICAOTIPO, COMERCIALIZADO ) VALUES ( :descr, :comerc ) ";

                OracleCommand command = new OracleCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("descr", tipoProduto.DescricaoTipo);
                command.Parameters.Add("comerc", Convert.ToInt32(tipoProduto.Comercializado));

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Alterar(TipoProduto tipoProduto)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                String query =
                    "UPDATE TIPOPRODUTO SET DESCRICAOTIPO = :descr , COMERCIALIZADO = :comerc WHERE IDTIPO = :id  ";

                OracleCommand command = new OracleCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("descr", tipoProduto.DescricaoTipo);
                command.Parameters.Add("comerc", Convert.ToInt32(tipoProduto.Comercializado));
                command.Parameters.Add("id", tipoProduto.IdTipo);

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Excluir(int id)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                String query =
                    "DELETE TIPOPRODUTO WHERE IDTIPO = :id  ";

                OracleCommand command = new OracleCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("id", id);

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

    }

}
