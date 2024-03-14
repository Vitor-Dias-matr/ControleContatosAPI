using AutoMapper;
using ControleContatosAPI.Models;
using ControleContatosAPI.ViewModel;

namespace ControleContatosAPI.Mappings
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel() 
        {
            CreateMap<AlterarSenhaModel, AlterarSenhaViewModel>().ReverseMap();
            CreateMap<ContatoModel, ContatoViewModel>().ReverseMap();
            CreateMap<ResponseModel, ResponseViewModel>().ReverseMap();
            CreateMap<UsuarioSemSenhaModel, UsuarioSemSenhaViewModel>().ReverseMap();
            CreateMap<UsuarioModel, UsuarioViewModel>().ReverseMap();
        }
    }
}