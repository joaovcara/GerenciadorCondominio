using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class EventosController : Controller
    {
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public EventosController(IEventoRepositorio eventoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _eventoRepositorio = eventoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.SelectUserName(User);

            if (await _usuarioRepositorio.VerificarUsuarioCadastradoFuncao(usuario, "Locatário"))
            {
                return View(await _eventoRepositorio.SelectEventoUsuarioId(usuario.Id));
            }

            return View(await _eventoRepositorio.SelectAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Usuario usuario = await _usuarioRepositorio.SelectUserName(User);
            Evento evento = new Evento
            {
                UsuarioId = usuario.Id
            };

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Descricao,Data,Obsercacao,UsuarioId")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Insert(evento);

                TempData["NovoRegistro"] = "Evento cadastrado com sucesso";

                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Evento evento = await _eventoRepositorio.SelectById(id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Descricao,Data,Obsercacao,UsuarioId")] Evento evento)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Update(evento);
                TempData["Atualizacao"] = "Evento atualizado com sucesso";

                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _eventoRepositorio.Delete(id);
            TempData["Exclusao"] = "Evento excluido com sucesso";

            return Json("Evento Excluido");
        }
    }
}
