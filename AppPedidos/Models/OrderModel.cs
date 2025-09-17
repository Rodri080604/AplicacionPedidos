using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppPedidos.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public UserModel Cliente { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public OrderStatus Estado { get; set; }

        [Required(ErrorMessage = "El total es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El total no puede ser negativo")]
        public decimal Total { get; set; }

        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    }
    public enum OrderStatus
    {
        Pendiente,
        Procesado,
        Enviado,
        Entregado
    }
}
