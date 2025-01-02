using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicBooking.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFieldValues_RegistrationEventFieldValue_RegistrationEventFieldValueId",
                table: "EventFieldValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_RegistrationEventFieldValue_RegistrationEventFieldValueId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationEventFieldValue",
                table: "RegistrationEventFieldValue");

            migrationBuilder.RenameTable(
                name: "RegistrationEventFieldValue",
                newName: "RegistrationEventFieldValues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationEventFieldValues",
                table: "RegistrationEventFieldValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFieldValues_RegistrationEventFieldValues_RegistrationEventFieldValueId",
                table: "EventFieldValues",
                column: "RegistrationEventFieldValueId",
                principalTable: "RegistrationEventFieldValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_RegistrationEventFieldValues_RegistrationEventFieldValueId",
                table: "Registrations",
                column: "RegistrationEventFieldValueId",
                principalTable: "RegistrationEventFieldValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFieldValues_RegistrationEventFieldValues_RegistrationEventFieldValueId",
                table: "EventFieldValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_RegistrationEventFieldValues_RegistrationEventFieldValueId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationEventFieldValues",
                table: "RegistrationEventFieldValues");

            migrationBuilder.RenameTable(
                name: "RegistrationEventFieldValues",
                newName: "RegistrationEventFieldValue");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationEventFieldValue",
                table: "RegistrationEventFieldValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFieldValues_RegistrationEventFieldValue_RegistrationEventFieldValueId",
                table: "EventFieldValues",
                column: "RegistrationEventFieldValueId",
                principalTable: "RegistrationEventFieldValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_RegistrationEventFieldValue_RegistrationEventFieldValueId",
                table: "Registrations",
                column: "RegistrationEventFieldValueId",
                principalTable: "RegistrationEventFieldValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
