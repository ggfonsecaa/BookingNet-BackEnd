using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.BookingFlowAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities.Relations
{
    public class BookingFlowMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingsFlows>().ToTable("BookingsFlows");

            /*Definición de propiedades*/
            modelBuilder.Entity<BookingsFlows>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<BookingsFlows>().Property(entity => entity.BookingId).IsRequired();
            modelBuilder.Entity<BookingsFlows>().Property(entity => entity.FlowId).IsRequired();
            modelBuilder.Entity<BookingsFlows>().Property(entity => entity.DateStartFlow).IsRequired().HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<BookingsFlows>().Property(entity => entity.DateEndFlow);
            modelBuilder.Entity<BookingsFlows>().Ignore(entity => entity.Booking);
            modelBuilder.Entity<BookingsFlows>().Ignore(entity => entity.Flow);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<BookingsFlows>().HasKey(entity => new { entity.BookingId, entity.FlowId, entity.Id });

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<BookingsFlows>().HasOne(entity => entity.Booking).WithMany(related => related.Flows).HasForeignKey(entity => entity.BookingId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BookingsFlows>().HasOne(entity => entity.Flow).WithMany(related => related.Bookings).HasForeignKey(entity => entity.FlowId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}