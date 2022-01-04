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
    public class VeiculosController : Controller
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public VeiculosController(IVeiculoRepositorio veiculoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeiculoId,Descricao,Marca,Cor,Placa,UsuarioId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.SelectUserName(User);
                veiculo.UsuarioId = usuario.Id;

                await _veiculoRepositorio.Insert(veiculo);

                return RedirectToAction("InformacoesUsuario", "Usuarios");
            }

            return View(veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var veiculo = await _veiculoRepositorio.SelectById(id);

            if(veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeiculoId,Descricao,Marca,Cor,Placa,UsuarioId")] Veiculo veiculo)
        {
            if(id != veiculo.VeiculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _veiculoRepositorio.Update(veiculo);
                return RedirectToAction("InformacoesUsuario", "Usuarios");
            }

            return View(veiculo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _veiculoRepositorio.Delete(id);

            return Json("Veiculo excluido com sucesso");
        }
    }
}
