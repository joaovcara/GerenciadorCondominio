using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using GerenciadorCondominio.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IFuncaoRepositorio _funcaoRepositorio;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, IFuncaoRepositorio funcaoRepositorio, IWebHostEnvironment webHostEnvironment)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _funcaoRepositorio = funcaoRepositorio;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _usuarioRepositorio.SelectAll());
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if(foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = "~/img/" + nomeFoto;
                    }
                }

                Usuario usuario = new Usuario();
                IdentityResult usuarioCriado;

                if (_usuarioRepositorio.VerificaUsuario() == 0)
                {
                    usuario.UserName = model.Nome;
                    usuario.Cpf = model.Cpf;
                    usuario.Email = model.Email;
                    usuario.PhoneNumber = model.Telefone;
                    usuario.Foto = model.Foto;
                    usuario.PrimeiroAcesso = false;
                    usuario.Status = StatusConta.Aprovado;

                    usuarioCriado = await _usuarioRepositorio.CreateUser(usuario, model.Senha);

                    if (usuarioCriado.Succeeded)
                    {
                        await _usuarioRepositorio.InsertUserFunction(usuario, "Administrador");
                        await _usuarioRepositorio.LoginUser(usuario, false);

                        return RedirectToAction("Index", "Usuarios");
                    }
                }

                usuario.UserName = model.Nome;
                usuario.Cpf = model.Cpf;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Foto = model.Foto;
                usuario.PrimeiroAcesso = true;
                usuario.Status = StatusConta.Analisando;

                usuarioCriado = await _usuarioRepositorio.CreateUser(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                {
                    return View("Analise", usuario.UserName);
                }
                else
                {
                    foreach (IdentityError error in usuarioCriado.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _usuarioRepositorio.DeslogarUsuario();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _usuarioRepositorio.DeslogarUsuario();

            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.SelectForEmail(model.Email);

                if(usuario != null)
                {
                    if(usuario.Status == StatusConta.Analisando)
                    {
                        return View("Analise", usuario.UserName);
                    }
                    else if(usuario.Status == StatusConta.Reprovado)
                    {
                        return View("Reprovacao", usuario.UserName);
                    }
                    else if(usuario.PrimeiroAcesso == true)
                    {
                        return RedirectToAction(nameof(RedefinirSenha), usuario);
                    }
                    else
                    {
                        PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

                        if(passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) != PasswordVerificationResult.Failed)
                        {
                            await _usuarioRepositorio.LoginUser(usuario, false);

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Usuario e/ou senha inválidos");

                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario e/ou senha inválidos");

                    return View(model);
                }
            }
            else
            {
                return View(model);
            }

            return View();
        }


        public IActionResult Analise(string nome)
        {
            return View(nome);
        }

        public IActionResult Reprovado(string nome)
        {
            return View(nome);
        }

        public async Task<JsonResult> AprovarUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepositorio.SelectById(usuarioId);
            usuario.Status = StatusConta.Aprovado;
            await _usuarioRepositorio.InsertUserFunction(usuario, "Locatário");
            await _usuarioRepositorio.AtualizarUsuario(usuario);

            return Json(true);
        }

        public async Task<JsonResult> ReprovarUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepositorio.SelectById(usuarioId);
            usuario.Status = StatusConta.Reprovado;
            await _usuarioRepositorio.AtualizarUsuario(usuario);

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GerenciarUsuario(string usuarioId, string nome)
        {
            if(usuarioId == null)
                return NotFound();

            TempData["usuarioId"] = usuarioId;
            ViewBag.Nome = nome;

            Usuario usuario = await _usuarioRepositorio.SelectById(usuarioId);

            if (usuario == null)
                return NotFound();

            List<FuncaoUsuarioViewModel> viewModel = new List<FuncaoUsuarioViewModel>();

            foreach (Funcao funcao in await _funcaoRepositorio.SelectAll())
            {
                FuncaoUsuarioViewModel model = new FuncaoUsuarioViewModel
                {
                    FuncaoId = funcao.Id,
                    Nome = funcao.Name,
                    Descricao = funcao.Descricao
                };

                if(await _usuarioRepositorio.VerificarUsuarioCadastradoFuncao(usuario, funcao.Name))
                {
                    model.IsSelected = true;
                }
                else
                {
                    model.IsSelected = false;
                }

                viewModel.Add(model);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GerenciarUsuario(List<FuncaoUsuarioViewModel> model)
        {
            string usuarioId = TempData["usuarioId"].ToString();

            Usuario usuario = await _usuarioRepositorio.SelectById(usuarioId);

            if (usuario == null)
                return NotFound();

            IEnumerable<string> funcoes = await _usuarioRepositorio.SelectFuncaoUsuario(usuario);

            IdentityResult resultado = await _usuarioRepositorio.RemoverFuncaoUsuario(usuario, funcoes);

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funções do usuário");

                return View("GerenciarUsuario", usuarioId);
            }

            resultado = await _usuarioRepositorio.IncluirFuncaoUsuario(usuario,
                model.Where(x => x.IsSelected == true).Select(x => x.Nome));

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funções do usuário");

                return View("GerenciarUsuario", usuarioId);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> InformacoesUsuario()
        {
            return View(await _usuarioRepositorio.SelectUserName(User));
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarLocatario(string id)
        {
            Usuario usuario = await _usuarioRepositorio.SelectById(id);

            if (usuario == null)
                return NotFound();

            AtualizarLocatarioViewModel model = new AtualizarLocatarioViewModel
            {
                UsuarioId = usuario.Id,
                Nome = usuario.UserName,
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Foto = usuario.Foto,
                Telefone = usuario.PhoneNumber
            };

            TempData["CaminhoFoto"] = usuario.Foto;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarLocatario(AtualizarLocatarioViewModel model, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if(foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = "~/img/" + nomeFoto;
                    }
                }
                else
                {
                    model.Foto = TempData["CaminhoFoto"].ToString();
                }

                Usuario usuario = await _usuarioRepositorio.SelectUserId(model.UsuarioId);

                usuario.UserName = model.Nome;
                usuario.Cpf = model.Cpf;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Foto = model.Foto;

                await _usuarioRepositorio.AtualizarUsuario(usuario);

                if(await _usuarioRepositorio.VerificarUsuarioCadastradoFuncao(usuario, "Administrador") ||
                    await _usuarioRepositorio.VerificarUsuarioCadastradoFuncao(usuario, "Locatário"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(InformacoesUsuario));
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RedefinirSenha(Usuario usuario)
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = usuario.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.SelectForEmail(model.Email);
                model.Senha = _usuarioRepositorio.CodificarSenha(usuario, model.Senha);

                usuario.PasswordHash = model.Senha;
                usuario.PrimeiroAcesso = false;

                await _usuarioRepositorio.AtualizarUsuario(usuario);

                await _usuarioRepositorio.LoginUser(usuario, false);

                return RedirectToAction(nameof(InformacoesUsuario));
            }

            return View(model);
        }
    }
}
