using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppPedidos.Models
{
    public class OrderItemModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El pedido es obligatorio")]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public OrderModel Order { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El subtotal no puede ser negativo")]
        public decimal Subtotal { get; set; }
    }
}
