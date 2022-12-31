﻿using Entities.Entities.Enuns;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_CPF")]
        [StringLength(50)]
        [Display(Name ="CPF")]
        public string? CPF { get; set; }
        [Column("USR_IDADE")]
        [Display(Name = "Idade")]
        public int? Idade { get; set; }
        [Column("USR_NOME")]
        [StringLength(255)]
        [Display(Name ="Nome")]
        public string? Nome { get; set; }
        [Column("USR_CEP")]
        [StringLength(15)]
        [Display(Name = "CEP")]
        public string? CEP { get; set; }
        [Column("USR_ENDERECO")]
        [StringLength(255)]
        [Display(Name = "Endereço")]
        public string? Endereco { get; set; }
        [Column("USR_COMPLEMENTO_ENDERECO")]
        [StringLength(450)]
        [Display(Name = "Complemento")]
        public string? Complemento { get; set; }
        [Column("USR_CELULAR")]
        [StringLength(20)]
        [Display(Name = "Celular")]
        public string? Celular { get; set; }
        [Column("USR_TELEFONE")]
        [StringLength(20)]
        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }
        [Column("USR_ESTADO")]
        [Display(Name = "Estado")]
        public bool? Estado { get; set; }
        [Column("USR_TIPO")]
        [Display(Name = "Tipo")]

        public TipoUsuario? Tipo { get; set; }
    }
}
