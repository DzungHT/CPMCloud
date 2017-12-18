namespace CPMCloud.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<ActionLog> ActionLogs { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<DomainData> DomainDatas { get; set; }
        public virtual DbSet<DomainType> DomainTypes { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<OAuthClientDetail> OAuthClientDetails { get; set; }
        public virtual DbSet<OAuthDetail> OAuthDetails { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRoleData> UserRoleDatas { get; set; }
        public virtual DbSet<OAuthAccessToken> OAuthAccessTokens { get; set; }
        public virtual DbSet<OAuthRefreshToken> OAuthRefreshTokens { get; set; }
        public virtual DbSet<RoleMenu> RoleMenus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasMany(e => e.DomainTypes)
                .WithMany(e => e.Applications)
                .Map(m => m.ToTable("AppDomainType").MapLeftKey("ApplicationID").MapRightKey("DomainTypeID"));

            modelBuilder.Entity<DomainData>()
                .HasMany(e => e.UserRoleDatas)
                .WithRequired(e => e.DomainData)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.FontIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Controller)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.RoleMenus)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OAuthDetail>()
                .HasOptional(e => e.OAuthClientDetail)
                .WithRequired(e => e.OAuthDetail);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("RolePermission").MapLeftKey("PermissionID").MapRightKey("RoleID"));

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RoleMenus)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoleDatas)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoleDatas)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
