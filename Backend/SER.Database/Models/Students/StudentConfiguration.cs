using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.ConfigurationTools;
using SER.Domain.Students.Enums;

namespace SER.Database.Models.Students;
public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
	public void Configure(EntityTypeBuilder<StudentEntity> builder)
	{
		builder.ToTable("students");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("name");

		builder.Property(x => x.SecondName)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("second_name");

		builder.Property(x => x.LastName)
			.HasMaxLength(200)
			.HasColumnName("last_name");

		builder.Property(x => x.Gender)
			.IsRequired()
			.HasColumnName("gender");

		builder.Property(x => x.BirthDate)
			.HasColumnName("birth_date")
			.HasColumnType("date");

		builder.Property(x => x.PhoneNumber)
			.HasMaxLength(200)
			.HasColumnName("phone_number");

		builder.Property(x => x.RepresentativePhoneNumber)
			.HasMaxLength(200)
			.HasColumnName("representative_phone_number");

		builder.Property(x => x.IsOnPaidStudy)
			.IsRequired()
			.HasColumnName("is_on_paid_study");

		builder.Property(x => x.Snils)
			.HasConversion(new EncryptedStringConverter())
			.HasMaxLength(200)
			.HasColumnName("snils");

		builder.Property(x => x.GroupId)
			.HasColumnName("group_id")
			.HasColumnType("bytea");

		builder.HasOne(x => x.Group)
			.WithMany()
			.HasForeignKey(x => x.GroupId)
			.HasConstraintName("fk_student_group")
			.OnDelete(DeleteBehavior.Restrict);

		builder.Property(x => x.PassportNumber)
			.HasConversion(new EncryptedStringConverter())
			.HasMaxLength(200)
			.HasColumnName("passport_number");

		builder.Property(x => x.PassportSeries)
			.HasConversion(new EncryptedStringConverter())
			.HasMaxLength(200)
			.HasColumnName("passport_series");

		builder.Property(x => x.PassportIssuedBy)
			.HasConversion(new EncryptedStringConverter())
			.HasMaxLength(200)
			.HasColumnName("passport_issued_by");

		builder.Property(x => x.PassportIssuedDate)
			.HasColumnName("passport_issued_date")
			.HasColumnType("date");

		builder.Property(x => x.PassportFiles)
			.IsRequired()
			.HasColumnName("passport_files")
			.HasColumnType("varchar[]");

		builder.HasMany(x => x.WorkPlaces)
			.WithOne(x => x.Student)
			.HasForeignKey(x => x.StudentId)
			.HasConstraintName("fk_workplace_student")
			.OnDelete(DeleteBehavior.Cascade);

		builder.Property(x => x.IsTargetAgreement)
			.IsRequired()
			.HasColumnName("is_target_agreement");

		builder.Property(x => x.TargetAgreementFiles)
			.IsRequired()
			.HasColumnName("target_agreement_files")
			.HasColumnType("varchar[]");

		builder.Property(x => x.TargetAgreementDate)
			.HasColumnName("target_agreement_date")
			.HasColumnType("date");

		builder.Property(x => x.TargetAgreementEnterpriseId)
			.HasColumnName("target_agreement_enterprise_id")
			.HasColumnType("bytea");

		builder.HasOne(x => x.TargetAgreementEnterprise)
			.WithMany()
			.HasForeignKey(x => x.TargetAgreementEnterpriseId)
			.HasConstraintName("fk_student_target_agreement_enterprise")
			.OnDelete(DeleteBehavior.SetNull);

		builder.Property(x => x.MustServeInArmy)
			.IsRequired()
			.HasColumnName("must_serve_in_army");

		builder.Property(x => x.ArmySubpoenaFiles)
			.IsRequired()
			.HasColumnName("army_subpoena_files")
			.HasColumnType("varchar[]");

		builder.Property(x => x.ArmyCallDate)
			.HasColumnName("army_call_date")
			.HasColumnType("date");

		builder.Property(x => x.SocialStatuses)
			.IsRequired()
			.HasEnumListConversion()
			.HasColumnType("int[]")
			.HasColumnName("social_statuses");

		builder.Property(x => x.Status)
			.IsRequired()
			.HasColumnName("status");

		builder.Property(x => x.Address)
			.HasMaxLength(200)
			.HasColumnName("address");

		builder.Property(x => x.IsForeignCitizen)
			.IsRequired()
			.HasColumnName("is_foreign_citizen");

		builder.Property(x => x.Inn)
			.HasMaxLength(200)
			.HasColumnName("inn");

		builder.Property(x => x.Mail)
			.HasMaxLength(200)
			.HasColumnName("mail");

		builder.Property(x => x.OtherFiles)
			.IsRequired()
			.HasColumnName("other_files")
			.HasColumnType("varchar[]");
	}
}
