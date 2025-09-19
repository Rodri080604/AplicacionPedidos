using Microsoft.EntityFrameworkCore;
using AppPedidos.Models;

namespace AppPedidos.Data
{
    public class PedidosDBContext : DbContext
    {
        public PedidosDBContext(DbContextOptions<PedidosDBContext> options) : base(options) { }

        public PedidosDBContext() { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-67EHGLH;Database=AppPedidosDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Order - User (Cliente)
            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Cliente)              // Un pedido tiene un cliente
                .WithMany(u => u.Orders)             // Un usuario puede tener muchos pedidos
                .HasForeignKey(o => o.ClienteId)     // FK en OrderModel
                .OnDelete(DeleteBehavior.Restrict);  // Evita borrar en cascada clientes con pedidos

            // Order - OrderItem
            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.Items)               // Un pedido tiene muchos items
                .WithOne(i => i.Order)               // Un item pertenece a un pedido
                .HasForeignKey(i => i.OrderId)       // FK en OrderItemModel
                .OnDelete(DeleteBehavior.Cascade);   // Si eliminas un pedido, se borran los items

            // OrderItem - Product
            modelBuilder.Entity<OrderItemModel>()
                .HasOne(i => i.Product)              // Un item tiene un producto
                .WithMany(p => p.OrderItems)         // Un producto puede estar en muchos items
                .HasForeignKey(i => i.ProductId)     // FK en OrderItemModel
                .OnDelete(DeleteBehavior.Restrict);  // Evita borrar producto si tiene items asociados
        }

    }
}