using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiBhs.Domain;
using WebApiBhs.Services;

namespace WebApiBhs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : Controller
    {
        private readonly IProductService productService;

        public ProdutosController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var listaDeProdutos = await productService.ListarProdutos();
            return View(listaDeProdutos.ToList());
        }


        [HttpPost("Criar")]
        public async Task<IActionResult> Create([FromBody] Produto produto)
        {
            await productService.AdicionarProduto(produto);
            return Ok();
        }

        [HttpGet("Listar")]
        public async Task<List<Produto>> ListarProdutosAsync()
        {
            var listaDeProdutos = await productService.ListarProdutos();

            return listaDeProdutos;
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> EditarProduto([FromQuery] int id, [FromBody] Produto produtoNovo)
        {
            var retorno = await productService.EditarProduto(id, produtoNovo);

            return Ok(retorno);
        }


        [HttpDelete("Excluir")]
        public async Task<IActionResult> Excluir([FromQuery] int id)
        {
            var retorno = await productService.ExcluirProduto(id);

            return Ok(retorno);
        }

    }
}
