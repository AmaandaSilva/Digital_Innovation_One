using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter no minino 3 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter no minino 3 caracteres")]
        public string Produtora { get; set; }

        [Required]
        [Range(1,1000, ErrorMessage = "O preço deve ser de no minino R$ 1,00")]
        public double Preco { get; set; }

    }
}
