using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.DataConcerns;

namespace Repository;

public partial class TodoContext : DbContext
{
    public TodoContext()
    {
    }

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TodoTask> TodoTasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoApp;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE20636B7F4021");

            entity.ToTable("Status");

            entity.Property(e => e.Status1)
                .HasMaxLength(10)
                .HasColumnName("Status");
        });

        modelBuilder.Entity<TodoTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__TodoTask__7C6949B102D6F9DC");

            entity.Property(e => e.CompletedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TaskDescription).HasMaxLength(200);
            entity.Property(e => e.TaskName).HasMaxLength(100);

            entity.HasOne(d => d.Status).WithMany(p => p.TodoTasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TodoTasks__Statu__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.TodoTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TodoTasks__UserI__46E78A0C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CF450A1FA");

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
