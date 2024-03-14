﻿using ControleContatosAPI.Models;

namespace ControleContatosAPI.ViewModel
{
    public class ContatoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public int? UsuarioId { get; set; }
    }
}