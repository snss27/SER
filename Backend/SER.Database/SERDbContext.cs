using Microsoft.EntityFrameworkCore;
using SER.Database.Models.AdditionalQualifications;
using SER.Database.Models.Clusters;
using SER.Database.Models.ConfigurationTools;
using SER.Database.Models.EducationLevels;
using SER.Database.Models.Employees;
using SER.Database.Models.Enterprises;
using SER.Database.Models.Groups;
using SER.Database.Models.Students;
using SER.Database.Models.WorkPlaces;
using SER.Tools.Types.IDs;

namespace SER.Database;
public class SERDbContext(DbContextOptions<SERDbContext> options) : DbContext(options)
{
	public DbSet<AdditionalQualificationEntity> AdditionalQualifications { get; set; }
	public DbSet<ClusterEntity> Clusters { get; set; }
	public DbSet<EducationLevelEntity> EducationLevels { get; set; }
	public DbSet<EmployeeEntity> Employees { get; set; }
	public DbSet<EnterpriseEntity> Enterprises { get; set; }
	public DbSet<GroupEntity> Groups { get; set; }
	public DbSet<StudentEntity> Students { get; set; }
	public DbSet<WorkPlaceEntity> WorkPlaces { get; set; }

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		configurationBuilder.Properties<ID>().HaveConversion<IDConverter>();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AdditionalQualificationConfiguration());
		modelBuilder.ApplyConfiguration(new ClusterConfiguration());
		modelBuilder.ApplyConfiguration(new EducationLevelConfiguration());
		modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
		modelBuilder.ApplyConfiguration(new EnterpriseConfiguration());
		modelBuilder.ApplyConfiguration(new GroupConfiguration());
		modelBuilder.ApplyConfiguration(new StudentConfiguration());
		modelBuilder.ApplyConfiguration(new WorkPlaceConfiguration());

		base.OnModelCreating(modelBuilder);
	}
}
