using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBhs.Data;
using WebApiBhs.Domain;

namespace WebApiBhs.Services
{
    public interface IProductService
    {
        Task<bool> AdicionarProduto(Produto produto);
        Task<List<Produto>> ListarProdutos();
        Task<bool> ExcluirProduto(int id);
        Task<bool> EditarProduto(int id, Produto produtoNovo);
    }
    public class ProductService : IProductService
    {
        private readonly Contexto _context;

        public ProductService(Contexto context)
        {
            _context = context;
        }
        public async Task<bool> AdicionarProduto(Produto produto)
        {
            //Verifica se o objeto veio null ou o nome dele veio sem preencher

            if(produto == null || String.IsNullOrEmpty(produto.nome) )
            {
                throw new Exception("Erro ao cadastrar Produto!");
            }

            //Salva no banco de dados o objeto 
            _context.Add(produto);
            await _context.SaveChangesAsync();

            return true;    
        }

        public async Task<bool> EditarProduto(int id, Produto produtoNovo)
        {
            bool produtoFoiEncontrado = VerificarSeProdutoExisteNaTabela(id);

            var produtoAntigo = _context.Produtos.FirstOrDefault(p => p.id == id);

            if (produtoAntigo != null)
            {
                produtoAntigo.nome = produtoNovo.nome;
                produtoAntigo.status = produtoNovo.status;

                await _context.SaveChangesAsync();

                return true;
            }

            throw new Exception("Erro ao editar Produto!");
        }

        public async Task<bool> ExcluirProduto(int id)
        {
            VerificarSeProdutoExisteNaTabela(id);

            var produto = await _context.Produtos.FindAsync(id);

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            List<Produto> listaDeProdutos = await _context.Produtos.ToListAsync();

            return listaDeProdutos;
        }
        private bool VerificarSeProdutoExisteNaTabela(int id)
        {
            //Percorre a lista de produtos na tabela e verifica se há algum produto com esse ID
            var produtoExiste = _context.Produtos.Any(e => e.id == id);

            if (produtoExiste == false)
            {
                throw new Exception("Produto não encontrado!");
            }

            return produtoExiste;
        }
    }

}
