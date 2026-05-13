using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escola_Sprint.Migrations
{
    /// <inheritdoc />
    public partial class Banco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CH_Total = table.Column<int>(type: "int", nullable: false),
                    CH_Semanal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.IdCurso);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sexo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titularidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    IdAluno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sexo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.IdAluno);
                    table.ForeignKey(
                        name: "fk_Aluno_Cursos",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    IdDisciplina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CH = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigoprofessor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.IdDisciplina);
                    table.ForeignKey(
                        name: "fk_disciplinas_professor1",
                        column: x => x.codigoprofessor,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aluno_has_disciplinas",
                columns: table => new
                {
                    Aluno_idAluno = table.Column<int>(type: "int", nullable: false),
                    Disciplinas_idDisciplina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno_has_disciplinas", x => new { x.Aluno_idAluno, x.Disciplinas_idDisciplina });
                    table.ForeignKey(
                        name: "FK_aluno_has_disciplinas_Disciplinas_Disciplinas_idDisciplina",
                        column: x => x.Disciplinas_idDisciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "IdDisciplina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aluno_has_disciplinas_aluno_Aluno_idAluno",
                        column: x => x.Aluno_idAluno,
                        principalTable: "aluno",
                        principalColumn: "IdAluno",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disciplinas_has_cursos",
                columns: table => new
                {
                    Cursos_idCurso = table.Column<int>(type: "int", nullable: false),
                    Disciplinas_idDisciplina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplinas_has_cursos", x => new { x.Cursos_idCurso, x.Disciplinas_idDisciplina });
                    table.ForeignKey(
                        name: "FK_disciplinas_has_cursos_Cursos_Cursos_idCurso",
                        column: x => x.Cursos_idCurso,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disciplinas_has_cursos_Disciplinas_Disciplinas_idDisciplina",
                        column: x => x.Disciplinas_idDisciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "IdDisciplina",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notas",
                columns: table => new
                {
                    idRelatorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nota_1 = table.Column<float>(type: "float", nullable: false),
                    Nota_2 = table.Column<float>(type: "float", nullable: false),
                    Nota_3 = table.Column<float>(type: "float", nullable: false),
                    Ano_letivo = table.Column<int>(type: "int", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notas", x => x.idRelatorio);
                    table.ForeignKey(
                        name: "fk_Notas_Aluno1",
                        column: x => x.AlunoId,
                        principalTable: "aluno",
                        principalColumn: "IdAluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_Notas_Disciplinas1",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "IdDisciplina",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_IdCurso",
                table: "aluno",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_has_disciplinas_Disciplinas_idDisciplina",
                table: "aluno_has_disciplinas",
                column: "Disciplinas_idDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_codigoprofessor",
                table: "Disciplinas",
                column: "codigoprofessor");

            migrationBuilder.CreateIndex(
                name: "IX_disciplinas_has_cursos_Disciplinas_idDisciplina",
                table: "disciplinas_has_cursos",
                column: "Disciplinas_idDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_notas_AlunoId",
                table: "notas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_notas_DisciplinaId",
                table: "notas",
                column: "DisciplinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aluno_has_disciplinas");

            migrationBuilder.DropTable(
                name: "disciplinas_has_cursos");

            migrationBuilder.DropTable(
                name: "notas");

            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
