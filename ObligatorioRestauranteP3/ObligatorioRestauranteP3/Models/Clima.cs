using System;
using System.Collections.Generic;

namespace ObligatorioP3.Models
{
    public partial class Clima
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }  // Cambiado a DateTime para almacenar la fecha y hora exacta
        public double Temperatura { get; set; }  // Cambiado a double para almacenar la temperatura en grados Celsius
        public string DescripcionClima { get; set; }  // Mantenido como string para la descripción del clima
    }
}