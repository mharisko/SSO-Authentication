// <copyright file="20171117103311_Column_LoginTime_Refactoring_to_ConnectedTime.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Migrations.UnityAuthDb
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Column_LoginTime_Refactoring_to_ConnectedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginTime",
                table: "ServerUsages",
                newName: "ConnectedTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectedTime",
                table: "ServerUsages",
                newName: "LoginTime");
        }
    }
}
