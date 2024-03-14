using AutoMapper;
using ControleContatosAPI.Data;
using ControleContatosAPI.Models;
using ControleContatosAPI.Repositorios.Interaces;
using ControleContatosAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ControleContatosAPI.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly IMapper _mapper;

        public UsuarioRepositorio(BancoContext bancoContext, IMapper mapper)
        {
            _bancoContext = bancoContext;
            _mapper = mapper;
        }

        public async Task<ResponseViewModel> BuscarTodosUsuarios()
        {
            try
            {
                List<UsuarioModel> usariosModel = await _bancoContext.Usuarios.Include(x => x.Contatos).ToListAsync();
                List<UsuarioViewModel> usariosViewModel = _mapper.Map<List<UsuarioViewModel>>(usariosModel);

                if (!usariosViewModel.Any())
                {
                    return new ResponseViewModel
                    {
                        data = null,
                        status = new StatusResponseViewModel { code = 200, message = "Não exite usuários cadastrados!" }
                    };
                }

                return new ResponseViewModel
                {
                    data = usariosViewModel,
                    status = new StatusResponseViewModel { code = 200, message = " buscados com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> BuscarPorId(int id)
        {
            try
            {
                UsuarioModel usuarioModel = await _bancoContext.Usuarios.Include(x => x.Contatos).FirstOrDefaultAsync(x => x.Id == id);
                UsuarioViewModel usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuarioModel);

                if (usuarioViewModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = null,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro ao buscar esse usuário! Este usuário é inexistente!" }
                    };
                }

                return new ResponseViewModel
                {
                    data = usuarioViewModel,
                    status = new StatusResponseViewModel { code = 200, message = "Usuário buscado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Método interno, alimenta os métodos de Atualizar, Apagar e AlterarSenhaUsuario
        public async Task<UsuarioModel> BuscarUsuarioModelPorId(int id)
        {
            return await _bancoContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ResponseViewModel> BuscarPorLogin(string login)
        {
            try
            {
                UsuarioModel usuarioModel = await _bancoContext.Usuarios.Include(x => x.Contatos).FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper());
                UsuarioViewModel usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuarioModel);

                if (usuarioViewModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = null,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro ao buscar esse usuário! Este usuário é inexistente!" }
                    };
                }

                return new ResponseViewModel
                {
                    data = usuarioViewModel,
                    status = new StatusResponseViewModel { code = 200, message = "Usuário buscado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> BuscarPorEmailELogin(string email, string login)
        {
            try
            {
                UsuarioModel usuarioModel = await _bancoContext.Usuarios.Include(x => x.Contatos).FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
                UsuarioViewModel usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuarioModel);

                if (usuarioViewModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = null,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro ao buscar esse usuário! Este usuário é inexistente!" }
                    };
                }

                return new ResponseViewModel
                {
                    data = usuarioViewModel,
                    status = new StatusResponseViewModel { code = 200, message = "Usuário buscado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> Adicionar(UsuarioViewModel usuarioViewModel)
        {
            try
            {
                usuarioViewModel.DataCadastro = DateTime.Now;
                UsuarioModel usuarioModel = _mapper.Map<UsuarioModel>(usuarioViewModel);

                if (usuarioModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro na criação deste usuário!" }
                    };
                }

                _bancoContext.Usuarios.Add(usuarioModel);
                await _bancoContext.SaveChangesAsync();

                return new ResponseViewModel
                {
                    data = true,
                    status = new StatusResponseViewModel { code = 200, message = "Usuário adicionado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> Atualizar(UsuarioSemSenhaViewModel usuarioSemSenhaViewModel)
        {
            try
            {
                UsuarioModel usuarioModel = await BuscarUsuarioModelPorId(usuarioSemSenhaViewModel.Id);

                if (usuarioModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro na edição deste usuário! Este usuário é inexistente!" }
                    };
                }

                usuarioModel.Nome = usuarioSemSenhaViewModel.Nome;
                usuarioModel.Login = usuarioSemSenhaViewModel.Login;
                usuarioModel.Email = usuarioSemSenhaViewModel.Email;
                usuarioModel.Perfil = usuarioSemSenhaViewModel.Perfil;
                usuarioModel.DataAtualizalcao = DateTime.Now;

                _bancoContext.Usuarios.Update(usuarioModel);
                await _bancoContext.SaveChangesAsync();

                UsuarioViewModel usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuarioModel);

                return new ResponseViewModel
                {
                    data = true,
                    status = new StatusResponseViewModel { code = 200, message = "Usuário alterado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> Apagar(int id)
        {
            try
            {
                UsuarioModel usuarioModel = await BuscarUsuarioModelPorId(id);

                if (usuarioModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro na deleção deste usuário! Este usuário é inexistente!" }
                    };
                }

                _bancoContext.Usuarios.Remove(usuarioModel);
                await _bancoContext.SaveChangesAsync();

                UsuarioViewModel usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuarioModel);

                return new ResponseViewModel
                {
                    data = true,
                    status = new StatusResponseViewModel { code = 200, message = "Usuário deletado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> AlterarSenhaUsuario(AlterarSenhaViewModel alterarSenhaViewModel)
        {
            try
            {
                UsuarioModel usuarioModel = await BuscarUsuarioModelPorId(alterarSenhaViewModel.Id);

                if (usuarioModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro na edição deste usuário! Este usuário é inexistente!" }
                    };
                }

                if (usuarioModel.Senha != alterarSenhaViewModel.SenhaAtual)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Senha atual não confere!" }
                    };
                }

                if (usuarioModel.Senha == alterarSenhaViewModel.NovaSenha)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Nova senha deve ser diferente da senha atual!" }
                    };
                }

                usuarioModel.Senha = alterarSenhaViewModel.NovaSenha;
                usuarioModel.DataAtualizalcao = DateTime.Now;

                _bancoContext.Usuarios.Update(usuarioModel);
                await _bancoContext.SaveChangesAsync();

                UsuarioViewModel usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuarioModel);

                return new ResponseViewModel
                {
                    data = true,
                    status = new StatusResponseViewModel { code = 200, message = "Senha alterada com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
