using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SER.Database.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "additional_qualifications",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    study_time = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additional_qualifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clusters",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clusters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "education_levels",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    study_time = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_levels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    second_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enterprises",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    legal_address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    actual_address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    inn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    kpp = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    orgn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    phone = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    mail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    is_opk = table.Column<bool>(type: "boolean", nullable: false),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enterprises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    number = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    sctructural_unit = table.Column<int>(type: "integer", nullable: false),
                    education_level_id = table.Column<byte[]>(type: "bytea", nullable: false),
                    enrollment_year = table.Column<int>(type: "integer", nullable: false),
                    curator_id = table.Column<byte[]>(type: "bytea", nullable: true),
                    has_cluster = table.Column<bool>(type: "boolean", nullable: false),
                    cluster_id = table.Column<byte[]>(type: "bytea", nullable: true),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_group_cluster",
                        column: x => x.cluster_id,
                        principalTable: "clusters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_group_curator",
                        column: x => x.curator_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_group_education_level",
                        column: x => x.education_level_id,
                        principalTable: "education_levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    second_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    birth_date = table.Column<DateTime>(type: "date", nullable: true),
                    phone_number = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    representative_phone_number = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    is_on_paid_study = table.Column<bool>(type: "boolean", nullable: false),
                    snils = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    group_id = table.Column<byte[]>(type: "bytea", nullable: false),
                    passport_number = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    passport_series = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    passport_issued_by = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    passport_issued_date = table.Column<DateTime>(type: "date", nullable: true),
                    passport_files = table.Column<List<string>>(type: "varchar[]", nullable: false),
                    is_target_agreement = table.Column<bool>(type: "boolean", nullable: false),
                    target_agreement_files = table.Column<List<string>>(type: "varchar[]", nullable: false),
                    target_agreement_date = table.Column<DateTime>(type: "date", nullable: true),
                    target_agreement_enterprise_id = table.Column<byte[]>(type: "bytea", nullable: true),
                    must_serve_in_army = table.Column<bool>(type: "boolean", nullable: false),
                    army_subpoena_files = table.Column<List<string>>(type: "varchar[]", nullable: false),
                    army_call_date = table.Column<DateTime>(type: "date", nullable: true),
                    social_statuses = table.Column<int[]>(type: "int[]", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    is_foreign_citizen = table.Column<bool>(type: "boolean", nullable: false),
                    inn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    mail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    other_files = table.Column<List<string>>(type: "varchar[]", nullable: false),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_group",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_student_target_agreement_enterprise",
                        column: x => x.target_agreement_enterprise_id,
                        principalTable: "enterprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "work_places",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    enterprise_id = table.Column<byte[]>(type: "bytea", nullable: false),
                    post = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    work_book_extract_files = table.Column<List<string>>(type: "varchar[]", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    finish_date = table.Column<DateTime>(type: "date", nullable: true),
                    student_id = table.Column<byte[]>(type: "bytea", nullable: false),
                    is_current = table.Column<bool>(type: "boolean", nullable: false),
                    created_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_datetime_utc = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_places", x => x.id);
                    table.ForeignKey(
                        name: "fk_workplace_enterprise",
                        column: x => x.enterprise_id,
                        principalTable: "enterprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_workplace_student",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_groups_cluster_id",
                table: "groups",
                column: "cluster_id");

            migrationBuilder.CreateIndex(
                name: "IX_groups_curator_id",
                table: "groups",
                column: "curator_id");

            migrationBuilder.CreateIndex(
                name: "IX_groups_education_level_id",
                table: "groups",
                column: "education_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_students_group_id",
                table: "students",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_students_target_agreement_enterprise_id",
                table: "students",
                column: "target_agreement_enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_places_enterprise_id",
                table: "work_places",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_places_student_id",
                table: "work_places",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "additional_qualifications");

            migrationBuilder.DropTable(
                name: "work_places");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "enterprises");

            migrationBuilder.DropTable(
                name: "clusters");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "education_levels");
        }
    }
}
