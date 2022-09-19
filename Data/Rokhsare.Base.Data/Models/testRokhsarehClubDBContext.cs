using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Rokhsare.Base.Data.Models.Mapping;

namespace Rokhsare.Base.Data.Models
{
    public partial class testRokhsarehClubDBContext : DbContext
    {
        static testRokhsarehClubDBContext()
        {
            Database.SetInitializer<testRokhsarehClubDBContext>(null);
        }

        public testRokhsarehClubDBContext()
            : base("Name=testRokhsarehClubDBContext")
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<BusinessUnitNetwork> BusinessUnitNetworks { get; set; }
        public DbSet<BusinesUnitClubPlan> BusinesUnitClubPlans { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<ClubFacture> ClubFactures { get; set; }
        public DbSet<ClubPlanDetail> ClubPlanDetails { get; set; }
        public DbSet<ClubPlanGroup> ClubPlanGroups { get; set; }
        public DbSet<ClubPlan> ClubPlans { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditStatu> CreditStatus { get; set; }
        public DbSet<CreditType> CreditTypes { get; set; }
        public DbSet<DefaultClubPlan> DefaultClubPlans { get; set; }
        public DbSet<FactureType> FactureTypes { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserPlan> UserPlans { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new BusinessUnitMap());
            modelBuilder.Configurations.Add(new BusinessUnitNetworkMap());
            modelBuilder.Configurations.Add(new BusinesUnitClubPlanMap());
            modelBuilder.Configurations.Add(new CardMap());
            modelBuilder.Configurations.Add(new ClubFactureMap());
            modelBuilder.Configurations.Add(new ClubPlanDetailMap());
            modelBuilder.Configurations.Add(new ClubPlanGroupMap());
            modelBuilder.Configurations.Add(new ClubPlanMap());
            modelBuilder.Configurations.Add(new CreditMap());
            modelBuilder.Configurations.Add(new CreditStatuMap());
            modelBuilder.Configurations.Add(new CreditTypeMap());
            modelBuilder.Configurations.Add(new DefaultClubPlanMap());
            modelBuilder.Configurations.Add(new FactureTypeMap());
            modelBuilder.Configurations.Add(new NetworkMap());
            modelBuilder.Configurations.Add(new ProductGroupMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductTypeMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserPlanMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserTypeMap());
        }
    }
}
