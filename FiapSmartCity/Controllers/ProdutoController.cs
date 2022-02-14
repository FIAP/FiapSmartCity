using FiapSmartCity.Models;
using FiapSmartCity.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FiapSmartCity.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly TipoProdutoRepository tipoProdutoRepository;
        private readonly ProdutoRepository produtoRepository;

        public ProdutoController()
        {
            tipoProdutoRepository = new TipoProdutoRepository();
            produtoRepository = new ProdutoRepository();
        }

        public IActionResult Index()
        {
            return View( produtoRepository.Listar() );
        }


        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public ActionResult Cadastrar()
        {
            var tipos = tipoProdutoRepository.ListarOrdenadoPorNome();
            ViewBag.Tipos = new SelectList(tipos, "IdTipo", "DescricaoTipo");
            return View(new Produto());
        }

        // Anotação de uso do Verb HTTP Post
        [HttpPost]
        public ActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produtoRepository.Inserir(produto);

                @TempData["mensagem"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index", "Produto");

            }
            else
            {
                var tipos = tipoProdutoRepository.ListarOrdenadoPorNome();
                ViewBag.Tipos = new SelectList(tipos, "IdTipo", "DescricaoTipo");
                return View(produto);
            }
        }


        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var produto = produtoRepository.Consultar(id);

            var tipos = tipoProdutoRepository.ListarOrdenadoPorNome();
            ViewBag.Tipos = new SelectList(tipos, "IdTipo", "DescricaoTipo");

            return View(produto);
        }

        // Anotação de uso do Verb HTTP Post
        [HttpPost]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produtoRepository.Alterar(produto);

                @TempData["mensagem"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index", "Produto");

            }
            else
            {
                var tipos = tipoProdutoRepository.ListarOrdenadoPorNome();
                ViewBag.Tipos = new SelectList(tipos, "IdTipo", "DescricaoTipo");
                return View(produto);
            }
        }


        [HttpGet]
        public ActionResult Excluir(int Id)
        {
            produtoRepository.Excluir(Id);

            @TempData["mensagem"] = "Produto removido com sucesso!";

            return RedirectToAction("Index", "Produto");
        }

    }
}
