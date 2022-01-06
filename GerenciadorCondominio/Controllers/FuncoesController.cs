using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL;
using GerenciadorCondominio.DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class FuncoesController : Controller
    {
        private readonly IFuncaoRepositorio _funcaoRepositorio;

        public FuncoesController(Contexto contexto, IFuncaoRepositorio funcaoRepositorio)
        {
            _funcaoRepositorio = funcaoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _funcaoRepositorio.SelectAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] Funcao funcao)
        {
            if (ModelState.IsValid)
            {
                await _funcaoRepositorio.AdicionarFuncao(funcao);
                return RedirectToAction(nameof(Index));
            }

            TempData["NovoRegistro"] = "Função cadastrada";

            return View(funcao);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Funcao funcao = await _funcaoRepositorio.SelectById(id);

            if (funcao == null)
            {
                return NotFound();
            }

            return View(funcao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] Funcao funcao)
        {
            if (Convert.ToString(id) != funcao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _funcaoRepositorio.Update(funcao);
                TempData["Atualizacao"] = "Funcao Atualizada";

                return RedirectToAction(nameof(Index));
            }

            return View(funcao);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            await _funcaoRepositorio.Delete(id);
            TempData["Exclusao"] = "Função excluida";

            return Json("Função excluida");
        }
    }
}
