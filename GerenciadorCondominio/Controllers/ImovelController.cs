using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class ImovelController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IImovelRepositorio _imovelRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ImovelController(IWebHostEnvironment webHostEnviroment, IImovelRepositorio imovelRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _webHostEnviroment = webHostEnviroment;
            _imovelRepositorio = imovelRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.SelectUserName(User);

            if (await _usuarioRepositorio.VerificarUsuarioCadastradoFuncao(usuario, "Administrador"))
            {
                return View(await _imovelRepositorio.SelectAll());
            }

            return View(await _imovelRepositorio.SelectImovelUsuarioId(usuario.Id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["LocatarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName");
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImovelId,Descricao,Endereco,Bairro,Numero,Cidade,Estado,Foto,LocatarioId,ProprietarioId")] Imovel imovel, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string dir = Path.Combine(_webHostEnviroment.WebRootPath, "img");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(dir, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        imovel.Foto = "~/img/" + nomeFoto;
                    }
                }
                await _imovelRepositorio.Insert(imovel);

                TempData["NovoRegistro"] = "Imovel criado com sucesso";

                return RedirectToAction(nameof(Index));
            }

            ViewData["LocatarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName", imovel.LocatarioId);
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName", imovel.ProprietarioId);

            return View(imovel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Imovel imovel = await _imovelRepositorio.SelectById(id);

            if (imovel == null)
            {
                return NotFound();
            }

            TempData["DirFoto"] = imovel.Foto;

            ViewData["LocatarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName", imovel.LocatarioId);
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName", imovel.ProprietarioId);

            return View(imovel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ImovelId,Descricao,Endereco,Bairro,Numero,Cidade,Estado,Foto,LocatarioId,ProprietarioId")] Imovel imovel, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string dir = Path.Combine(_webHostEnviroment.WebRootPath, "img");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(dir, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        imovel.Foto = "~/img/" + nomeFoto;

                        System.IO.File.Delete(TempData["DirFoto"].ToString().Replace("~", "wwwroot"));
                    }
                }else
                    imovel.Foto = TempData["DirFoto"].ToString();

                await _imovelRepositorio.Update(imovel);

                TempData["Atualizacao"] = "Apartamento atualizado";

                return RedirectToAction(nameof(Index));
            }

            ViewData["LocatarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName", imovel.LocatarioId);
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.SelectAll(), "Id", "UserName", imovel.ProprietarioId);

            return View(imovel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            Imovel imovel = await _imovelRepositorio.SelectById(id);
            System.IO.File.Delete(imovel.Foto.Replace("~", "wwwroot"));

            await _imovelRepositorio.Delete(imovel);

            TempData["Exclusao"] = "Imovel excluido com sucesso";

            return Json("Imovel excluido com sucesso");
        }
    }
}
