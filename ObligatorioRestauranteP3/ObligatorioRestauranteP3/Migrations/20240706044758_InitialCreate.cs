using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioRestauranteP3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clima",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<TimeOnly>(type: "time", nullable: true),
                    Temperatura = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DescripcionClima = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clima__3214EC27FF2E80F8", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePlato = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: true),
                    Categoria = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menu__3214EC27C1AF2A5D", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Restaurante",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    HoraApertura = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraCierre = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Restaura__3214EC27883AC825", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Contraseña = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3214EC27BD9EA1BF", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FotoRestaurante",
                columns: table => new
                {
                    RestauranteId = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FotoRest__AAF3667BA3052A49", x => x.RestauranteId);
                    table.ForeignKey(
                        name: "FK__FotoResta__Resta__44FF419A",
                        column: x => x.RestauranteId,
                        principalTable: "Restaurante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MenuRestaurante",
                columns: table => new
                {
                    IdMenu = table.Column<int>(type: "int", nullable: false),
                    IdRestaurante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuRest__FFE24EAE75CC1FE9", x => new { x.IdMenu, x.IdRestaurante });
                    table.ForeignKey(
                        name: "FkMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menu",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "IdRestaurante",
                        column: x => x.IdRestaurante,
                        principalTable: "Restaurante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Mesa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroMesa = table.Column<int>(type: "int", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Restauranteid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mesa__3214EC27097A673C", x => x.ID);
                    table.ForeignKey(
                        name: "FkMesaRestaurante",
                        column: x => x.Restauranteid,
                        principalTable: "Restaurante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    TipoCliente = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Puntaje = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__3214EC273891B22C", x => x.ID);
                    table.ForeignKey(
                        name: "FkClienteID",
                        column: x => x.ID,
                        principalTable: "Usuario",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Anular = table.Column<bool>(type: "bit", nullable: true),
                    modificar = table.Column<bool>(type: "bit", nullable: true),
                    ResID = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empleado__3214EC27C74BB42B", x => x.ID);
                    table.ForeignKey(
                        name: "FkEmpleadoID",
                        column: x => x.ID,
                        principalTable: "Usuario",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PermisosUsuarios",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PageAccess = table.Column<string>(type: "varchar(24)", unicode: false, maxLength: 24, nullable: false),
                    AcessTable = table.Column<bool>(type: "bit", nullable: true),
                    SeeList = table.Column<bool>(type: "bit", nullable: true),
                    InsertData = table.Column<bool>(type: "bit", nullable: true),
                    EditData = table.Column<bool>(type: "bit", nullable: true),
                    DeleteData = table.Column<bool>(type: "bit", nullable: true),
                    EditOwnData = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permisos__C6516914A82265F6", x => new { x.UserID, x.PageAccess });
                    table.ForeignKey(
                        name: "FK__PermisosU__UserI__403A8C7D",
                        column: x => x.UserID,
                        principalTable: "Usuario",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clienteid = table.Column<int>(type: "int", nullable: true),
                    RestauranteId = table.Column<int>(type: "int", nullable: true),
                    MesaId = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reserva__3214EC27991ABE55", x => x.ID);
                    table.ForeignKey(
                        name: "FkClienteIdReserva",
                        column: x => x.Clienteid,
                        principalTable: "Cliente",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FkMesaIdReserva",
                        column: x => x.MesaId,
                        principalTable: "Mesa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FkRestauranteIdReserva",
                        column: x => x.RestauranteId,
                        principalTable: "Restaurante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Orden",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    RestauranteId = table.Column<int>(type: "int", nullable: true),
                    DescCliente = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    DescTemperatura = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    DescClima = table.Column<decimal>(type: "numeric(20,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orden__3214EC2758C59AAC", x => x.ID);
                    table.ForeignKey(
                        name: "FkReservaIdOrden",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FkRestauranteIdOrdenDetalle",
                        column: x => x.RestauranteId,
                        principalTable: "Restaurante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaID = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<decimal>(type: "numeric(14,2)", nullable: true),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    MetodoPago = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pago__3214EC2774B61C1D", x => x.ID);
                    table.ForeignKey(
                        name: "FkReservaIdPago",
                        column: x => x.ReservaID,
                        principalTable: "Reserva",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Reseña",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Comentario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Puntaje = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reseña__3214EC27B3DC7B30", x => x.ID);
                    table.ForeignKey(
                        name: "FkClienteIdReseña",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FkReservaIdReseña",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrdenDetalle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenId = table.Column<int>(type: "int", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrdenDet__3214EC276B6D0E31", x => x.ID);
                    table.ForeignKey(
                        name: "FkMenuIdOrdenDetalle",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FkOrdenIdOrdenDetalle",
                        column: x => x.OrdenId,
                        principalTable: "Orden",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuRestaurante_IdRestaurante",
                table: "MenuRestaurante",
                column: "IdRestaurante");

            migrationBuilder.CreateIndex(
                name: "chkMesaRestaurante",
                table: "Mesa",
                columns: new[] { "NumeroMesa", "Restauranteid" },
                unique: true,
                filter: "[Restauranteid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Mesa_Restauranteid",
                table: "Mesa",
                column: "Restauranteid");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_ReservaId",
                table: "Orden",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_RestauranteId",
                table: "Orden",
                column: "RestauranteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDetalle_MenuId",
                table: "OrdenDetalle",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDetalle_OrdenId",
                table: "OrdenDetalle",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ReservaID",
                table: "Pago",
                column: "ReservaID");

            migrationBuilder.CreateIndex(
                name: "IX_Reseña_ClienteId",
                table: "Reseña",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reseña_ReservaId",
                table: "Reseña",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Clienteid",
                table: "Reserva",
                column: "Clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_MesaId",
                table: "Reserva",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_RestauranteId",
                table: "Reserva",
                column: "RestauranteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clima");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "FotoRestaurante");

            migrationBuilder.DropTable(
                name: "MenuRestaurante");

            migrationBuilder.DropTable(
                name: "OrdenDetalle");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "PermisosUsuarios");

            migrationBuilder.DropTable(
                name: "Reseña");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Orden");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Mesa");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Restaurante");
        }
    }
}
