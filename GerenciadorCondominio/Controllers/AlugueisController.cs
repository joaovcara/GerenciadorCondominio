using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class AlugueisController : Controller
    {

        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPagamentoRepositorio _pagamentoRepositorio;
        private readonly IMesRepositorio _mesRepositorio;

        public AlugueisController(IAluguelRepositorio aluguelRepositorio, IUsuarioRepositorio usuarioRepositorio, IPagamentoRepositorio pagamentoRepositorio, IMesRepositorio mesRepositorio)
        {
            _aluguelRepositorio = aluguelRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _pagamentoRepositorio = pagamentoRepositorio;
            _mesRepositorio = mesRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _aluguelRepositorio.SelectAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["MesId"] = new SelectList(await _mesRepositorio.SelectAll(), "MesId", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AluguelId,Valor,MesId,Ano")] Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                if (! _aluguelRepositorio.AluguelExists(aluguel.MesId, aluguel.Ano))
                {
                    await _aluguelRepositorio.Insert(aluguel);
                    IEnumerable<Usuario> usuarios = await _usuarioRepositorio.SelectAll();

                    Pagamento pagamento;

                    List<Pagamento> pagamentos = new List<Pagamento>();

                    foreach (var item in usuarios)
                    {
                        pagamento = new Pagamento
                        {
                            AluguelId = aluguel.AluguelId,
                            UsuarioId = item.Id,
                            DataPagamento = null,
                            Status = StatusPagamento.Pendente
                        };

                        pagamentos.Add(pagamento);
                    }

                    await _pagamentoRepositorio.Insert(pagamentos);
                    TempData["NovoRegistro"] = "Aluguel adicionado";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Aluguel já cadastrado");
                    ViewData["MesId"] = new SelectList(await _mesRepositorio.SelectAll(), "MesId", "Descricao", aluguel.MesId);
                    return View(aluguel);
                }
            }

            ViewData["MesId"] = new SelectList(await _mesRepositorio.SelectAll(), "MesId", "Descricao", aluguel.MesId);
            return View(aluguel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Aluguel aluguel = await _aluguelRepositorio.SelectById(id);
            if(aluguel == null)
            {
                return NotFound();
            }

            ViewData["MesId"] = new SelectList(await _mesRepositorio.SelectAll(), "MesId", "Descricao", aluguel.MesId);
            return View(aluguel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AluguelId,Valor,MesId,Ano")] Aluguel aluguel)
        {
            if(id != aluguel.AluguelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _aluguelRepositorio.Update(aluguel);

                TempData["Atualizacao"] = "Registro atualizado";

                return RedirectToAction(nameof(Index));
            }

            ViewData["MesId"] = new SelectList(await _mesRepositorio.SelectAll(), "MesId", "Descricao", aluguel.MesId);
            return View(aluguel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _aluguelRepositorio.Delete(id);

            TempData["Exclusao"] = "Registro excluido";

            return Json("Registro excluido");
        }
    }
}
