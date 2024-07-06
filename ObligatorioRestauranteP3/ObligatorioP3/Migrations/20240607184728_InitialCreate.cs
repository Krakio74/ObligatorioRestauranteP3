using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioP3.Migrations
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
                    table.PrimaryKey("PK__Clima__3214EC27D9248E40", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rango = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePlato = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(14,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menu__3214EC27A8DA0DF5", x => x.ID);
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
                    table.PrimaryKey("PK__Restaura__3214EC27F8AC1771", x => x.ID);
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
                    Telefono = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: true),
                    Contraseña = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3214EC27A5486A8A", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FotoMenu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FotoMenu__C99ED230FEDE4F1F", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK__FotoMenu__MenuId__4AB81AF0",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FotoRestaurante",
                columns: table => new
                {
                    RestauranteId = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FotoRest__AAF3667B0EBE0B7E", x => x.RestauranteId);
                    table.ForeignKey(
                        name: "FK__FotoResta__Resta__45F365D3",
                        column: x => x.RestauranteId,
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
                    table.PrimaryKey("PK__Mesa__3214EC278A878A88", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Mesa__Restaurant__4E88ABD4",
                        column: x => x.Restauranteid,
                        principalTable: "Restaurante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    TipoCliente = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__3214EC27E645851C", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Cliente__ID__3C69FB99",
                        column: x => x.ID,
                        principalTable: "Usuario",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Rango = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empleado__3214EC27B248631C", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Empleado__ID__403A8C7D",
                        column: x => x.ID,
                        principalTable: "Usuario",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FotoUsuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FotoUsua__2B3DE7B874FE19B2", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK__FotoUsuar__Usuar__398D8EEE",
                        column: x => x.UsuarioId,
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
                    table.PrimaryKey("PK__Reserva__3214EC27B66A03C9", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Reserva__Cliente__5165187F",
                        column: x => x.Clienteid,
                        principalTable: "Cliente",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Reserva__MesaId__534D60F1",
                        column: x => x.MesaId,
                        principalTable: "Mesa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Reserva__Restaur__52593CB8",
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
                    Descuento = table.Column<decimal>(type: "numeric(20,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orden__3214EC273393CBBB", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Orden__ReservaId__5CD6CB2B",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
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
                    table.PrimaryKey("PK__Pago__3214EC27A9198358", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Pago__ReservaID__59FA5E80",
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
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reseña__3214EC2717D3C5C7", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Reseña__ClienteI__571DF1D5",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Reseña__ReservaI__5629CD9C",
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
                    RestauranteId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrdenDet__3214EC27607CAC7D", x => x.ID);
                    table.ForeignKey(
                        name: "FK__OrdenDeta__MenuI__60A75C0F",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__OrdenDeta__Orden__5FB337D6",
                        column: x => x.OrdenId,
                        principalTable: "Orden",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__OrdenDeta__Resta__619B8048",
                        column: x => x.RestauranteId,
                        principalTable: "Restaurante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mesa_Restauranteid",
                table: "Mesa",
                column: "Restauranteid");

            migrationBuilder.CreateIndex(
                name: "ukMesaRestaurante",
                table: "Mesa",
                columns: new[] { "NumeroMesa", "Restauranteid" },
                unique: true,
                filter: "[Restauranteid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_ReservaId",
                table: "Orden",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDetalle_MenuId",
                table: "OrdenDetalle",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDetalle_OrdenId",
                table: "OrdenDetalle",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDetalle_RestauranteId",
                table: "OrdenDetalle",
                column: "RestauranteId");

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
                name: "FotoMenu");

            migrationBuilder.DropTable(
                name: "FotoRestaurante");

            migrationBuilder.DropTable(
                name: "FotoUsuario");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "OrdenDetalle");

            migrationBuilder.DropTable(
                name: "Pago");

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
