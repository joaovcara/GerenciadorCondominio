using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using GerenciadorCondominio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class ServicosController : Controller
    {
        private readonly IServicoRepositorio _servicoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IServicoImovelRepositorio _servicoImovelRepositorio;
        private readonly IMovimentoRepositorio _movimentoRepositorio;

        public ServicosController(IServicoRepositorio servicoRepositorio, IUsuarioRepositorio usuarioRepositorio,
            IServicoImovelRepositorio servicoImovelRepositorio, IMovimentoRepositorio movimentoRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _servicoImovelRepositorio = servicoImovelRepositorio;
            _movimentoRepositorio = movimentoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.SelectUserName(User);

            if (await _usuarioRepositorio.VerificarUsuarioCadastradoFuncao(usuario, "Locatário"))
            {
                return View(await _servicoRepositorio.SelectServicoUsuarioId(usuario.Id));
            }

            return View(await _servicoRepositorio.SelectAll());
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Usuario usuario = await _usuarioRepositorio.SelectUserName(User);
            Servico servico = new Servico
            {
                UsuarioId = usuario.Id
            };

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicoId,Descricao,Valor,Status,UsuarioId")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.Status = StatusServico.Pendente;

                await _servicoRepositorio.Insert(servico);

                TempData["NovoRegistro"] = "Servico adicionado";

                return RedirectToAction(nameof(Index));
            }

            return View(servico);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Servico servico = await _servicoRepositorio.SelectById(id);

            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicoId,Descricao,Valor,Status,UsuarioId")] Servico servico)
        {
            if (Convert.ToInt32(id) != servico.ServicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _servicoRepositorio.Update(servico);

                TempData["Atualizacao"] = "Servico atualizado";

                return RedirectToAction(nameof(Index));
            }

            return View(servico);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _servicoRepositorio.Delete(id);

            TempData["Exclusao"] = "Servico excluido";

            return Json("Serviço excluido");
        }

        [HttpGet]
        public async Task<IActionResult> AprovarServico(int id)
        {
            Servico servico = await _servicoRepositorio.SelectById(id);

            ServicoAprovadoViewModel model = new ServicoAprovadoViewModel
            { 
                ServicoId = servico.ServicoId,
                Descricao = servico.Descricao
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AprovarServico(ServicoAprovadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Servico servico = await _servicoRepositorio.SelectById(model.ServicoId);

                servico.Status = StatusServico.Aceito;

                await _servicoRepositorio.Update(servico);

                ServicoImovel servicoImovel = new ServicoImovel
                {
                    ServicoId = model.ServicoId,
                    DataExecucao = model.Data
                };

                await _servicoImovelRepositorio.Insert(servicoImovel);

                Movimento movimento = new Movimento
                {
                    Valor = servico.Valor,
                    MesId = servicoImovel.DataExecucao.Month,
                    Dia = servicoImovel.DataExecucao.Day,
                    Ano = servicoImovel.DataExecucao.Year,
                    Tipo = Tipos.Saida
                };

                await _movimentoRepositorio.Insert(movimento);

                TempData["NovoRegistro"] = "Serviço escalado com sucesso";

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> ReprovarServico(int id)
        {
            Servico servico = await _servicoRepositorio.SelectById(id);

            if(servico == null)
            {
                return NotFound();
            }

            servico.Status = StatusServico.Recusado;

            await _servicoRepositorio.Update(servico);

            TempData["Exclusao"] = "Servico recusado";

            return RedirectToAction(nameof(Index));
        }
    }
}
