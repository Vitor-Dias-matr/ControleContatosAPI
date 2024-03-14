using AutoMapper;
using ControleContatosAPI.Data;
using ControleContatosAPI.Models;
using ControleContatosAPI.Repositorios.Interaces;
using ControleContatosAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ControleContatosAPI.Repositorios
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ContatoRepositorio(BancoContext bancoContext, IMapper mapper, IUsuarioRepositorio usuarioRepositorio)
        {
            _bancoContext = bancoContext;
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<ResponseViewModel> BuscarTodosContatos()
        {
            try
            {
                List<ContatoModel> contatosModel = await _bancoContext.Contatos.ToListAsync();
                List<ContatoViewModel> contatosViewModel = _mapper.Map<List<ContatoViewModel>>(contatosModel);

                if (!contatosViewModel.Any())
                {
                    return new ResponseViewModel
                    {
                        data = null,
                        status = new StatusResponseViewModel { code = 200, message = "Não exite contatos cadastrados!" }
                    };
                }

                return new ResponseViewModel
                {
                    data = contatosViewModel,
                    status = new StatusResponseViewModel { code = 200, message = "Contatos buscados com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> BuscarContatosDeUsuario(int usuarioId)
        {
            try
            {
                ResponseViewModel response = await _usuarioRepositorio.BuscarPorId(usuarioId);

                if (response.data == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro ao buscar os contatos deste usuário! Este usuário é inexistente!" }
                    };
                }

                List<ContatoModel> contatosModel = await _bancoContext.Contatos.Where(x => x.UsuarioId == usuarioId).ToListAsync();
                List<ContatoViewModel> contatosViewModel = _mapper.Map<List<ContatoViewModel>>(contatosModel);

                if (!contatosViewModel.Any())
                {
                    return new ResponseViewModel
                    {
                        data = null,
                        status = new StatusResponseViewModel { code = 200, message = "Não exite contatos vinculados a esse usuário!" }
                    };
                }

                return new ResponseViewModel
                {
                    data = contatosViewModel,
                    status = new StatusResponseViewModel { code = 200, message = "Contatos buscados com sucesso!" }
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
                ContatoModel contatoModel = await _bancoContext.Contatos.FirstOrDefaultAsync(x => x.Id == id);
                ContatoViewModel contatoViewModel = _mapper.Map<ContatoViewModel>(contatoModel);

                if (contatoViewModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = null,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro ao buscar esse contato! Este contato é inexistente!" }
                    };
                }

                return new ResponseViewModel
                {
                    data = contatoViewModel,
                    status = new StatusResponseViewModel { code = 200, message = "Contato buscado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Método interno, alimenta os métodos de Atualizar e Apagar
        public async Task<ContatoModel> BuscarContatoModelPorId(int id)
        {
            return await _bancoContext.Contatos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ResponseViewModel> Adicionar(ContatoViewModel contatoViewModel)
        {
            try
            {
                ContatoModel contatoModel = _mapper.Map<ContatoModel>(contatoViewModel);

                if (contatoModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro na criação deste contato!" }
                    };
                }

                _bancoContext.Contatos.Add(contatoModel);
                await _bancoContext.SaveChangesAsync();

                return new ResponseViewModel
                {
                    data = true,
                    status = new StatusResponseViewModel { code = 200, message = "Contato adicionado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseViewModel> Atualizar(ContatoViewModel contatoViewModel)
        {
            try
            {
                ContatoModel contatoModel = await BuscarContatoModelPorId(contatoViewModel.Id);

                if (contatoModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro na edição do contato! Este contato é inexistente!" }
                    };
                }

                contatoModel.Nome = contatoViewModel.Nome;
                contatoModel.Celular = contatoViewModel.Celular;
                contatoModel.Email = contatoViewModel.Email;
                contatoModel.UsuarioId = contatoViewModel.UsuarioId;

                _bancoContext.Contatos.Update(contatoModel);
                await _bancoContext.SaveChangesAsync();
                
                return new ResponseViewModel
                {
                    data = true,
                    status = new StatusResponseViewModel { code = 200, message = "Contato alterado com sucesso!" }
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
                ContatoModel contatoModel = await BuscarContatoModelPorId(id);

                if (contatoModel == null)
                {
                    return new ResponseViewModel
                    {
                        data = false,
                        status = new StatusResponseViewModel { code = 500, message = "Houve um erro na deleção do contato! Este contato é inexistente!" }
                    };
                }

                _bancoContext.Contatos.Remove(contatoModel);
                await _bancoContext.SaveChangesAsync();

                ContatoViewModel contatoViewModel = _mapper.Map<ContatoViewModel>(contatoModel);

                return new ResponseViewModel
                {
                    data = true,
                    status = new StatusResponseViewModel { code = 200, message = "Contato deletado com sucesso!" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}