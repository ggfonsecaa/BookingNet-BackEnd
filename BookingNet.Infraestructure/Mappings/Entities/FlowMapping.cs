using BookingNet.Domain.Aggregates.FlowAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class FlowMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Flow>().ToTable("Flows");

            /*Definición de propiedades*/
            modelBuilder.Entity<Flow>().Property(g => g.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Flow>().Property(g => g.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Flow>().Property(g => g.FlowId);
            modelBuilder.Entity<Flow>().Property(g => g.UserId).IsRequired();
            modelBuilder.Entity<Flow>().Property(g => g.BookingStatusId).IsRequired();
            modelBuilder.Entity<Flow>().Property(g => g.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Flow>().Ignore(g => g.ParentFlow);
            modelBuilder.Entity<Flow>().Ignore(g => g.User);
            modelBuilder.Entity<Flow>().Ignore(g => g.BookingStatus);

            /*Definición de indices*/
            modelBuilder.Entity<Flow>().HasIndex(g => new { g.Name }).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Flow>().HasKey(g => g.Id);

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<Flow>().HasOne(f => f.ParentFlow).WithOne().HasForeignKey<Flow>(f => f.FlowId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flow>().HasOne(f => f.User).WithMany(f => f.Flows).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flow>().HasOne(f => f.BookingStatus).WithMany(f => f.Flows).HasForeignKey(f => f.BookingStatusId).OnDelete(DeleteBehavior.Restrict);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<Flow>().HasData(
                    new Flow { Id = 1, Name = "Creación de reserva", HasPreviousFlow = false, FlowId = null, UserId = 1, BookingStatusId = 1 },
                    new Flow { Id = 2, Name = "Validación de disponibilidad", HasPreviousFlow = true, FlowId = 1, UserId = 1, BookingStatusId = 2 },
                    new Flow { Id = 3, Name = "Validación de inventarios", HasPreviousFlow = true, FlowId = 2, UserId = 1, BookingStatusId = 3 },
                    new Flow { Id = 4, Name = "Pago de reserva", HasPreviousFlow = true, FlowId = 3, UserId = 1, BookingStatusId = 4 },
                    new Flow { Id = 5, Name = "Confirmación de evento", HasPreviousFlow = true, FlowId = 4, UserId = 1, BookingStatusId = 5 },
                    new Flow { Id = 6, Name = "Realización del evento", HasPreviousFlow = true, FlowId = 5, UserId = 1, BookingStatusId = 6 },
                    new Flow { Id = 7, Name = "Rechazo del evento", HasPreviousFlow = false, FlowId = null, UserId = 1, BookingStatusId = 7 });
        }
    }
}