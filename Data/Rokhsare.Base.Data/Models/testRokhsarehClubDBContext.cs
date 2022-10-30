using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Rokhsare.Models.Mapping;

namespace Rokhsare.Models
{
    public partial class testRokhsarehClubDBContext : DbContext
    {
        static testRokhsarehClubDBContext()
        {
            Database.SetInitializer<testRokhsarehClubDBContext>(null);
        }

        public testRokhsarehClubDBContext(string cnn)
            : base(cnn)
        {
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
        public DbSet<ClubFactureStaus> ClubFactureStaus { get; set; }
        public DbSet<ClubPlanDetail> ClubPlanDetails { get; set; }
        public DbSet<ClubPlan> ClubPlans { get; set; }
        public DbSet<ConfilictClubPlanGroup> ConfilictClubPlanGroups { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditStatus> CreditStatus { get; set; }
        public DbSet<CreditType> CreditTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<DefaultClubPlan> DefaultClubPlans { get; set; }
        public DbSet<FactureType> FactureTypes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserPlan> UserPlans { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<SMSTemplate> SMSTemplates { get; set; }
        public DbSet<SMSTemplateToken> SMSTemplateTokens { get; set; }
        public DbSet<SMSTemplateType> SMSTemplateTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new BusinessUnitMap());
            modelBuilder.Configurations.Add(new BusinessUnitNetworkMap());
            modelBuilder.Configurations.Add(new BusinesUnitClubPlanMap());
            modelBuilder.Configurations.Add(new CardMap());
            modelBuilder.Configurations.Add(new ClubFactureMap());
            modelBuilder.Configurations.Add(new ClubFactureStausMap());
            modelBuilder.Configurations.Add(new ClubPlanDetailMap());
            modelBuilder.Configurations.Add(new ClubPlanMap());
            modelBuilder.Configurations.Add(new ConfilictClubPlanGroupMap());
            modelBuilder.Configurations.Add(new CreditMap());
            modelBuilder.Configurations.Add(new CreditStatusMap());
            modelBuilder.Configurations.Add(new CreditTypeMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new DefaultClubPlanMap());
            modelBuilder.Configurations.Add(new FactureTypeMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new NetworkMap());
            modelBuilder.Configurations.Add(new ProductGroupMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductTypeMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserPlanMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserTypeMap());
            modelBuilder.Configurations.Add(new SMSTemplateMap());
            modelBuilder.Configurations.Add(new SMSTemplateTokenMap());
            modelBuilder.Configurations.Add(new SMSTemplateTypeMap());
        }
    }
}
