// <auto-generated />
using Expense_Tracker_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Expense_Tracker_2.Migrations
{
    [DbContext(typeof(ExpenseDBContext))]
    [Migration("20220626183716_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Expense_Tracker_2.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ExpenseCategoryId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseId");

                    b.HasIndex("ExpenseCategoryId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Expense_Tracker_2.Models.ExpenseCategory", b =>
                {
                    b.Property<int>("ExpenseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ExpenseCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseCategoryId");

                    b.ToTable("ExpensesCategory");
                });

            modelBuilder.Entity("Expense_Tracker_2.Models.Expense", b =>
                {
                    b.HasOne("Expense_Tracker_2.Models.ExpenseCategory", "expenseCategory")
                        .WithMany()
                        .HasForeignKey("ExpenseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("expenseCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
