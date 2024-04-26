using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EFFirst.Model
{
    public partial class BookModel : DbContext
    {
        public BookModel()
            : base("name=BookDBContext")
        {
        }

        public virtual DbSet<admins> admins { get; set; }
        public virtual DbSet<books> books { get; set; }
        public virtual DbSet<borrows> borrows { get; set; }
        //public virtual DbSet<book_categories> book_categories { get; set; }
        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<logs> logs { get; set; }
        public virtual DbSet<readers> readers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admins>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<admins>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<admins>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<admins>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<admins>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<admins>()
                .HasMany(e => e.logs)
                .WithRequired(e => e.admins)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<books>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<books>()
                .Property(e => e.author)
                .IsUnicode(false);

            modelBuilder.Entity<books>()
                .Property(e => e.publisher)
                .IsUnicode(false);

            modelBuilder.Entity<books>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<books>()
                .HasMany(e => e.borrows)
                .WithRequired(e => e.books)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<books>()
                .HasMany(e => e.categories)
                .WithMany(e => e.books)
                .Map(m => m.ToTable("book_categories").MapLeftKey("book_id").MapRightKey("category_id"));

            modelBuilder.Entity<categories>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<categories>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<logs>()
                .Property(e => e.action)
                .IsUnicode(false);

            modelBuilder.Entity<readers>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<readers>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<readers>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<readers>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<readers>()
                .HasMany(e => e.borrows)
                .WithRequired(e => e.readers)
                .WillCascadeOnDelete(false);
        }
    }
}
