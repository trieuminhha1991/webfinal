using MyCompanyName.AbpZeroTemplate.Ebook;
using MyCompanyName.AbpZeroTemplate.TypeEbook;
using MyCompanyName.AbpZeroTemplate.Place;
using MyCompanyName.AbpZeroTemplate.SubjectEducation;
using MyCompanyName.AbpZeroTemplate.Status;
using MyCompanyName.AbpZeroTemplate.TypeFile;
using MyCompanyName.AbpZeroTemplate.Rank;
using MyCompanyName.AbpZeroTemplate.Class;
using MyCompanyName.AbpZeroTemplate.Subject;
using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyCompanyName.AbpZeroTemplate.Authorization.Roles;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Chat;
using MyCompanyName.AbpZeroTemplate.Editions;
using MyCompanyName.AbpZeroTemplate.Friendships;
using MyCompanyName.AbpZeroTemplate.MultiTenancy;
using MyCompanyName.AbpZeroTemplate.MultiTenancy.Accounting;
using MyCompanyName.AbpZeroTemplate.MultiTenancy.Payments;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.EntityFrameworkCore
{
    public class AbpZeroTemplateDbContext : AbpZeroDbContext<Tenant, Role, User, AbpZeroTemplateDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<PbEbook> PbEbooks { get; set; }

        public virtual DbSet<PbTypeEbook> PbTypeEbooks { get; set; }

        public virtual DbSet<PbPlace> PbPlaces { get; set; }

        public virtual DbSet<PbSubjectEducation> PbSubjectEducations { get; set; }

        public virtual DbSet<PbStatus> PbStatuses { get; set; }

        public virtual DbSet<PbTypeFile> PbTypeFiles { get; set; }

        public virtual DbSet<PbRank> PbRanks { get; set; }

        public virtual DbSet<PbClass> PbClasses { get; set; }

        public virtual DbSet<PbSubject> PbSubjects { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public AbpZeroTemplateDbContext(DbContextOptions<AbpZeroTemplateDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
