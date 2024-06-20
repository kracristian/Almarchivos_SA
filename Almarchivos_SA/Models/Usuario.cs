using Almarchivos_SA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Almarchivos_SA.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre_Usuario { get; set; }

        [Required]
        [StringLength(1000)]
        public string Contraseña { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Fecha_Creacion { get; set; }
    }
}
