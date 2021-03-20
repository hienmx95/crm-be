using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class Initialize_Schemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PER");

            migrationBuilder.EnsureSchema(
                name: "ENUM");

            migrationBuilder.EnsureSchema(
                name: "MDM");

            migrationBuilder.EnsureSchema(
                name: "OPP");

            migrationBuilder.EnsureSchema(
                name: "CUSTOMER");

            migrationBuilder.EnsureSchema(
                name: "ORDER");

            migrationBuilder.EnsureSchema(
                name: "WF");

            migrationBuilder.CreateTable(
                name: "F1_ResourceActionPageMapping",
                columns: table => new
                {
                    PageCode = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ResourceCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ActionCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_F1_ResourceActionPageMapping", x => x.PageCode);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalationPhone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAEscalationId = table.Column<long>(nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalationPhone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsQueueStatus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsQueueStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketGeneratedId",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketGeneratedId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityPriority",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityPriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallCategory",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: true, comment: "Mã quận huyện"),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsultingService",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultingService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CooperativeAttitude",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperativeAttitude", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFeedbackType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFeedbackType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadLevel",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadSource",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditedPriceStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditedPriceStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityReference",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityReference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false, comment: "Id"),
                    Code = table.Column<string>(maxLength: 50, nullable: false, comment: "Tên"),
                    Name = table.Column<string>(maxLength: 500, nullable: false, comment: "Tên")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfulenceLevelMarket",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfulenceLevelMarket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KMSStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KMSStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiCriteriaGeneral",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiCriteriaGeneral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiCriteriaItem",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiCriteriaItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiPeriod",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiYear",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketPrice",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityResultType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityResultType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderCategory",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPaymentStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderQuoteStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderQuoteStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PotentialResult",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotentialResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Probability",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Probability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipCustomerType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipCustomerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleStage",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleStage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sex",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SLAStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ColorCode = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SLATimeUnit",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLATimeUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialChannelType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialChannelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreDeliveryTime",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDeliveryTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreStatus",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketResolveType",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketResolveType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsedVariation",
                schema: "ENUM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedVariation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventMessage",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false),
                    RoutingKey = table.Column<string>(maxLength: 100, nullable: false),
                    RowId = table.Column<Guid>(nullable: false),
                    EntityName = table.Column<string>(maxLength: 500, nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false, comment: "Id"),
                    Name = table.Column<string>(maxLength: 4000, nullable: false, comment: "Tên"),
                    Url = table.Column<string>(maxLength: 4000, nullable: false, comment: "Đường dẫn Url"),
                    ThumbnailUrl = table.Column<string>(maxLength: 4000, nullable: true, comment: "Đường dẫn Url"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGrouping",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    Path = table.Column<string>(maxLength: 3000, nullable: false),
                    Level = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGrouping_ProductGrouping",
                        column: x => x.ParentId,
                        principalSchema: "MDM",
                        principalTable: "ProductGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldType",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 3000, nullable: false),
                    Path = table.Column<string>(maxLength: 3000, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Path = table.Column<string>(maxLength: 400, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestState",
                schema: "WF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowState",
                schema: "WF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowType",
                schema: "WF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallEmotion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallEmotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallEmotion_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CallType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ColorCode = table.Column<string>(maxLength: 20, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallType_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeGroup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    StatusId = table.Column<long>(nullable: true),
                    DisplayOrder = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeGroup_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MailTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    StatusId = table.Column<long>(nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailTemplate_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmsTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 2000, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    StatusId = table.Column<long>(nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsTemplate_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketPriority",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    OrderNumber = table.Column<long>(nullable: true),
                    ColorCode = table.Column<string>(maxLength: 20, nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPriority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketPriority_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketSource",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderNumber = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSource_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ColorCode = table.Column<string>(maxLength: 20, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketType_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brand_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGrouping",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    CustomerTypeId = table.Column<long>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Path = table.Column<string>(maxLength: 400, nullable: false),
                    Level = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGrouping_CustomerType",
                        column: x => x.CustomerTypeId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerGrouping_CustomerGrouping",
                        column: x => x.ParentId,
                        principalSchema: "MDM",
                        principalTable: "CustomerGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerGrouping_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLevel",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Color = table.Column<string>(maxLength: 500, nullable: true),
                    PointFrom = table.Column<long>(nullable: false),
                    PointTo = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLevel_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerResource",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerResource_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nation",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false, comment: "Mã quận huyện"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Priority = table.Column<long>(nullable: true),
                    StatusId = table.Column<long>(nullable: false, comment: "Trạng thái hoạt động"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nation_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Path = table.Column<string>(maxLength: 400, nullable: false),
                    Level = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Organization",
                        column: x => x.ParentId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organization_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneType",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneType_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductType_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profession",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profession_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Priority = table.Column<long>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Province_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreGrouping",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Path = table.Column<string>(maxLength: 400, nullable: false),
                    Level = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreGrouping_StoreGrouping",
                        column: x => x.ParentId,
                        principalSchema: "MDM",
                        principalTable: "StoreGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreGrouping_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreType",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    ColorId = table.Column<long>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreType_Color",
                        column: x => x.ColorId,
                        principalSchema: "ENUM",
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreType_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxType_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatus",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderNumber = table.Column<long>(nullable: false),
                    ColorCode = table.Column<string>(maxLength: 20, nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketStatus_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasure_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Level = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    ImageId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Image",
                        column: x => x.ImageId,
                        principalSchema: "MDM",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Category",
                        column: x => x.ParentId,
                        principalSchema: "MDM",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionOperator",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    FieldTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionOperator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionOperator_FieldType",
                        column: x => x.FieldTypeId,
                        principalSchema: "PER",
                        principalTable: "FieldType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Action",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Action_Menu",
                        column: x => x.MenuId,
                        principalSchema: "PER",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    FieldTypeId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_FieldType",
                        column: x => x.FieldTypeId,
                        principalSchema: "PER",
                        principalTable: "FieldType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionField_Menu",
                        column: x => x.MenuId,
                        principalSchema: "PER",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketGroup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderNumber = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    TicketTypeId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketGroup_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketGroup_TicketType",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    OrganizationId = table.Column<long>(nullable: true),
                    NotificationStatusId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationStatus",
                        column: x => x.NotificationStatusId,
                        principalSchema: "ENUM",
                        principalTable: "NotificationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false, comment: "Id"),
                    Username = table.Column<string>(maxLength: 500, nullable: false, comment: "Tên đăng nhập"),
                    DisplayName = table.Column<string>(maxLength: 500, nullable: true, comment: "Tên hiển thị"),
                    Address = table.Column<string>(maxLength: 500, nullable: true, comment: "Địa chỉ nhà"),
                    Email = table.Column<string>(maxLength: 500, nullable: true, comment: "Địa chỉ email"),
                    Phone = table.Column<string>(maxLength: 500, nullable: true, comment: "Số điện thoại liên hệ"),
                    SexId = table.Column<long>(nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Avatar = table.Column<string>(maxLength: 4000, nullable: true, comment: "Ảnh đại diện"),
                    Department = table.Column<string>(maxLength: 500, nullable: true, comment: "Phòng ban"),
                    OrganizationId = table.Column<long>(nullable: false, comment: "Đơn vị công tác"),
                    Longitude = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    StatusId = table.Column<long>(nullable: false, comment: "Trạng thái"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    RowId = table.Column<Guid>(nullable: false, comment: "Trường để đồng bộ"),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUser_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUser_Sex",
                        column: x => x.SexId,
                        principalSchema: "ENUM",
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUser_UserStatus",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "District",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false, comment: "Id"),
                    Code = table.Column<string>(maxLength: 500, nullable: true, comment: "Mã quận huyện"),
                    Name = table.Column<string>(maxLength: 500, nullable: false, comment: "Tên quận huyện"),
                    Priority = table.Column<long>(nullable: true, comment: "Thứ tự ưu tiên"),
                    ProvinceId = table.Column<long>(nullable: false, comment: "Tỉnh phụ thuộc"),
                    StatusId = table.Column<long>(nullable: false, comment: "Trạng thái hoạt động"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    RowId = table.Column<Guid>(nullable: false, comment: "Trường để đồng bộ"),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_District_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasureGrouping",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasureGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasureGrouping_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasureGrouping_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Menu",
                        column: x => x.MenuId,
                        principalSchema: "PER",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_Role",
                        column: x => x.RoleId,
                        principalSchema: "PER",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionPageMapping",
                schema: "PER",
                columns: table => new
                {
                    ActionId = table.Column<long>(nullable: false),
                    PageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPageMapping", x => new { x.ActionId, x.PageId });
                    table.ForeignKey(
                        name: "FK_ActionPageMapping_Action",
                        column: x => x.ActionId,
                        principalSchema: "PER",
                        principalTable: "Action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionPageMapping_Page",
                        column: x => x.PageId,
                        principalSchema: "PER",
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketIssueLevel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderNumber = table.Column<long>(nullable: false),
                    TicketGroupId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    SLA = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketIssueLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketIssueLevel_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketIssueLevel_TicketGroup",
                        column: x => x.TicketGroupId,
                        principalTable: "TicketGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<long>(nullable: true),
                    Property = table.Column<string>(maxLength: 255, nullable: false),
                    OldValue = table.Column<string>(maxLength: 4000, nullable: false),
                    NewValue = table.Column<string>(maxLength: 4000, nullable: false),
                    ClassName = table.Column<string>(maxLength: 255, nullable: true),
                    ActionName = table.Column<string>(maxLength: 255, nullable: true),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogProperty_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CallLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Content = table.Column<string>(maxLength: 4000, nullable: false),
                    Phone = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false),
                    CallTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EntityReferenceId = table.Column<long>(nullable: false),
                    EntityId = table.Column<long>(nullable: false),
                    CallTypeId = table.Column<long>(nullable: false),
                    CallCategoryId = table.Column<long>(nullable: true),
                    CallEmotionId = table.Column<long>(nullable: true),
                    CallStatusId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallLog_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallLog_CallCategory",
                        column: x => x.CallCategoryId,
                        principalSchema: "ENUM",
                        principalTable: "CallCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallLog_CallEmotion",
                        column: x => x.CallEmotionId,
                        principalTable: "CallEmotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallLog_CallStatus",
                        column: x => x.CallStatusId,
                        principalSchema: "ENUM",
                        principalTable: "CallStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallLog_CallType",
                        column: x => x.CallTypeId,
                        principalTable: "CallType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallLog_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallLog_CallLogEntityType",
                        column: x => x.EntityReferenceId,
                        principalSchema: "ENUM",
                        principalTable: "EntityReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false, comment: "Id"),
                    Name = table.Column<string>(maxLength: 4000, nullable: false, comment: "Tên"),
                    Url = table.Column<string>(maxLength: 4000, nullable: false, comment: "Đường dẫn Url"),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiGeneral",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    KpiYearId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiGeneral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiGeneral_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiGeneral_AppUser",
                        column: x => x.EmployeeId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiGeneral_KpiYear",
                        column: x => x.KpiYearId,
                        principalSchema: "ENUM",
                        principalTable: "KpiYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiGeneral_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiGeneral_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationId = table.Column<long>(nullable: false),
                    KpiYearId = table.Column<long>(nullable: false),
                    KpiPeriodId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiItem_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItem_AppUser",
                        column: x => x.EmployeeId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItem_KpiPeriod",
                        column: x => x.KpiPeriodId,
                        principalSchema: "ENUM",
                        principalTable: "KpiPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItem_KpiYear",
                        column: x => x.KpiYearId,
                        principalSchema: "ENUM",
                        principalTable: "KpiYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItem_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItem_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ManagerId = table.Column<long>(nullable: true),
                    SalerId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: true),
                    StatusId = table.Column<long>(nullable: true),
                    RecurDays = table.Column<DateTime>(type: "datetime", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NoEndDate = table.Column<bool>(nullable: true),
                    StartDayOfWeek = table.Column<DateTime>(type: "datetime", nullable: true),
                    DisplayOrder = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleMaster_AppUser2",
                        column: x => x.ManagerId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleMaster_AppUser3",
                        column: x => x.SalerId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleMaster_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmsQueue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SmsCode = table.Column<string>(nullable: true),
                    SmsTitle = table.Column<string>(nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SmsContent = table.Column<string>(nullable: true),
                    SentByAppUserId = table.Column<long>(nullable: true),
                    SmsQueueStatusId = table.Column<long>(nullable: true),
                    EntityReferenceId = table.Column<long>(nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsQueue_SmsQueueReference",
                        column: x => x.EntityReferenceId,
                        principalSchema: "ENUM",
                        principalTable: "EntityReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmsQueue_AppUser",
                        column: x => x.SentByAppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmsQueue_SmsQueueStatus",
                        column: x => x.SmsQueueStatusId,
                        principalTable: "SmsQueueStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoleMapping",
                schema: "MDM",
                columns: table => new
                {
                    AppUserId = table.Column<long>(nullable: false, comment: "Id nhân viên"),
                    RoleId = table.Column<long>(nullable: false, comment: "Id nhóm quyền")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMapping", x => new { x.AppUserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRoleMapping_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserRoleMapping_Role",
                        column: x => x.RoleId,
                        principalSchema: "PER",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDefinition",
                schema: "WF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    ModifierId = table.Column<long>(nullable: true),
                    WorkflowTypeId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowDefinition_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowDefinition_AppUser1",
                        column: x => x.ModifierId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowDefinition_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowDefinition_WorkflowType",
                        column: x => x.WorkflowTypeId,
                        principalSchema: "WF",
                        principalTable: "WorkflowType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ward",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Priority = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ward_District",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ward_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 3000, nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    ScanCode = table.Column<string>(maxLength: 500, nullable: true),
                    ERPCode = table.Column<string>(maxLength: 500, nullable: true),
                    CategoryId = table.Column<long>(nullable: false),
                    ProductTypeId = table.Column<long>(nullable: false),
                    BrandId = table.Column<long>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureGroupingId = table.Column<long>(nullable: true),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    RetailPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxTypeId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    OtherName = table.Column<string>(maxLength: 1000, nullable: true),
                    TechnicalName = table.Column<string>(maxLength: 1000, nullable: true),
                    Note = table.Column<string>(maxLength: 3000, nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    UsedVariationId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand",
                        column: x => x.BrandId,
                        principalSchema: "MDM",
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalSchema: "MDM",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_ProductType",
                        column: x => x.ProductTypeId,
                        principalSchema: "MDM",
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_TaxType",
                        column: x => x.TaxTypeId,
                        principalSchema: "MDM",
                        principalTable: "TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_UnitOfMeasureGrouping",
                        column: x => x.UnitOfMeasureGroupingId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasureGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_UsedVariation",
                        column: x => x.UsedVariationId,
                        principalSchema: "ENUM",
                        principalTable: "UsedVariation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasureGroupingContent",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    UnitOfMeasureGroupingId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Factor = table.Column<long>(nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasureGroupingContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasureGroupingContent_UnitOfMeasureGrouping",
                        column: x => x.UnitOfMeasureGroupingId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasureGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasureGroupingContent_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionActionMapping",
                schema: "PER",
                columns: table => new
                {
                    ActionId = table.Column<long>(nullable: false),
                    PermissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPermissionMapping", x => new { x.ActionId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ActionPermissionMapping_Action",
                        column: x => x.ActionId,
                        principalSchema: "PER",
                        principalTable: "Action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionPermissionMapping_Permission",
                        column: x => x.PermissionId,
                        principalSchema: "PER",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionContent",
                schema: "PER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PermissionId = table.Column<long>(nullable: false),
                    FieldId = table.Column<long>(nullable: false),
                    PermissionOperatorId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionContent_Field",
                        column: x => x.FieldId,
                        principalSchema: "PER",
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionContent_Permission",
                        column: x => x.PermissionId,
                        principalSchema: "PER",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionContent_PermissionOperator",
                        column: x => x.PermissionOperatorId,
                        principalSchema: "PER",
                        principalTable: "PermissionOperator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlert",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketIssueLevelId = table.Column<long>(nullable: true),
                    IsNotification = table.Column<bool>(nullable: true),
                    IsMail = table.Column<bool>(nullable: true),
                    IsSMS = table.Column<bool>(nullable: true),
                    Time = table.Column<long>(nullable: true),
                    TimeUnitId = table.Column<long>(nullable: true),
                    IsAssignedToUser = table.Column<bool>(nullable: true),
                    IsAssignedToGroup = table.Column<bool>(nullable: true),
                    SmsTemplateId = table.Column<long>(nullable: true),
                    MailTemplateId = table.Column<long>(nullable: true),
                    RowId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlert_MailTemplate",
                        column: x => x.MailTemplateId,
                        principalTable: "MailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlert_SmsTemplate",
                        column: x => x.SmsTemplateId,
                        principalTable: "SmsTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlert_TicketIssueLevel",
                        column: x => x.TicketIssueLevelId,
                        principalTable: "TicketIssueLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlert_SLATimeUnit",
                        column: x => x.TimeUnitId,
                        principalSchema: "ENUM",
                        principalTable: "SLATimeUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlertFRT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketIssueLevelId = table.Column<long>(nullable: true),
                    IsNotification = table.Column<bool>(nullable: true),
                    IsMail = table.Column<bool>(nullable: true),
                    IsSMS = table.Column<bool>(nullable: true),
                    Time = table.Column<long>(nullable: true),
                    TimeUnitId = table.Column<long>(nullable: true),
                    IsAssignedToUser = table.Column<bool>(nullable: true),
                    IsAssignedToGroup = table.Column<bool>(nullable: true),
                    SmsTemplateId = table.Column<long>(nullable: true),
                    MailTemplateId = table.Column<long>(nullable: true),
                    RowId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlertFRT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRT_MailTemplate",
                        column: x => x.MailTemplateId,
                        principalTable: "MailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRT_SmsTemplate",
                        column: x => x.SmsTemplateId,
                        principalTable: "SmsTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRT_TicketIssueLevel",
                        column: x => x.TicketIssueLevelId,
                        principalTable: "TicketIssueLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRT_SLATimeUnit",
                        column: x => x.TimeUnitId,
                        principalSchema: "ENUM",
                        principalTable: "SLATimeUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketIssueLevelId = table.Column<long>(nullable: true),
                    IsNotification = table.Column<bool>(nullable: true),
                    IsMail = table.Column<bool>(nullable: true),
                    IsSMS = table.Column<bool>(nullable: true),
                    Time = table.Column<long>(nullable: true),
                    TimeUnitId = table.Column<long>(nullable: true),
                    IsAssignedToUser = table.Column<bool>(nullable: true),
                    IsAssignedToGroup = table.Column<bool>(nullable: true),
                    SmsTemplateId = table.Column<long>(nullable: true),
                    MailTemplateId = table.Column<long>(nullable: true),
                    RowId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAEscalation_MailTemplate",
                        column: x => x.MailTemplateId,
                        principalTable: "MailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalation_SmsTemplate",
                        column: x => x.SmsTemplateId,
                        principalTable: "SmsTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalation_TicketIssueLevel",
                        column: x => x.TicketIssueLevelId,
                        principalTable: "TicketIssueLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalation_SLATimeUnit",
                        column: x => x.TimeUnitId,
                        principalSchema: "ENUM",
                        principalTable: "SLATimeUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalationFRT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketIssueLevelId = table.Column<long>(nullable: true),
                    IsNotification = table.Column<bool>(nullable: true),
                    IsMail = table.Column<bool>(nullable: true),
                    IsSMS = table.Column<bool>(nullable: true),
                    Time = table.Column<long>(nullable: true),
                    TimeUnitId = table.Column<long>(nullable: true),
                    IsAssignedToUser = table.Column<bool>(nullable: true),
                    IsAssignedToGroup = table.Column<bool>(nullable: true),
                    SmsTemplateId = table.Column<long>(nullable: true),
                    MailTemplateId = table.Column<long>(nullable: true),
                    RowId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalationFRT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRT_MailTemplate",
                        column: x => x.MailTemplateId,
                        principalTable: "MailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRT_SmsTemplate",
                        column: x => x.SmsTemplateId,
                        principalTable: "SmsTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRT_TicketIssueLevel",
                        column: x => x.TicketIssueLevelId,
                        principalTable: "TicketIssueLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRT_SLATimeUnit",
                        column: x => x.TimeUnitId,
                        principalSchema: "ENUM",
                        principalTable: "SLATimeUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAPolicy",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketIssueLevelId = table.Column<long>(nullable: true),
                    TicketPriorityId = table.Column<long>(nullable: true),
                    FirstResponseTime = table.Column<long>(nullable: true),
                    FirstResponseUnitId = table.Column<long>(nullable: true),
                    ResolveTime = table.Column<long>(nullable: true),
                    ResolveUnitId = table.Column<long>(nullable: true),
                    IsAlert = table.Column<bool>(nullable: true),
                    IsAlertFRT = table.Column<bool>(nullable: true),
                    IsEscalation = table.Column<bool>(nullable: true),
                    IsEscalationFRT = table.Column<bool>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAPolicy_SLATimeUnit",
                        column: x => x.FirstResponseUnitId,
                        principalSchema: "ENUM",
                        principalTable: "SLATimeUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAPolicy_SLATimeUnit1",
                        column: x => x.ResolveUnitId,
                        principalSchema: "ENUM",
                        principalTable: "SLATimeUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAPolicy_TicketIssueLevel",
                        column: x => x.TicketIssueLevelId,
                        principalTable: "TicketIssueLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAPolicy_TicketPriority",
                        column: x => x.TicketPriorityId,
                        principalTable: "TicketPriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiGeneralContent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KpiGeneralId = table.Column<long>(nullable: false),
                    KpiCriteriaGeneralId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiGeneralContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiGeneralContent_KpiCriteriaGeneral",
                        column: x => x.KpiCriteriaGeneralId,
                        principalSchema: "ENUM",
                        principalTable: "KpiCriteriaGeneral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiGeneralContent_KpiGeneral",
                        column: x => x.KpiGeneralId,
                        principalTable: "KpiGeneral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiGeneralContent_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestWorkflowDefinitionMapping",
                schema: "WF",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(nullable: false),
                    WorkflowDefinitionId = table.Column<long>(nullable: false),
                    RequestStateId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestWorkflowDefinitionMapping", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_RequestWorkflowDefinitionMapping_RequestState",
                        column: x => x.RequestStateId,
                        principalSchema: "WF",
                        principalTable: "RequestState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestWorkflowDefinitionMapping_WorkflowDefinition",
                        column: x => x.WorkflowDefinitionId,
                        principalSchema: "WF",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowParameter",
                schema: "WF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkflowDefinitionId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowParameter_WorkflowDefinition",
                        column: x => x.WorkflowDefinitionId,
                        principalSchema: "WF",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStep",
                schema: "WF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkflowDefinitionId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    SubjectMailForReject = table.Column<string>(maxLength: 4000, nullable: true),
                    BodyMailForReject = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowStep_Role",
                        column: x => x.RoleId,
                        principalSchema: "PER",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowStep_WorkflowDefinition",
                        column: x => x.WorkflowDefinitionId,
                        principalSchema: "WF",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    TaxCode = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 2000, nullable: true),
                    NationId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    WardId = table.Column<long>(nullable: true),
                    OwnerName = table.Column<string>(maxLength: 50, nullable: true),
                    PersonInChargeId = table.Column<long>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_District",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_Nation",
                        column: x => x.NationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_AppUser",
                        column: x => x.PersonInChargeId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_Ward",
                        column: x => x.WardId,
                        principalSchema: "MDM",
                        principalTable: "Ward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    OrganizationId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    WardId = table.Column<long>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_District",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_Ward",
                        column: x => x.WardId,
                        principalSchema: "MDM",
                        principalTable: "Ward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 4000, nullable: false),
                    Name = table.Column<string>(maxLength: 4000, nullable: false),
                    ScanCode = table.Column<string>(maxLength: 4000, nullable: true),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    RetailPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Product",
                        column: x => x.ProductId,
                        principalSchema: "MDM",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImageMapping",
                schema: "MDM",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false),
                    ImageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageMapping", x => new { x.ProductId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_ProductImageMapping_Image",
                        column: x => x.ImageId,
                        principalSchema: "MDM",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImageMapping_Product",
                        column: x => x.ProductId,
                        principalSchema: "MDM",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductGroupingMapping",
                schema: "MDM",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false),
                    ProductGroupingId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductGroupingMapping", x => new { x.ProductId, x.ProductGroupingId });
                    table.ForeignKey(
                        name: "FK_ProductProductGroupingMapping_ProductGrouping",
                        column: x => x.ProductGroupingId,
                        principalSchema: "MDM",
                        principalTable: "ProductGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductGroupingMapping_Product",
                        column: x => x.ProductId,
                        principalSchema: "MDM",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VariationGrouping",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariationGrouping_Product",
                        column: x => x.ProductId,
                        principalSchema: "MDM",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlertMail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAAlertId = table.Column<long>(nullable: true),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlertMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlertMail_SLAAlert",
                        column: x => x.SLAAlertId,
                        principalTable: "SLAAlert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlertPhone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAAlertId = table.Column<long>(nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlertPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlertPhone_SLAAlert",
                        column: x => x.SLAAlertId,
                        principalTable: "SLAAlert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlertUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAAlertId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlertUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlertUser_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlertUser_SLAAlert",
                        column: x => x.SLAAlertId,
                        principalTable: "SLAAlert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlertFRTMail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAAlertFRTId = table.Column<long>(nullable: true),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlertFRTMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRTMail_SLAAlertFRT",
                        column: x => x.SLAAlertFRTId,
                        principalTable: "SLAAlertFRT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlertFRTPhone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAAlertFRTId = table.Column<long>(nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlertFRTPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRTPhone_SLAAlertFRT",
                        column: x => x.SLAAlertFRTId,
                        principalTable: "SLAAlertFRT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAAlertFRTUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAAlertFRTId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAAlertFRTUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRTUser_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAAlertFRTUser_SLAAlertFRT",
                        column: x => x.SLAAlertFRTId,
                        principalTable: "SLAAlertFRT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalationMail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAEscalationId = table.Column<long>(nullable: true),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalationMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAEscalationMail_SLAEscalation",
                        column: x => x.SLAEscalationId,
                        principalTable: "SLAEscalation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalationUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAEscalationId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAEscalationUser_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalationUser_SLAEscalation",
                        column: x => x.SLAEscalationId,
                        principalTable: "SLAEscalation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalationFRTMail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAEscalationFRTId = table.Column<long>(nullable: true),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalationFRTMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRTMail_SLAEscalationFRT",
                        column: x => x.SLAEscalationFRTId,
                        principalTable: "SLAEscalationFRT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalationFRTPhone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAEscalationFRTId = table.Column<long>(nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalationFRTPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRTPhone_SLAEscalationFRT",
                        column: x => x.SLAEscalationFRTId,
                        principalTable: "SLAEscalationFRT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLAEscalationFRTUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SLAEscalationFRTId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAEscalationFRTUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRTUser_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SLAEscalationFRTUser_SLAEscalationFRT",
                        column: x => x.SLAEscalationFRTId,
                        principalTable: "SLAEscalationFRT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiGeneralContentKpiPeriodMapping",
                columns: table => new
                {
                    KpiGeneralContentId = table.Column<long>(nullable: false),
                    KpiPeriodId = table.Column<long>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18, 4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiGeneralContentKpiPeriodMapping", x => new { x.KpiGeneralContentId, x.KpiPeriodId });
                    table.ForeignKey(
                        name: "FK_KpiGeneralContentKpiPeriodMapping_KpiGeneralContent",
                        column: x => x.KpiGeneralContentId,
                        principalTable: "KpiGeneralContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiGeneralContentKpiPeriodMapping_KpiPeriod",
                        column: x => x.KpiPeriodId,
                        principalSchema: "ENUM",
                        principalTable: "KpiPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestWorkflowParameterMapping",
                schema: "WF",
                columns: table => new
                {
                    WorkflowParameterId = table.Column<long>(nullable: false),
                    RequestId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 500, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestWorkflowParameterMapping", x => new { x.WorkflowParameterId, x.RequestId });
                    table.ForeignKey(
                        name: "FK_StoreWorkflowParameterMapping_WorkflowParameter",
                        column: x => x.WorkflowParameterId,
                        principalSchema: "WF",
                        principalTable: "WorkflowParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestWorkflowStepMapping",
                schema: "WF",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(nullable: false),
                    WorkflowStepId = table.Column<long>(nullable: false),
                    WorkflowStateId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestWorkflowStepMapping", x => new { x.RequestId, x.WorkflowStepId });
                    table.ForeignKey(
                        name: "FK_StoreWorkflow_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreWorkflow_WorkflowState",
                        column: x => x.WorkflowStateId,
                        principalSchema: "WF",
                        principalTable: "WorkflowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreWorkflow_WorkflowStep",
                        column: x => x.WorkflowStepId,
                        principalSchema: "WF",
                        principalTable: "WorkflowStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDirection",
                schema: "WF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkflowDefinitionId = table.Column<long>(nullable: false),
                    FromStepId = table.Column<long>(nullable: false),
                    ToStepId = table.Column<long>(nullable: false),
                    SubjectMailForCreator = table.Column<string>(maxLength: 500, nullable: true),
                    SubjectMailForNextStep = table.Column<string>(maxLength: 500, nullable: true),
                    BodyMailForCreator = table.Column<string>(maxLength: 4000, nullable: true),
                    BodyMailForNextStep = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDirection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowDirection_WorkflowStep",
                        column: x => x.FromStepId,
                        principalSchema: "WF",
                        principalTable: "WorkflowStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowDirection_WorkflowStep1",
                        column: x => x.ToStepId,
                        principalSchema: "WF",
                        principalTable: "WorkflowStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowDirection_WorkflowDefinition",
                        column: x => x.WorkflowDefinitionId,
                        principalSchema: "WF",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeArticle",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 2000, nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    KMSStatusId = table.Column<long>(nullable: true),
                    GroupId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: true),
                    DisplayOrder = table.Column<long>(nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeArticle_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeArticle_KnowledgeGroup",
                        column: x => x.GroupId,
                        principalTable: "KnowledgeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeArticle_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeArticle_KMSStatus",
                        column: x => x.KMSStatusId,
                        principalSchema: "ENUM",
                        principalTable: "KMSStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeArticle_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiItemContent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KpiItemId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiItemContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiItemContent_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItemContent_KpiItem",
                        column: x => x.KpiItemId,
                        principalTable: "KpiItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WarehouseId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    SaleStock = table.Column<long>(nullable: false),
                    AccountingStock = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_Warehouse",
                        column: x => x.WarehouseId,
                        principalSchema: "MDM",
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemImageMapping",
                schema: "MDM",
                columns: table => new
                {
                    ItemId = table.Column<long>(nullable: false),
                    ImageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImageMapping", x => new { x.ItemId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_ItemImageMapping_Image",
                        column: x => x.ImageId,
                        principalSchema: "MDM",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemImageMapping_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Variation",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    VariationGroupingId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variation_VariationGrouping",
                        column: x => x.VariationGroupingId,
                        principalSchema: "MDM",
                        principalTable: "VariationGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeArticleKeyword",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    KnowledgeArticleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeArticleKeyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeArticleKeyword_KnowledgeArticle",
                        column: x => x.KnowledgeArticleId,
                        principalTable: "KnowledgeArticle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeArticleOrganizationMapping",
                columns: table => new
                {
                    KnowledgeArticleId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeArticleOrganizationMapping", x => new { x.KnowledgeArticleId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_KnowledgeArticleOrganizationMapping_KnowledgeArticle",
                        column: x => x.KnowledgeArticleId,
                        principalTable: "KnowledgeArticle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeArticleOrganizationMapping_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiItemContentKpiCriteriaItemMapping",
                columns: table => new
                {
                    KpiItemContentId = table.Column<long>(nullable: false),
                    KpiCriteriaItemId = table.Column<long>(nullable: false),
                    Value = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiItemContentKpiCriteriaItemMapping", x => new { x.KpiItemContentId, x.KpiCriteriaItemId });
                    table.ForeignKey(
                        name: "FK_KpiItemContentKpiCriteriaItemMapping_KpiCriteriaItem",
                        column: x => x.KpiCriteriaItemId,
                        principalSchema: "ENUM",
                        principalTable: "KpiCriteriaItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItemContentKpiCriteriaItemMapping_KpiItemContent",
                        column: x => x.KpiItemContentId,
                        principalTable: "KpiItemContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryHistory",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InventoryId = table.Column<long>(nullable: false),
                    OldSaleStock = table.Column<long>(nullable: false),
                    SaleStock = table.Column<long>(nullable: false),
                    OldAccountingStock = table.Column<long>(nullable: false),
                    AccountingStock = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryHistory_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryHistory_Inventory",
                        column: x => x.InventoryId,
                        principalSchema: "MDM",
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerTypeId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ProcessDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Subject = table.Column<string>(maxLength: 500, nullable: false),
                    Content = table.Column<string>(maxLength: 4000, nullable: false),
                    TicketIssueLevelId = table.Column<long>(nullable: false),
                    TicketPriorityId = table.Column<long>(nullable: false),
                    TicketStatusId = table.Column<long>(nullable: false),
                    TicketSourceId = table.Column<long>(nullable: false),
                    TicketNumber = table.Column<string>(maxLength: 50, nullable: false),
                    DepartmentId = table.Column<long>(nullable: true),
                    RelatedTicketId = table.Column<long>(nullable: true),
                    SLA = table.Column<long>(nullable: false),
                    RelatedCallLogId = table.Column<long>(nullable: true),
                    ResponseMethodId = table.Column<long>(nullable: true),
                    EntityReferenceId = table.Column<long>(nullable: true),
                    TicketResolveTypeId = table.Column<long>(nullable: true),
                    ResolveContent = table.Column<string>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    IsAlerted = table.Column<bool>(nullable: true),
                    IsAlertedFRT = table.Column<bool>(nullable: true),
                    IsEscalated = table.Column<bool>(nullable: true),
                    IsEscalatedFRT = table.Column<bool>(nullable: true),
                    closedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    AppUserClosedId = table.Column<long>(nullable: true),
                    FirstResponseAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastResponseAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastHoldingAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReraisedTimes = table.Column<long>(nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    AppUserResolvedId = table.Column<long>(nullable: true),
                    IsClose = table.Column<bool>(nullable: true),
                    IsOpen = table.Column<bool>(nullable: true),
                    IsWait = table.Column<bool>(nullable: true),
                    IsWork = table.Column<bool>(nullable: true),
                    SLAPolicyId = table.Column<long>(nullable: true),
                    HoldingTime = table.Column<long>(nullable: true),
                    FirstResponeTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResolveTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    FirstRespTimeRemaining = table.Column<long>(nullable: true),
                    ResolveTimeRemaining = table.Column<long>(nullable: true),
                    SLAStatusId = table.Column<long>(nullable: true),
                    ResolveMinute = table.Column<long>(nullable: true),
                    SLAOverTime = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    Phone = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_AppUser2",
                        column: x => x.AppUserClosedId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_AppUser3",
                        column: x => x.AppUserResolvedId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_CustomerType",
                        column: x => x.CustomerTypeId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Organization",
                        column: x => x.DepartmentId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketReference",
                        column: x => x.EntityReferenceId,
                        principalSchema: "ENUM",
                        principalTable: "EntityReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Product",
                        column: x => x.ProductId,
                        principalSchema: "MDM",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_CallLog",
                        column: x => x.RelatedCallLogId,
                        principalTable: "CallLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Ticket",
                        column: x => x.RelatedTicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_SLAPolicy",
                        column: x => x.SLAPolicyId,
                        principalTable: "SLAPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_SLAStatus",
                        column: x => x.SLAStatusId,
                        principalSchema: "ENUM",
                        principalTable: "SLAStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketIssueLevel",
                        column: x => x.TicketIssueLevelId,
                        principalTable: "TicketIssueLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketPriority",
                        column: x => x.TicketPriorityId,
                        principalTable: "TicketPriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketResolveType",
                        column: x => x.TicketResolveTypeId,
                        principalSchema: "ENUM",
                        principalTable: "TicketResolveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketSource",
                        column: x => x.TicketSourceId,
                        principalTable: "TicketSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketStatus",
                        column: x => x.TicketStatusId,
                        principalSchema: "MDM",
                        principalTable: "TicketStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_AppUser",
                        column: x => x.UserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketOfDepartment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(maxLength: 4000, nullable: true),
                    DepartmentId = table.Column<long>(nullable: false),
                    TicketId = table.Column<long>(nullable: false),
                    TicketStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOfDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketOfDepartment_Organization",
                        column: x => x.DepartmentId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketOfDepartment_Ticket",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketOfDepartment_TicketStatus",
                        column: x => x.TicketStatusId,
                        principalSchema: "MDM",
                        principalTable: "TicketStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketOfUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(maxLength: 4000, nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    TicketId = table.Column<long>(nullable: false),
                    TicketStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOfUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketOfUser_Ticket",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketOfUser_TicketStatus",
                        column: x => x.TicketStatusId,
                        principalSchema: "MDM",
                        principalTable: "TicketStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketOfUser_AppUser",
                        column: x => x.UserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCallLogMapping",
                schema: "CUSTOMER",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false),
                    CallLogId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCallLogMapping", x => new { x.CustomerId, x.CallLogId });
                    table.ForeignKey(
                        name: "FK_CustomerCallLogMapping_CallLog",
                        column: x => x.CallLogId,
                        principalTable: "CallLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCallLogMapping",
                schema: "OPP",
                columns: table => new
                {
                    CompanyId = table.Column<long>(nullable: false),
                    CallLogId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCallLogMapping", x => new { x.CompanyId, x.CallLogId });
                    table.ForeignKey(
                        name: "FK_CompanyCallLogMapping_CallLog",
                        column: x => x.CallLogId,
                        principalTable: "CallLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactCallLogMapping",
                schema: "OPP",
                columns: table => new
                {
                    ContactId = table.Column<long>(nullable: false),
                    CallLogId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCallLogMapping", x => new { x.ContactId, x.CallLogId });
                    table.ForeignKey(
                        name: "FK_ContactCallLogMapping_CallLog",
                        column: x => x.CallLogId,
                        principalTable: "CallLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadCallLogMapping",
                schema: "OPP",
                columns: table => new
                {
                    CustomerLeadId = table.Column<long>(nullable: false),
                    CallLogId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadCallLogMapping", x => new { x.CustomerLeadId, x.CallLogId });
                    table.ForeignKey(
                        name: "FK_CustomerLeadCallLogMapping_CallLog",
                        column: x => x.CallLogId,
                        principalTable: "CallLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityCallLogMapping",
                schema: "OPP",
                columns: table => new
                {
                    OpportunityId = table.Column<long>(nullable: false),
                    CallLogId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityCallLogMapping", x => new { x.OpportunityId, x.CallLogId });
                    table.ForeignKey(
                        name: "FK_OpportunityCallLogMapping_CallLog",
                        column: x => x.CallLogId,
                        principalTable: "CallLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractContactMapping",
                columns: table => new
                {
                    ContractId = table.Column<long>(nullable: false),
                    ContactId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractContactMapping", x => new { x.ContractId, x.ContactId });
                });

            migrationBuilder.CreateTable(
                name: "ContractFileGrouping",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    ContractId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    FileTypeId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractFileGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractFileGrouping_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractFileGrouping_FileType",
                        column: x => x.FileTypeId,
                        principalSchema: "ENUM",
                        principalTable: "FileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractFileMapping",
                columns: table => new
                {
                    ContractFileGroupingId = table.Column<long>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractFileMapping", x => new { x.ContractFileGroupingId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ContractFileMapping_ContractFileGrouping",
                        column: x => x.ContractFileGroupingId,
                        principalTable: "ContractFileGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractFileMapping_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractItemDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    PrimaryUnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    RequestQuantity = table.Column<long>(nullable: false),
                    PrimaryPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxPercentageOther = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    TaxAmountOther = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Factor = table.Column<long>(nullable: true),
                    TaxTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractItemDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractItemDetail_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractItemDetail_UnitOfMeasure1",
                        column: x => x.PrimaryUnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractItemDetail_TaxType",
                        column: x => x.TaxTypeId,
                        principalSchema: "MDM",
                        principalTable: "TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractItemDetail_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractPaymentHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<long>(nullable: false),
                    PaymentMilestone = table.Column<string>(maxLength: 4000, nullable: true),
                    PaymentPercentage = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    IsPaid = table.Column<bool>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPaymentHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSalesOrder",
                schema: "ORDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerTypeId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    OpportunityId = table.Column<long>(nullable: true),
                    ContractId = table.Column<long>(nullable: true),
                    OrderPaymentStatusId = table.Column<long>(nullable: true),
                    RequestStateId = table.Column<long>(nullable: true),
                    EditedPriceStatusId = table.Column<long>(nullable: false),
                    ShippingName = table.Column<string>(maxLength: 500, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SalesEmployeeId = table.Column<long>(nullable: false),
                    Note = table.Column<string>(maxLength: 4000, nullable: true),
                    InvoiceAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    InvoiceNationId = table.Column<long>(nullable: true),
                    InvoiceProvinceId = table.Column<long>(nullable: true),
                    InvoiceDistrictId = table.Column<long>(nullable: true),
                    InvoiceWardId = table.Column<long>(nullable: true),
                    InvoiceZIPCode = table.Column<string>(maxLength: 50, nullable: true),
                    DeliveryAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    DeliveryNationId = table.Column<long>(nullable: true),
                    DeliveryProvinceId = table.Column<long>(nullable: true),
                    DeliveryDistrictId = table.Column<long>(nullable: true),
                    DeliveryWardId = table.Column<long>(nullable: true),
                    DeliveryZIPCode = table.Column<string>(maxLength: 50, nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TotalTaxOther = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSalesOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_CustomerType",
                        column: x => x.CustomerTypeId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_District1",
                        column: x => x.DeliveryDistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_Nation1",
                        column: x => x.DeliveryNationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_Province1",
                        column: x => x.DeliveryProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_Ward1",
                        column: x => x.DeliveryWardId,
                        principalSchema: "MDM",
                        principalTable: "Ward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_EditedPriceStatus",
                        column: x => x.EditedPriceStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EditedPriceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_District",
                        column: x => x.InvoiceDistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_Nation",
                        column: x => x.InvoiceNationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_Province",
                        column: x => x.InvoiceProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_Ward",
                        column: x => x.InvoiceWardId,
                        principalSchema: "MDM",
                        principalTable: "Ward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_OrderPaymentStatus",
                        column: x => x.OrderPaymentStatusId,
                        principalSchema: "ENUM",
                        principalTable: "OrderPaymentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_RequestState",
                        column: x => x.RequestStateId,
                        principalSchema: "WF",
                        principalTable: "RequestState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrder_AppUser",
                        column: x => x.SalesEmployeeId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSalesOrderContent",
                schema: "ORDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerSalesOrderId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    RequestedQuantity = table.Column<long>(nullable: false),
                    PrimaryUnitOfMeasureId = table.Column<long>(nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    PrimaryPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxPercentageOther = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    TaxAmountOther = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Factor = table.Column<long>(nullable: true),
                    EditedPriceStatusId = table.Column<long>(nullable: false),
                    TaxTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSalesOrderContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderContent_CustomerSalesOrder",
                        column: x => x.CustomerSalesOrderId,
                        principalSchema: "ORDER",
                        principalTable: "CustomerSalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderContent_EditedPriceStatus",
                        column: x => x.EditedPriceStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EditedPriceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderContent_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderContent_UnitOfMeasure1",
                        column: x => x.PrimaryUnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderContent_TaxType",
                        column: x => x.TaxTypeId,
                        principalSchema: "MDM",
                        principalTable: "TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderContent_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSalesOrderPaymentHistory",
                schema: "ORDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerSalesOrderId = table.Column<long>(nullable: false),
                    PaymentMilestone = table.Column<string>(maxLength: 400, nullable: true),
                    PaymentPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    IsPaid = table.Column<bool>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSalesOrderPaymentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderPaymentHistory_CustomerSalesOrder",
                        column: x => x.CustomerSalesOrderId,
                        principalSchema: "ORDER",
                        principalTable: "CustomerSalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSalesOrderPromotion",
                schema: "ORDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerSalesOrderId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    RequestedQuantity = table.Column<long>(nullable: false),
                    PrimaryUnitOfMeasureId = table.Column<long>(nullable: false),
                    Factor = table.Column<long>(nullable: true),
                    Note = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSalesOrderPromotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderPromotion_CustomerSalesOrder",
                        column: x => x.CustomerSalesOrderId,
                        principalSchema: "ORDER",
                        principalTable: "CustomerSalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderPromotion_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderPromotion_UnitOfMeasure1",
                        column: x => x.PrimaryUnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSalesOrderPromotion_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyFileMapping",
                schema: "OPP",
                columns: table => new
                {
                    CompanyFileGroupingId = table.Column<long>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFileMapping", x => new { x.CompanyFileGroupingId, x.FileId });
                    table.ForeignKey(
                        name: "FK_CompanyFileMapping_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactFileMapping",
                schema: "OPP",
                columns: table => new
                {
                    ContactFileGroupingId = table.Column<long>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFileMapping", x => new { x.ContactFileGroupingId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ContactFileMapping_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadFileMapping",
                schema: "OPP",
                columns: table => new
                {
                    CustomerLeadFileGroupId = table.Column<long>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadFileMapping", x => new { x.CustomerLeadFileGroupId, x.FileId });
                    table.ForeignKey(
                        name: "FK_CustomerLeadFileMapping_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityFileMapping",
                schema: "OPP",
                columns: table => new
                {
                    OpportunityFileGroupingId = table.Column<long>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityFileMapping", x => new { x.OpportunityFileGroupingId, x.FileId });
                    table.ForeignKey(
                        name: "FK_OpportunityFileMapping_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    CompanyId = table.Column<long>(nullable: true),
                    OpportunityId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    ContractTypeId = table.Column<long>(nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    DeliveryUnit = table.Column<string>(maxLength: 500, nullable: true),
                    ContractStatusId = table.Column<long>(nullable: false),
                    PaymentStatusId = table.Column<long>(nullable: false),
                    InvoiceAddress = table.Column<string>(maxLength: 4000, nullable: false),
                    InvoiceNationId = table.Column<long>(nullable: true),
                    InvoiceProvinceId = table.Column<long>(nullable: true),
                    InvoiceDistrictId = table.Column<long>(nullable: true),
                    InvoiceZipCode = table.Column<string>(maxLength: 100, nullable: true),
                    ReceiveAddress = table.Column<string>(maxLength: 4000, nullable: false),
                    ReceiveNationId = table.Column<long>(nullable: true),
                    ReceiveProvinceId = table.Column<long>(nullable: true),
                    ReceiveDistrictId = table.Column<long>(nullable: true),
                    ReceiveZipCode = table.Column<string>(maxLength: 100, nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TotalTaxAmountOther = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TotalTaxAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TermAndCondition = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatorId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_ContractStatus",
                        column: x => x.ContractStatusId,
                        principalSchema: "ENUM",
                        principalTable: "ContractStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_ContractType",
                        column: x => x.ContractTypeId,
                        principalSchema: "ENUM",
                        principalTable: "ContractType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "ENUM",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_District",
                        column: x => x.InvoiceDistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Nation",
                        column: x => x.InvoiceNationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Province",
                        column: x => x.InvoiceProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_PaymentStatus",
                        column: x => x.PaymentStatusId,
                        principalSchema: "ENUM",
                        principalTable: "PaymentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_District1",
                        column: x => x.ReceiveDistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Nation1",
                        column: x => x.ReceiveNationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Province1",
                        column: x => x.ReceiveProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairTicket",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    DeviceSerial = table.Column<string>(maxLength: 500, nullable: true),
                    OrderCategoryId = table.Column<long>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    RepairDueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ItemId = table.Column<long>(nullable: true),
                    IsRejectRepair = table.Column<bool>(nullable: true),
                    RejectReason = table.Column<string>(maxLength: 4000, nullable: true),
                    DeviceState = table.Column<string>(maxLength: 500, nullable: false),
                    RepairStatusId = table.Column<long>(nullable: true),
                    RepairAddess = table.Column<string>(maxLength: 500, nullable: true),
                    ReceiveUser = table.Column<string>(maxLength: 255, nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RepairDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RepairSolution = table.Column<string>(maxLength: 4000, nullable: true),
                    Note = table.Column<string>(maxLength: 1000, nullable: true),
                    RepairCost = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    PaymentStatusId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairTicket_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairTicket_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairTicket_OrderCategory",
                        column: x => x.OrderCategoryId,
                        principalSchema: "ENUM",
                        principalTable: "OrderCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairTicket_PaymentStatus",
                        column: x => x.PaymentStatusId,
                        principalSchema: "ENUM",
                        principalTable: "PaymentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairTicket_RepairStatus",
                        column: x => x.RepairStatusId,
                        principalSchema: "ENUM",
                        principalTable: "RepairStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCustomerGroupingMapping",
                schema: "CUSTOMER",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerGroupingId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCustomerGroupingMapping", x => new { x.CustomerId, x.CustomerGroupingId });
                    table.ForeignKey(
                        name: "FK_CustomerCustomerGroupingMapping_CustomerGrouping",
                        column: x => x.CustomerGroupingId,
                        principalSchema: "MDM",
                        principalTable: "CustomerGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerEmail",
                schema: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    EmailTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerEmail_EmailType",
                        column: x => x.EmailTypeId,
                        principalSchema: "ENUM",
                        principalTable: "EmailType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerEmailHistory",
                schema: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    Reciepient = table.Column<string>(maxLength: 500, nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    EmailStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEmailHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerEmailHistory_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerEmailHistory_EmailStatus",
                        column: x => x.EmailStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EmailStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCCEmailHistory",
                schema: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerEmailHistoryId = table.Column<long>(nullable: false),
                    CCEmail = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCCEmailHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCCEmailHistory_CustomerEmailHistory",
                        column: x => x.CustomerEmailHistoryId,
                        principalSchema: "CUSTOMER",
                        principalTable: "CustomerEmailHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFeedback",
                schema: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsSystemCustomer = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    FullName = table.Column<string>(maxLength: 500, nullable: false),
                    Email = table.Column<string>(maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CustomerFeedbackTypeId = table.Column<long>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Content = table.Column<string>(nullable: true),
                    StatusId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerFeedback_CustomerFeedbackType",
                        column: x => x.CustomerFeedbackTypeId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerFeedbackType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerFeedback_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPhone",
                schema: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPhone_PhoneType",
                        column: x => x.PhoneTypeId,
                        principalSchema: "MDM",
                        principalTable: "PhoneType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPointHistory",
                schema: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    TotalPoint = table.Column<long>(nullable: false),
                    CurrentPoint = table.Column<long>(nullable: false),
                    ChangePoint = table.Column<long>(nullable: false),
                    IsIncrease = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    ReduceTotal = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPointHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "MDM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 400, nullable: true),
                    CodeDraft = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    UnsignName = table.Column<string>(maxLength: 500, nullable: true),
                    ParentStoreId = table.Column<long>(nullable: true),
                    OrganizationId = table.Column<long>(nullable: false),
                    StoreTypeId = table.Column<long>(nullable: false),
                    StoreGroupingId = table.Column<long>(nullable: true),
                    Telephone = table.Column<string>(maxLength: 500, nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    WardId = table.Column<long>(nullable: true),
                    Address = table.Column<string>(maxLength: 3000, nullable: true),
                    UnsignAddress = table.Column<string>(maxLength: 3000, nullable: true),
                    DeliveryAddress = table.Column<string>(maxLength: 3000, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18, 15)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18, 15)", nullable: false),
                    DeliveryLatitude = table.Column<decimal>(type: "decimal(18, 15)", nullable: true),
                    DeliveryLongitude = table.Column<decimal>(type: "decimal(18, 15)", nullable: true),
                    OwnerName = table.Column<string>(maxLength: 500, nullable: false),
                    OwnerPhone = table.Column<string>(maxLength: 500, nullable: false),
                    OwnerEmail = table.Column<string>(maxLength: 500, nullable: true),
                    TaxCode = table.Column<string>(maxLength: 500, nullable: true),
                    LegalEntity = table.Column<string>(maxLength: 500, nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    StoreStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false),
                    Used = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_District",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_Store",
                        column: x => x.ParentStoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_StoreGrouping",
                        column: x => x.StoreGroupingId,
                        principalSchema: "MDM",
                        principalTable: "StoreGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_StoreStatus",
                        column: x => x.StoreStatusId,
                        principalSchema: "ENUM",
                        principalTable: "StoreStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_StoreType",
                        column: x => x.StoreTypeId,
                        principalSchema: "MDM",
                        principalTable: "StoreType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_Ward",
                        column: x => x.WardId,
                        principalSchema: "MDM",
                        principalTable: "Ward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessConcentrationLevel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Manufacturer = table.Column<string>(maxLength: 200, nullable: true),
                    Branch = table.Column<string>(maxLength: 200, nullable: true),
                    RevenueInYear = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    MarketingStaff = table.Column<long>(nullable: true),
                    StoreId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessConcentrationLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessConcentrationLevel_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImproveQualityServing",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Detail = table.Column<string>(maxLength: 200, nullable: true),
                    StoreId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImproveQualityServing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImproveQualityServing_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreAssets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Quantity = table.Column<long>(nullable: true),
                    Owned = table.Column<long>(nullable: true),
                    Rent = table.Column<long>(nullable: true),
                    StoreId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreAssets_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreConsultingServiceMapping",
                columns: table => new
                {
                    StoreId = table.Column<long>(nullable: false),
                    ConsultingServiceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreConsultingServiceMapping", x => new { x.StoreId, x.ConsultingServiceId });
                    table.ForeignKey(
                        name: "FK_StoreConsultingServiceMapping_ConsultingService",
                        column: x => x.ConsultingServiceId,
                        principalSchema: "ENUM",
                        principalTable: "ConsultingService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreConsultingServiceMapping_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreCooperativeAttitudeMapping",
                columns: table => new
                {
                    StoreId = table.Column<long>(nullable: false),
                    CooperativeAttitudeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCooperativeAttitudeMapping", x => new { x.StoreId, x.CooperativeAttitudeId });
                    table.ForeignKey(
                        name: "FK_StoreCooperativeAttitudeMapping_CooperativeAttitude",
                        column: x => x.CooperativeAttitudeId,
                        principalSchema: "ENUM",
                        principalTable: "CooperativeAttitude",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreCooperativeAttitudeMapping_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreCoverageCapacity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Detail = table.Column<string>(maxLength: 200, nullable: true),
                    StoreId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCoverageCapacity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreCoverageCapacity_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreDeliveryTimeMapping",
                columns: table => new
                {
                    StoreId = table.Column<long>(nullable: false),
                    StoreDeliveryTimeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDeliveryTimeMapping", x => new { x.StoreId, x.StoreDeliveryTimeId });
                    table.ForeignKey(
                        name: "FK_StoreDeliveryTimeMapping_StoreDeliveryTime",
                        column: x => x.StoreDeliveryTimeId,
                        principalSchema: "ENUM",
                        principalTable: "StoreDeliveryTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreDeliveryTimeMapping_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreExtend",
                columns: table => new
                {
                    StoreId = table.Column<long>(nullable: false),
                    PhoneOther = table.Column<string>(maxLength: 50, nullable: false),
                    Fax = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    BusinessCapital = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    BusinessTypeId = table.Column<long>(nullable: false),
                    BankAccountNumber = table.Column<string>(maxLength: 50, nullable: false),
                    BankName = table.Column<string>(maxLength: 200, nullable: false),
                    BusinessLicense = table.Column<string>(maxLength: 200, nullable: false),
                    DateOfBusinessLicense = table.Column<DateTime>(type: "datetime", nullable: true),
                    AgentContractNumber = table.Column<string>(maxLength: 200, nullable: false),
                    DateOfAgentContractNumber = table.Column<DateTime>(type: "datetime", nullable: true),
                    DistributionArea = table.Column<string>(maxLength: 1000, nullable: false),
                    RegionalPopulation = table.Column<long>(nullable: false),
                    DistributionAcreage = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    UrbanizationLevel = table.Column<string>(maxLength: 50, nullable: false),
                    NumberOfPointsOfSale = table.Column<long>(nullable: false),
                    NumberOfKeyCustomer = table.Column<long>(nullable: false),
                    MarketCharacteristics = table.Column<string>(nullable: false),
                    StoreAcreage = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    WareHouseAcreage = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AbilityToPay = table.Column<string>(nullable: false),
                    RewardInYear = table.Column<long>(nullable: true),
                    AbilityRaisingCapital = table.Column<string>(nullable: true),
                    AbilityLimitedCapital = table.Column<string>(nullable: true),
                    DivideEachPart = table.Column<bool>(nullable: true),
                    DivideHuman = table.Column<bool>(nullable: true),
                    AnotherStrongPoint = table.Column<string>(nullable: true),
                    ReadyCoordinate = table.Column<string>(maxLength: 1000, nullable: true),
                    Invest = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreExtend", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_StoreExtend_BusinessType",
                        column: x => x.BusinessTypeId,
                        principalSchema: "ENUM",
                        principalTable: "BusinessType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreExtend_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "ENUM",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreExtend_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreImageMapping",
                columns: table => new
                {
                    StoreId = table.Column<long>(nullable: false),
                    ImageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreImageMapping", x => new { x.StoreId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_StoreImageMapping_Image",
                        column: x => x.ImageId,
                        principalSchema: "MDM",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreImageMapping_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreInfulenceLevelMarketMapping",
                columns: table => new
                {
                    StoreId = table.Column<long>(nullable: false),
                    InfulenceLevelMarketId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInfulenceLevelMarketMapping", x => new { x.StoreId, x.InfulenceLevelMarketId });
                    table.ForeignKey(
                        name: "FK_StoreInfulenceLevelMarketMapping_InfulenceLevelMarket",
                        column: x => x.InfulenceLevelMarketId,
                        principalSchema: "ENUM",
                        principalTable: "InfulenceLevelMarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreInfulenceLevelMarketMapping_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreMarketPriceMapping",
                columns: table => new
                {
                    StoreId = table.Column<long>(nullable: false),
                    MarketPriceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketPriceMapping", x => new { x.StoreId, x.MarketPriceId });
                    table.ForeignKey(
                        name: "FK_StoreMarketPriceMapping_MarketPrice",
                        column: x => x.MarketPriceId,
                        principalSchema: "ENUM",
                        principalTable: "MarketPrice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreMarketPriceMapping_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreMeansOfDelivery",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Quantity = table.Column<long>(nullable: true),
                    Owned = table.Column<long>(nullable: true),
                    Rent = table.Column<long>(nullable: true),
                    StoreId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMeansOfDelivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMeansOfDelivery_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorePersonnel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Quantity = table.Column<long>(nullable: true),
                    StoreId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePersonnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorePersonnel_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreRelationshipCustomerMapping",
                columns: table => new
                {
                    RelationshipCustomerTypeId = table.Column<long>(nullable: false),
                    StoreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRelationshipCustomer", x => new { x.RelationshipCustomerTypeId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_StoreRelationshipCustomerMapping_RelationshipCustomerType",
                        column: x => x.RelationshipCustomerTypeId,
                        principalSchema: "ENUM",
                        principalTable: "RelationshipCustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreRelationshipCustomerMapping_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreRepresent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 500, nullable: true),
                    PositionId = table.Column<long>(nullable: true),
                    StoreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRepresent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreRepresent_Position",
                        column: x => x.PositionId,
                        principalSchema: "MDM",
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreRepresent_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreWarrantyService",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Detail = table.Column<string>(maxLength: 200, nullable: true),
                    StoreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreWarrantyService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreWarrantyService_Store",
                        column: x => x.StoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectSalesOrder",
                schema: "ORDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false, comment: "Id"),
                    Code = table.Column<string>(maxLength: 50, nullable: false, comment: "Mã đơn hàng"),
                    OrganizationId = table.Column<long>(nullable: false),
                    BuyerStoreId = table.Column<long>(nullable: false, comment: "Cửa hàng mua"),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true, comment: "Số điện thoại"),
                    StoreAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    DeliveryAddress = table.Column<string>(maxLength: 4000, nullable: true, comment: "Địa chỉ giao hàng"),
                    SaleEmployeeId = table.Column<long>(nullable: false, comment: "Nhân viên kinh doanh"),
                    CreatorId = table.Column<long>(nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày đặt hàng"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày giao hàng"),
                    RequestStateId = table.Column<long>(nullable: false),
                    EditedPriceStatusId = table.Column<long>(nullable: false, comment: "Sửa giá"),
                    Note = table.Column<string>(maxLength: 4000, nullable: true, comment: "Ghi chú"),
                    SubTotal = table.Column<decimal>(type: "decimal(18, 4)", nullable: false, comment: "Tổng tiền trước thuế"),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true, comment: "% chiết khấu tổng"),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true, comment: "Số tiền chiết khấu tổng"),
                    TotalTaxAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: false, comment: "Tổng thuế"),
                    TotalAfterTax = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    PromotionCode = table.Column<string>(maxLength: 50, nullable: true),
                    PromotionValue = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18, 4)", nullable: false, comment: "Tổng tiền sau thuế"),
                    RowId = table.Column<Guid>(nullable: false, comment: "Id global cho phê duyệt"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectSalesOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrder_Store",
                        column: x => x.BuyerStoreId,
                        principalSchema: "MDM",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrder_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrder_EditedPriceStatus",
                        column: x => x.EditedPriceStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EditedPriceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrder_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "MDM",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrder_RequestState",
                        column: x => x.RequestStateId,
                        principalSchema: "WF",
                        principalTable: "RequestState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrder_AppUser",
                        column: x => x.SaleEmployeeId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectSalesOrderContent",
                schema: "ORDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    DirectSalesOrderId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    PrimaryUnitOfMeasureId = table.Column<long>(nullable: false),
                    RequestedQuantity = table.Column<long>(nullable: false),
                    PrimaryPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false, comment: "Giá theo đơn vị lưu kho"),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false, comment: "Giá bán theo đơn vị xuất hàng"),
                    EditedPriceStatusId = table.Column<long>(nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Factor = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectSalesOrderContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderContent_DirectSalesOrder",
                        column: x => x.DirectSalesOrderId,
                        principalSchema: "ORDER",
                        principalTable: "DirectSalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderContent_EditedPriceStatus",
                        column: x => x.EditedPriceStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EditedPriceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderContent_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderContent_UnitOfMeasure1",
                        column: x => x.PrimaryUnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderContent_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectSalesOrderPromotion",
                schema: "ORDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    DirectSalesOrderId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    PrimaryUnitOfMeasureId = table.Column<long>(nullable: false),
                    RequestedQuantity = table.Column<long>(nullable: false),
                    Note = table.Column<string>(maxLength: 4000, nullable: true),
                    Factor = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectSalesOrderPromotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderPromotion_DirectSalesOrder",
                        column: x => x.DirectSalesOrderId,
                        principalSchema: "ORDER",
                        principalTable: "DirectSalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderPromotion_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderPromotion_UnitOfMeasure",
                        column: x => x.PrimaryUnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectSalesOrderPromotion_UnitOfMeasure1",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyActivity",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: false),
                    ActivityPriorityId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    CompanyId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    ActivityStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyActivity_ActivityPriority",
                        column: x => x.ActivityPriorityId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityPriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyActivity_ActivityStatus",
                        column: x => x.ActivityStatusId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyActivity_ActivityType",
                        column: x => x.ActivityTypeId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyActivity_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactActivity",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: false),
                    ActivityPriorityId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    ContactId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    ActivityStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactActivity_ActivityPriority",
                        column: x => x.ActivityPriorityId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityPriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactActivity_ActivityStatus",
                        column: x => x.ActivityStatusId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactActivity_ActivityType",
                        column: x => x.ActivityTypeId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactActivity_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadActivity",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: false),
                    ActivityPriorityId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    CustomerLeadId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    ActivityStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLeadActivity_ActivityPriority",
                        column: x => x.ActivityPriorityId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityPriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadActivity_ActivityStatus",
                        column: x => x.ActivityStatusId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadActivity_ActivityType",
                        column: x => x.ActivityTypeId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadActivity_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityActivity",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: false),
                    ActivityPriorityId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    OpportunityId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    ActivityStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpportunityActivity_ActivityPriority",
                        column: x => x.ActivityPriorityId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityPriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityActivity_ActivityStatus",
                        column: x => x.ActivityStatusId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityActivity_ActivityType",
                        column: x => x.ActivityTypeId,
                        principalSchema: "ENUM",
                        principalTable: "ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityActivity_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 400, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    NationId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    WardId = table.Column<long>(nullable: true),
                    CustomerTypeId = table.Column<long>(nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    ProfessionId = table.Column<long>(nullable: true),
                    CustomerResourceId = table.Column<long>(nullable: true),
                    SexId = table.Column<long>(nullable: true),
                    StatusId = table.Column<long>(nullable: false),
                    CompanyId = table.Column<long>(nullable: true),
                    ParentCompanyId = table.Column<long>(nullable: true),
                    TaxCode = table.Column<string>(maxLength: 50, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    Website = table.Column<string>(maxLength: 500, nullable: true),
                    NumberOfEmployee = table.Column<long>(nullable: true),
                    BusinessTypeId = table.Column<long>(nullable: true),
                    Investment = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    RevenueAnnual = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    IsSupplier = table.Column<bool>(nullable: true),
                    Descreption = table.Column<string>(maxLength: 4000, nullable: true),
                    AppUserId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    Used = table.Column<bool>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_AppUser1",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_BusinessType",
                        column: x => x.BusinessTypeId,
                        principalSchema: "ENUM",
                        principalTable: "BusinessType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerResource",
                        column: x => x.CustomerResourceId,
                        principalSchema: "MDM",
                        principalTable: "CustomerResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerType",
                        column: x => x.CustomerTypeId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_District",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Nation",
                        column: x => x.NationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Profession",
                        column: x => x.ProfessionId,
                        principalSchema: "MDM",
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Sex",
                        column: x => x.SexId,
                        principalSchema: "ENUM",
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Status",
                        column: x => x.StatusId,
                        principalSchema: "ENUM",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Ward",
                        column: x => x.WardId,
                        principalSchema: "MDM",
                        principalTable: "Ward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    FAX = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneOther = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 500, nullable: true),
                    EmailOther = table.Column<string>(maxLength: 500, nullable: true),
                    ZIPCode = table.Column<string>(maxLength: 50, nullable: true),
                    Revenue = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    NationId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    NumberOfEmployee = table.Column<long>(nullable: true),
                    RefuseReciveEmail = table.Column<bool>(nullable: true),
                    RefuseReciveSMS = table.Column<bool>(nullable: true),
                    CustomerLeadId = table.Column<long>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    Path = table.Column<string>(maxLength: 400, nullable: true),
                    Level = table.Column<long>(nullable: true),
                    ProfessionId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatorId = table.Column<long>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: true),
                    CompanyStatusId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_CompanyStatus",
                        column: x => x.CompanyStatusId,
                        principalSchema: "ENUM",
                        principalTable: "CompanyStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "ENUM",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_District2",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Nation2",
                        column: x => x.NationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Comapny",
                        column: x => x.ParentId,
                        principalSchema: "OPP",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Profession",
                        column: x => x.ProfessionId,
                        principalSchema: "MDM",
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Province2",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmail",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    Reciepient = table.Column<string>(maxLength: 500, nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    EmailStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyEmail_Company",
                        column: x => x.CompanyId,
                        principalSchema: "OPP",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyEmail_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyEmail_EmailStatus",
                        column: x => x.EmailStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EmailStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyFileGrouping",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    CompanyId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    FileTypeId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFileGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFileGroupMapping_Company1",
                        column: x => x.CompanyId,
                        principalSchema: "OPP",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyFileGroupMapping_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyFileGroupMapping_FileType",
                        column: x => x.FileTypeId,
                        principalSchema: "ENUM",
                        principalTable: "FileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLead",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 255, nullable: true),
                    TelePhone = table.Column<string>(maxLength: 255, nullable: true),
                    Phone = table.Column<string>(maxLength: 255, nullable: true),
                    Fax = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    SecondEmail = table.Column<string>(maxLength: 255, nullable: true),
                    Website = table.Column<string>(maxLength: 255, nullable: true),
                    CustomerLeadSourceId = table.Column<long>(nullable: true),
                    CustomerLeadLevelId = table.Column<long>(nullable: true),
                    CompanyId = table.Column<long>(nullable: true),
                    CampaignId = table.Column<long>(nullable: true),
                    ProfessionId = table.Column<long>(nullable: true),
                    Revenue = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    EmployeeQuantity = table.Column<long>(nullable: true),
                    Address = table.Column<string>(maxLength: 400, nullable: true),
                    NationId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    CustomerLeadStatusId = table.Column<long>(nullable: true),
                    BusinessRegistrationCode = table.Column<string>(maxLength: 50, nullable: true),
                    SexId = table.Column<long>(nullable: true),
                    RefuseReciveSMS = table.Column<bool>(nullable: true),
                    RefuseReciveEmail = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    CreatorId = table.Column<long>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 50, nullable: true),
                    CurrencyId = table.Column<long>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLead_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_Company",
                        column: x => x.CompanyId,
                        principalSchema: "OPP",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "ENUM",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_CustomerLeadLevel",
                        column: x => x.CustomerLeadLevelId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerLeadLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_CustomerLeadSource",
                        column: x => x.CustomerLeadSourceId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerLeadSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_CustomerLeadStatus",
                        column: x => x.CustomerLeadStatusId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerLeadStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_District",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_Nation",
                        column: x => x.NationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_Profession",
                        column: x => x.ProfessionId,
                        principalSchema: "MDM",
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLead_Sex",
                        column: x => x.SexId,
                        principalSchema: "ENUM",
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmailCCMapping",
                schema: "OPP",
                columns: table => new
                {
                    CompanyEmailId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmailCCMapping", x => new { x.CompanyEmailId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_CompanyEmailCCMapping_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyEmailCCMapping_CompanyEmail",
                        column: x => x.CompanyEmailId,
                        principalSchema: "OPP",
                        principalTable: "CompanyEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    ProfessionId = table.Column<long>(nullable: true),
                    CompanyId = table.Column<long>(nullable: true),
                    ContactStatusId = table.Column<long>(nullable: true),
                    Address = table.Column<string>(maxLength: 2000, nullable: true),
                    NationId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    CustomerLeadId = table.Column<long>(nullable: true),
                    ImageId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    EmailOther = table.Column<string>(maxLength: 500, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneHome = table.Column<string>(maxLength: 50, nullable: true),
                    FAX = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 500, nullable: true),
                    Department = table.Column<string>(maxLength: 500, nullable: true),
                    ZIPCode = table.Column<string>(maxLength: 200, nullable: true),
                    SexId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    RefuseReciveEmail = table.Column<bool>(nullable: true),
                    RefuseReciveSMS = table.Column<bool>(nullable: true),
                    PositionId = table.Column<long>(nullable: true),
                    CreatorId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_AppUser1",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Company1",
                        column: x => x.CompanyId,
                        principalSchema: "OPP",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_ContactStatus",
                        column: x => x.ContactStatusId,
                        principalSchema: "ENUM",
                        principalTable: "ContactStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_CustomerLead",
                        column: x => x.CustomerLeadId,
                        principalSchema: "OPP",
                        principalTable: "CustomerLead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_District",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Image1",
                        column: x => x.ImageId,
                        principalSchema: "MDM",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Nation",
                        column: x => x.NationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Position",
                        column: x => x.PositionId,
                        principalSchema: "MDM",
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Profession",
                        column: x => x.ProfessionId,
                        principalSchema: "MDM",
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Sex",
                        column: x => x.SexId,
                        principalSchema: "ENUM",
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadEmail",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    Reciepient = table.Column<string>(maxLength: 500, nullable: false),
                    CustomerLeadId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    EmailStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLeadEmailMapping_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadEmail_CustomerLead",
                        column: x => x.CustomerLeadId,
                        principalSchema: "OPP",
                        principalTable: "CustomerLead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadEmailMapping_EmailStatus",
                        column: x => x.EmailStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EmailStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadFileGroup",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false, comment: "Id")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 4000, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(maxLength: 4000, nullable: true, comment: "Tên"),
                    CustomerLeadId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    FileTypeId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá"),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadFileGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLeadFileGroupMapping_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadFileGroup_CustomerLead",
                        column: x => x.CustomerLeadId,
                        principalSchema: "OPP",
                        principalTable: "CustomerLead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadFileGroup_FileType",
                        column: x => x.FileTypeId,
                        principalSchema: "ENUM",
                        principalTable: "FileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadItemMapping",
                schema: "OPP",
                columns: table => new
                {
                    CustomerLeadId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    RequestQuantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PrimaryPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    VATOther = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Factor = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLeadItemMapping", x => new { x.CustomerLeadId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_CustomerLeadItemMapping_CustomerLead",
                        column: x => x.CustomerLeadId,
                        principalSchema: "OPP",
                        principalTable: "CustomerLead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadItemMapping_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadItemMapping_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opportunity",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    CompanyId = table.Column<long>(nullable: true),
                    CustomerLeadId = table.Column<long>(nullable: true),
                    ClosingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SaleStageId = table.Column<long>(nullable: true),
                    ProbabilityId = table.Column<long>(nullable: false),
                    PotentialResultId = table.Column<long>(nullable: true),
                    LeadSourceId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    ForecastAmount = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    RefuseReciveSMS = table.Column<bool>(nullable: true),
                    RefuseReciveEmail = table.Column<bool>(nullable: true),
                    OpportunityResultTypeId = table.Column<long>(nullable: true),
                    CreatorId = table.Column<long>(nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opportunity_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_Company",
                        column: x => x.CompanyId,
                        principalSchema: "OPP",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "ENUM",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_CustomerLead",
                        column: x => x.CustomerLeadId,
                        principalSchema: "OPP",
                        principalTable: "CustomerLead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_CustomerLeadSource",
                        column: x => x.LeadSourceId,
                        principalSchema: "ENUM",
                        principalTable: "CustomerLeadSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_OpportunityResultType",
                        column: x => x.OpportunityResultTypeId,
                        principalSchema: "ENUM",
                        principalTable: "OpportunityResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_PotentialResult",
                        column: x => x.PotentialResultId,
                        principalSchema: "ENUM",
                        principalTable: "PotentialResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_Probability",
                        column: x => x.ProbabilityId,
                        principalSchema: "ENUM",
                        principalTable: "Probability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunity_SaleStage",
                        column: x => x.SaleStageId,
                        principalSchema: "ENUM",
                        principalTable: "SaleStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactEmail",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    Reciepient = table.Column<string>(maxLength: 500, nullable: false),
                    ContactId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    EmailStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactEmail_Contact",
                        column: x => x.ContactId,
                        principalSchema: "OPP",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactEmail_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactEmail_EmailStatus",
                        column: x => x.EmailStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EmailStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactFileGrouping",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    ContactId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    FileTypeId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFileGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactFileGrouping_Contact",
                        column: x => x.ContactId,
                        principalSchema: "OPP",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactFileGrouping_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactFileGrouping_FileType",
                        column: x => x.FileTypeId,
                        principalSchema: "ENUM",
                        principalTable: "FileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLeadEmailCCMapping",
                schema: "OPP",
                columns: table => new
                {
                    CustomerLeadEmailId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEmailCCMapping", x => new { x.CustomerLeadEmailId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_CustomerLeadEmailCCMapping_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLeadEmailCCMapping_CustomerLeadEmail",
                        column: x => x.CustomerLeadEmailId,
                        principalSchema: "OPP",
                        principalTable: "CustomerLeadEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityContactMapping",
                schema: "OPP",
                columns: table => new
                {
                    ContactId = table.Column<long>(nullable: false),
                    OpportunityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityContactMapping", x => new { x.ContactId, x.OpportunityId });
                    table.ForeignKey(
                        name: "FK_OpportunityContactMapping_Contact",
                        column: x => x.ContactId,
                        principalSchema: "OPP",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityContactMapping_Opportunity",
                        column: x => x.OpportunityId,
                        principalSchema: "OPP",
                        principalTable: "Opportunity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityEmail",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    Reciepient = table.Column<string>(maxLength: 500, nullable: false),
                    OpportunityId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    EmailStatusId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày cập nhật"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày xoá")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpportunityEmail_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityEmail_EmailStatus",
                        column: x => x.EmailStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EmailStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityEmail_Opportunity",
                        column: x => x.OpportunityId,
                        principalSchema: "OPP",
                        principalTable: "Opportunity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityFileGrouping",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    OpportunityId = table.Column<long>(nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    FileTypeId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityFileGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpportunityFileGrouping_AppUser",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityFileGrouping_FileType",
                        column: x => x.FileTypeId,
                        principalSchema: "ENUM",
                        principalTable: "FileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityFileGrouping_Opportunity",
                        column: x => x.OpportunityId,
                        principalSchema: "OPP",
                        principalTable: "Opportunity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityItemMapping",
                schema: "OPP",
                columns: table => new
                {
                    OpportunityId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    RequestQuantity = table.Column<long>(nullable: false),
                    PrimaryPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    VATOther = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Factor = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityItemMapping", x => new { x.OpportunityId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_OpportunityItemMapping_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityProductMapping_Opportunity",
                        column: x => x.OpportunityId,
                        principalSchema: "OPP",
                        principalTable: "Opportunity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityItemMapping_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderQuote",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(maxLength: 255, nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    ContactId = table.Column<long>(nullable: false),
                    OpportunityId = table.Column<long>(nullable: true),
                    EditedPriceStatusId = table.Column<long>(nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    OrderQuoteStatusId = table.Column<long>(nullable: false),
                    Note = table.Column<string>(maxLength: 4000, nullable: true),
                    InvoiceAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    InvoiceNationId = table.Column<long>(nullable: true),
                    InvoiceProvinceId = table.Column<long>(nullable: true),
                    InvoiceDistrictId = table.Column<long>(nullable: true),
                    InvoiceZIPCode = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    NationId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    ZIPCode = table.Column<string>(maxLength: 255, nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    TotalTaxAmountOther = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TotalTaxAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    CreatorId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Ngày tạo"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(curdate())"),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderQuote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderQuote_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_Company",
                        column: x => x.CompanyId,
                        principalSchema: "OPP",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_Contact",
                        column: x => x.ContactId,
                        principalSchema: "OPP",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_AppUser1",
                        column: x => x.CreatorId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_District1",
                        column: x => x.DistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_EditedPriceStatus",
                        column: x => x.EditedPriceStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EditedPriceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_District",
                        column: x => x.InvoiceDistrictId,
                        principalSchema: "MDM",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_Nation1",
                        column: x => x.InvoiceNationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_Province1",
                        column: x => x.InvoiceProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_Nation",
                        column: x => x.NationId,
                        principalSchema: "MDM",
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_Opportunity",
                        column: x => x.OpportunityId,
                        principalSchema: "OPP",
                        principalTable: "Opportunity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_OrderQuoteStatus",
                        column: x => x.OrderQuoteStatusId,
                        principalSchema: "ENUM",
                        principalTable: "OrderQuoteStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuote_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "MDM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactEmailCCMapping",
                schema: "OPP",
                columns: table => new
                {
                    ContactEmailId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEmailCCMapping", x => new { x.ContactEmailId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_ContactEmailCCMapping_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactEmailCCMapping_ContactEmail",
                        column: x => x.ContactEmailId,
                        principalSchema: "OPP",
                        principalTable: "ContactEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityEmailCCMapping",
                schema: "OPP",
                columns: table => new
                {
                    OpportunityEmailId = table.Column<long>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpporutunityEmaiCCMapping", x => new { x.OpportunityEmailId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_OpportunityEmailCCMapping_AppUser",
                        column: x => x.AppUserId,
                        principalSchema: "MDM",
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityEmailCCMapping_OpportunityEmail",
                        column: x => x.OpportunityEmailId,
                        principalSchema: "OPP",
                        principalTable: "OpportunityEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderQuoteContent",
                schema: "OPP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderQuoteId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    RequestedQuantity = table.Column<long>(nullable: false),
                    PrimaryUnitOfMeasureId = table.Column<long>(nullable: false),
                    PrimaryPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    GeneralDiscountPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    GeneralDiscountAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxPercentage = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxAmountOther = table.Column<decimal>(type: "decimal(18, 4)", nullable: true),
                    TaxPercentageOther = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Factor = table.Column<long>(nullable: true),
                    EditedPriceStatusId = table.Column<long>(nullable: false),
                    TaxTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderQuoteContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderQuoteContent_EditedPriceStatus",
                        column: x => x.EditedPriceStatusId,
                        principalSchema: "ENUM",
                        principalTable: "EditedPriceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuoteContent_Item",
                        column: x => x.ItemId,
                        principalSchema: "MDM",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuoteContent_OrderQuote",
                        column: x => x.OrderQuoteId,
                        principalSchema: "OPP",
                        principalTable: "OrderQuote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuoteContent_UnitOfMeasure1",
                        column: x => x.PrimaryUnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuoteContent_TaxType",
                        column: x => x.TaxTypeId,
                        principalSchema: "MDM",
                        principalTable: "TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderQuoteContent_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "MDM",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogProperty_AppUserId",
                table: "AuditLogProperty",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessConcentrationLevel_StoreId",
                table: "BusinessConcentrationLevel",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CallEmotion_StatusId",
                table: "CallEmotion",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CallLog_AppUserId",
                table: "CallLog",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CallLog_CallCategoryId",
                table: "CallLog",
                column: "CallCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CallLog_CallEmotionId",
                table: "CallLog",
                column: "CallEmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_CallLog_CallStatusId",
                table: "CallLog",
                column: "CallStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CallLog_CallTypeId",
                table: "CallLog",
                column: "CallTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CallLog_CreatorId",
                table: "CallLog",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CallLog_EntityReferenceId",
                table: "CallLog",
                column: "EntityReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CallType_StatusId",
                table: "CallType",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_AppUserId",
                table: "Contract",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CompanyId",
                table: "Contract",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractStatusId",
                table: "Contract",
                column: "ContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeId",
                table: "Contract",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CreatorId",
                table: "Contract",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CurrencyId",
                table: "Contract",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerId",
                table: "Contract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_InvoiceDistrictId",
                table: "Contract",
                column: "InvoiceDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_InvoiceNationId",
                table: "Contract",
                column: "InvoiceNationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_InvoiceProvinceId",
                table: "Contract",
                column: "InvoiceProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_OpportunityId",
                table: "Contract",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_OrganizationId",
                table: "Contract",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_PaymentStatusId",
                table: "Contract",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ReceiveDistrictId",
                table: "Contract",
                column: "ReceiveDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ReceiveNationId",
                table: "Contract",
                column: "ReceiveNationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ReceiveProvinceId",
                table: "Contract",
                column: "ReceiveProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractContactMapping_ContactId",
                table: "ContractContactMapping",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractFileGrouping_ContractId",
                table: "ContractFileGrouping",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractFileGrouping_CreatorId",
                table: "ContractFileGrouping",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractFileGrouping_FileTypeId",
                table: "ContractFileGrouping",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractFileMapping_FileId",
                table: "ContractFileMapping",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractItemDetail_ContractId",
                table: "ContractItemDetail",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractItemDetail_ItemId",
                table: "ContractItemDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractItemDetail_PrimaryUnitOfMeasureId",
                table: "ContractItemDetail",
                column: "PrimaryUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractItemDetail_TaxTypeId",
                table: "ContractItemDetail",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractItemDetail_UnitOfMeasureId",
                table: "ContractItemDetail",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPaymentHistory_ContractId",
                table: "ContractPaymentHistory",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_File_AppUserId",
                table: "File",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImproveQualityServing_StoreId",
                table: "ImproveQualityServing",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticle_CreatorId",
                table: "KnowledgeArticle",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticle_GroupId",
                table: "KnowledgeArticle",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticle_ItemId",
                table: "KnowledgeArticle",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticle_KMSStatusId",
                table: "KnowledgeArticle",
                column: "KMSStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticle_StatusId",
                table: "KnowledgeArticle",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticleKeyword_KnowledgeArticleId",
                table: "KnowledgeArticleKeyword",
                column: "KnowledgeArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticleOrganizationMapping_OrganizationId",
                table: "KnowledgeArticleOrganizationMapping",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeGroup_StatusId",
                table: "KnowledgeGroup",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneral_CreatorId",
                table: "KpiGeneral",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneral_EmployeeId",
                table: "KpiGeneral",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneral_KpiYearId",
                table: "KpiGeneral",
                column: "KpiYearId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneral_OrganizationId",
                table: "KpiGeneral",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneral_StatusId",
                table: "KpiGeneral",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneralContent_KpiCriteriaGeneralId",
                table: "KpiGeneralContent",
                column: "KpiCriteriaGeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneralContent_KpiGeneralId",
                table: "KpiGeneralContent",
                column: "KpiGeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneralContent_StatusId",
                table: "KpiGeneralContent",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiGeneralContentKpiPeriodMapping_KpiPeriodId",
                table: "KpiGeneralContentKpiPeriodMapping",
                column: "KpiPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItem_CreatorId",
                table: "KpiItem",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItem_EmployeeId",
                table: "KpiItem",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItem_KpiPeriodId",
                table: "KpiItem",
                column: "KpiPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItem_KpiYearId",
                table: "KpiItem",
                column: "KpiYearId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItem_OrganizationId",
                table: "KpiItem",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItem_StatusId",
                table: "KpiItem",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItemContent_ItemId",
                table: "KpiItemContent",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItemContent_KpiItemId",
                table: "KpiItemContent",
                column: "KpiItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiItemContentKpiCriteriaItemMapping_KpiCriteriaItemId",
                table: "KpiItemContentKpiCriteriaItemMapping",
                column: "KpiCriteriaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MailTemplate_StatusId",
                table: "MailTemplate",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationStatusId",
                table: "Notification",
                column: "NotificationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OrganizationId",
                table: "Notification",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTicket_CreatorId",
                table: "RepairTicket",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTicket_CustomerId",
                table: "RepairTicket",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTicket_ItemId",
                table: "RepairTicket",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTicket_OrderCategoryId",
                table: "RepairTicket",
                column: "OrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTicket_PaymentStatusId",
                table: "RepairTicket",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTicket_RepairStatusId",
                table: "RepairTicket",
                column: "RepairStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleMaster_ManagerId",
                table: "ScheduleMaster",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleMaster_SalerId",
                table: "ScheduleMaster",
                column: "SalerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleMaster_StatusId",
                table: "ScheduleMaster",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlert_MailTemplateId",
                table: "SLAAlert",
                column: "MailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlert_SmsTemplateId",
                table: "SLAAlert",
                column: "SmsTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlert_TicketIssueLevelId",
                table: "SLAAlert",
                column: "TicketIssueLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlert_TimeUnitId",
                table: "SLAAlert",
                column: "TimeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRT_MailTemplateId",
                table: "SLAAlertFRT",
                column: "MailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRT_SmsTemplateId",
                table: "SLAAlertFRT",
                column: "SmsTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRT_TicketIssueLevelId",
                table: "SLAAlertFRT",
                column: "TicketIssueLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRT_TimeUnitId",
                table: "SLAAlertFRT",
                column: "TimeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRTMail_SLAAlertFRTId",
                table: "SLAAlertFRTMail",
                column: "SLAAlertFRTId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRTPhone_SLAAlertFRTId",
                table: "SLAAlertFRTPhone",
                column: "SLAAlertFRTId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRTUser_AppUserId",
                table: "SLAAlertFRTUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertFRTUser_SLAAlertFRTId",
                table: "SLAAlertFRTUser",
                column: "SLAAlertFRTId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertMail_SLAAlertId",
                table: "SLAAlertMail",
                column: "SLAAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertPhone_SLAAlertId",
                table: "SLAAlertPhone",
                column: "SLAAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertUser_AppUserId",
                table: "SLAAlertUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAAlertUser_SLAAlertId",
                table: "SLAAlertUser",
                column: "SLAAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalation_MailTemplateId",
                table: "SLAEscalation",
                column: "MailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalation_SmsTemplateId",
                table: "SLAEscalation",
                column: "SmsTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalation_TicketIssueLevelId",
                table: "SLAEscalation",
                column: "TicketIssueLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalation_TimeUnitId",
                table: "SLAEscalation",
                column: "TimeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRT_MailTemplateId",
                table: "SLAEscalationFRT",
                column: "MailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRT_SmsTemplateId",
                table: "SLAEscalationFRT",
                column: "SmsTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRT_TicketIssueLevelId",
                table: "SLAEscalationFRT",
                column: "TicketIssueLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRT_TimeUnitId",
                table: "SLAEscalationFRT",
                column: "TimeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRTMail_SLAEscalationFRTId",
                table: "SLAEscalationFRTMail",
                column: "SLAEscalationFRTId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRTPhone_SLAEscalationFRTId",
                table: "SLAEscalationFRTPhone",
                column: "SLAEscalationFRTId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRTUser_AppUserId",
                table: "SLAEscalationFRTUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationFRTUser_SLAEscalationFRTId",
                table: "SLAEscalationFRTUser",
                column: "SLAEscalationFRTId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationMail_SLAEscalationId",
                table: "SLAEscalationMail",
                column: "SLAEscalationId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationUser_AppUserId",
                table: "SLAEscalationUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAEscalationUser_SLAEscalationId",
                table: "SLAEscalationUser",
                column: "SLAEscalationId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAPolicy_FirstResponseUnitId",
                table: "SLAPolicy",
                column: "FirstResponseUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAPolicy_ResolveUnitId",
                table: "SLAPolicy",
                column: "ResolveUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAPolicy_TicketIssueLevelId",
                table: "SLAPolicy",
                column: "TicketIssueLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAPolicy_TicketPriorityId",
                table: "SLAPolicy",
                column: "TicketPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsQueue_EntityReferenceId",
                table: "SmsQueue",
                column: "EntityReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsQueue_SentByAppUserId",
                table: "SmsQueue",
                column: "SentByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsQueue_SmsQueueStatusId",
                table: "SmsQueue",
                column: "SmsQueueStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsTemplate_StatusId",
                table: "SmsTemplate",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreAssets_StoreId",
                table: "StoreAssets",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreConsultingServiceMapping_ConsultingServiceId",
                table: "StoreConsultingServiceMapping",
                column: "ConsultingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCooperativeAttitudeMapping_CooperativeAttitudeId",
                table: "StoreCooperativeAttitudeMapping",
                column: "CooperativeAttitudeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCoverageCapacity_StoreId",
                table: "StoreCoverageCapacity",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreDeliveryTimeMapping_StoreDeliveryTimeId",
                table: "StoreDeliveryTimeMapping",
                column: "StoreDeliveryTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExtend_BusinessTypeId",
                table: "StoreExtend",
                column: "BusinessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExtend_CurrencyId",
                table: "StoreExtend",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreImageMapping_ImageId",
                table: "StoreImageMapping",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInfulenceLevelMarketMapping_InfulenceLevelMarketId",
                table: "StoreInfulenceLevelMarketMapping",
                column: "InfulenceLevelMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketPriceMapping_MarketPriceId",
                table: "StoreMarketPriceMapping",
                column: "MarketPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMeansOfDelivery_StoreId",
                table: "StoreMeansOfDelivery",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePersonnel_StoreId",
                table: "StorePersonnel",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRelationshipCustomerMapping_StoreId",
                table: "StoreRelationshipCustomerMapping",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRepresent_PositionId",
                table: "StoreRepresent",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRepresent_StoreId",
                table: "StoreRepresent",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreWarrantyService_StoreId",
                table: "StoreWarrantyService",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AppUserClosedId",
                table: "Ticket",
                column: "AppUserClosedId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AppUserResolvedId",
                table: "Ticket",
                column: "AppUserResolvedId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CreatorId",
                table: "Ticket",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CustomerId",
                table: "Ticket",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CustomerTypeId",
                table: "Ticket",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DepartmentId",
                table: "Ticket",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EntityReferenceId",
                table: "Ticket",
                column: "EntityReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ProductId",
                table: "Ticket",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_RelatedCallLogId",
                table: "Ticket",
                column: "RelatedCallLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_RelatedTicketId",
                table: "Ticket",
                column: "RelatedTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SLAPolicyId",
                table: "Ticket",
                column: "SLAPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SLAStatusId",
                table: "Ticket",
                column: "SLAStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_StatusId",
                table: "Ticket",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketIssueLevelId",
                table: "Ticket",
                column: "TicketIssueLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketPriorityId",
                table: "Ticket",
                column: "TicketPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketResolveTypeId",
                table: "Ticket",
                column: "TicketResolveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketSourceId",
                table: "Ticket",
                column: "TicketSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketStatusId",
                table: "Ticket",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketGroup_StatusId",
                table: "TicketGroup",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketGroup_TicketTypeId",
                table: "TicketGroup",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketIssueLevel_StatusId",
                table: "TicketIssueLevel",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketIssueLevel_TicketGroupId",
                table: "TicketIssueLevel",
                column: "TicketGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOfDepartment_DepartmentId",
                table: "TicketOfDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOfDepartment_TicketId",
                table: "TicketOfDepartment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOfDepartment_TicketStatusId",
                table: "TicketOfDepartment",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOfUser_TicketId",
                table: "TicketOfUser",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOfUser_TicketStatusId",
                table: "TicketOfUser",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOfUser_UserId",
                table: "TicketOfUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPriority_StatusId",
                table: "TicketPriority",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSource_StatusId",
                table: "TicketSource",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_StatusId",
                table: "TicketType",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AppUserId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_BusinessTypeId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "BusinessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CompanyId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreatorId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerResourceId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "CustomerResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerTypeId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DistrictId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_NationId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ParentCompanyId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ProfessionId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ProvinceId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_SexId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StatusId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_WardId",
                schema: "CUSTOMER",
                table: "Customer",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCallLogMapping_CallLogId",
                schema: "CUSTOMER",
                table: "CustomerCallLogMapping",
                column: "CallLogId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCCEmailHistory_CustomerEmailHistoryId",
                schema: "CUSTOMER",
                table: "CustomerCCEmailHistory",
                column: "CustomerEmailHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCustomerGroupingMapping_CustomerGroupingId",
                schema: "CUSTOMER",
                table: "CustomerCustomerGroupingMapping",
                column: "CustomerGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmail_CustomerId",
                schema: "CUSTOMER",
                table: "CustomerEmail",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmail_EmailTypeId",
                schema: "CUSTOMER",
                table: "CustomerEmail",
                column: "EmailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmailHistory_CreatorId",
                schema: "CUSTOMER",
                table: "CustomerEmailHistory",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmailHistory_CustomerId",
                schema: "CUSTOMER",
                table: "CustomerEmailHistory",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmailHistory_EmailStatusId",
                schema: "CUSTOMER",
                table: "CustomerEmailHistory",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedback_CustomerFeedbackTypeId",
                schema: "CUSTOMER",
                table: "CustomerFeedback",
                column: "CustomerFeedbackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedback_CustomerId",
                schema: "CUSTOMER",
                table: "CustomerFeedback",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedback_StatusId",
                schema: "CUSTOMER",
                table: "CustomerFeedback",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhone_CustomerId",
                schema: "CUSTOMER",
                table: "CustomerPhone",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhone_PhoneTypeId",
                schema: "CUSTOMER",
                table: "CustomerPhone",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPointHistory_CustomerId",
                schema: "CUSTOMER",
                table: "CustomerPointHistory",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_OrganizationId",
                schema: "MDM",
                table: "AppUser",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_SexId",
                schema: "MDM",
                table: "AppUser",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_StatusId",
                schema: "MDM",
                table: "AppUser",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoleMapping_RoleId",
                schema: "MDM",
                table: "AppUserRoleMapping",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_StatusId",
                schema: "MDM",
                table: "Brand",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ImageId",
                schema: "MDM",
                table: "Category",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                schema: "MDM",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_StatusId",
                schema: "MDM",
                table: "Category",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGrouping_CustomerTypeId",
                schema: "MDM",
                table: "CustomerGrouping",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGrouping_ParentId",
                schema: "MDM",
                table: "CustomerGrouping",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGrouping_StatusId",
                schema: "MDM",
                table: "CustomerGrouping",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLevel_StatusId",
                schema: "MDM",
                table: "CustomerLevel",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerResource_StatusId",
                schema: "MDM",
                table: "CustomerResource",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_District_ProvinceId",
                schema: "MDM",
                table: "District",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_District_StatusId",
                schema: "MDM",
                table: "District",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ItemId",
                schema: "MDM",
                table: "Inventory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_WarehouseId",
                schema: "MDM",
                table: "Inventory",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_AppUserId",
                schema: "MDM",
                table: "InventoryHistory",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_InventoryId",
                schema: "MDM",
                table: "InventoryHistory",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ProductId",
                schema: "MDM",
                table: "Item",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_StatusId",
                schema: "MDM",
                table: "Item",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImageMapping_ImageId",
                schema: "MDM",
                table: "ItemImageMapping",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Nation_StatusId",
                schema: "MDM",
                table: "Nation",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_ParentId",
                schema: "MDM",
                table: "Organization",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_StatusId",
                schema: "MDM",
                table: "Organization",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneType_StatusId",
                schema: "MDM",
                table: "PhoneType",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_StatusId",
                schema: "MDM",
                table: "Position",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                schema: "MDM",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "MDM",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                schema: "MDM",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_StatusId",
                schema: "MDM",
                table: "Product",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TaxTypeId",
                schema: "MDM",
                table: "Product",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureGroupingId",
                schema: "MDM",
                table: "Product",
                column: "UnitOfMeasureGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                schema: "MDM",
                table: "Product",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UsedVariationId",
                schema: "MDM",
                table: "Product",
                column: "UsedVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGrouping_ParentId",
                schema: "MDM",
                table: "ProductGrouping",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageMapping_ImageId",
                schema: "MDM",
                table: "ProductImageMapping",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductGroupingMapping_ProductGroupingId",
                schema: "MDM",
                table: "ProductProductGroupingMapping",
                column: "ProductGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_StatusId",
                schema: "MDM",
                table: "ProductType",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Profession_StatusId",
                schema: "MDM",
                table: "Profession",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_StatusId",
                schema: "MDM",
                table: "Province",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_AppUserId",
                schema: "MDM",
                table: "Store",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_CustomerId",
                schema: "MDM",
                table: "Store",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_DistrictId",
                schema: "MDM",
                table: "Store",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_OrganizationId",
                schema: "MDM",
                table: "Store",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_ParentStoreId",
                schema: "MDM",
                table: "Store",
                column: "ParentStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_ProvinceId",
                schema: "MDM",
                table: "Store",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_StatusId",
                schema: "MDM",
                table: "Store",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_StoreGroupingId",
                schema: "MDM",
                table: "Store",
                column: "StoreGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_StoreStatusId",
                schema: "MDM",
                table: "Store",
                column: "StoreStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_StoreTypeId",
                schema: "MDM",
                table: "Store",
                column: "StoreTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_WardId",
                schema: "MDM",
                table: "Store",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGrouping_ParentId",
                schema: "MDM",
                table: "StoreGrouping",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreGrouping_StatusId",
                schema: "MDM",
                table: "StoreGrouping",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreType_ColorId",
                schema: "MDM",
                table: "StoreType",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreType_StatusId",
                schema: "MDM",
                table: "StoreType",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_DistrictId",
                schema: "MDM",
                table: "Supplier",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_NationId",
                schema: "MDM",
                table: "Supplier",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_PersonInChargeId",
                schema: "MDM",
                table: "Supplier",
                column: "PersonInChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_ProvinceId",
                schema: "MDM",
                table: "Supplier",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_StatusId",
                schema: "MDM",
                table: "Supplier",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_WardId",
                schema: "MDM",
                table: "Supplier",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxType_StatusId",
                schema: "MDM",
                table: "TaxType",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatus_StatusId",
                schema: "MDM",
                table: "TicketStatus",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_StatusId",
                schema: "MDM",
                table: "UnitOfMeasure",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasureGrouping_StatusId",
                schema: "MDM",
                table: "UnitOfMeasureGrouping",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasureGrouping_UnitOfMeasureId",
                schema: "MDM",
                table: "UnitOfMeasureGrouping",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasureGroupingContent_UnitOfMeasureGroupingId",
                schema: "MDM",
                table: "UnitOfMeasureGroupingContent",
                column: "UnitOfMeasureGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasureGroupingContent_UnitOfMeasureId",
                schema: "MDM",
                table: "UnitOfMeasureGroupingContent",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Variation_VariationGroupingId",
                schema: "MDM",
                table: "Variation",
                column: "VariationGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationGrouping_ProductId",
                schema: "MDM",
                table: "VariationGrouping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ward_DistrictId",
                schema: "MDM",
                table: "Ward",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Ward_StatusId",
                schema: "MDM",
                table: "Ward",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_DistrictId",
                schema: "MDM",
                table: "Warehouse",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_OrganizationId",
                schema: "MDM",
                table: "Warehouse",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_ProvinceId",
                schema: "MDM",
                table: "Warehouse",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_StatusId",
                schema: "MDM",
                table: "Warehouse",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_WardId",
                schema: "MDM",
                table: "Warehouse",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AppUserId",
                schema: "OPP",
                table: "Company",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyStatusId",
                schema: "OPP",
                table: "Company",
                column: "CompanyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CreatorId",
                schema: "OPP",
                table: "Company",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CurrencyId",
                schema: "OPP",
                table: "Company",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CustomerLeadId",
                schema: "OPP",
                table: "Company",
                column: "CustomerLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_DistrictId",
                schema: "OPP",
                table: "Company",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_NationId",
                schema: "OPP",
                table: "Company",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ParentId",
                schema: "OPP",
                table: "Company",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ProfessionId",
                schema: "OPP",
                table: "Company",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ProvinceId",
                schema: "OPP",
                table: "Company",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyActivity_ActivityPriorityId",
                schema: "OPP",
                table: "CompanyActivity",
                column: "ActivityPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyActivity_ActivityStatusId",
                schema: "OPP",
                table: "CompanyActivity",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyActivity_ActivityTypeId",
                schema: "OPP",
                table: "CompanyActivity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyActivity_AppUserId",
                schema: "OPP",
                table: "CompanyActivity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyActivity_CompanyId",
                schema: "OPP",
                table: "CompanyActivity",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCallLogMapping_CallLogId",
                schema: "OPP",
                table: "CompanyCallLogMapping",
                column: "CallLogId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmail_CompanyId",
                schema: "OPP",
                table: "CompanyEmail",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmail_CreatorId",
                schema: "OPP",
                table: "CompanyEmail",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmail_EmailStatusId",
                schema: "OPP",
                table: "CompanyEmail",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmailCCMapping_AppUserId",
                schema: "OPP",
                table: "CompanyEmailCCMapping",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFileGrouping_CompanyId",
                schema: "OPP",
                table: "CompanyFileGrouping",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFileGrouping_CreatorId",
                schema: "OPP",
                table: "CompanyFileGrouping",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFileGrouping_FileTypeId",
                schema: "OPP",
                table: "CompanyFileGrouping",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFileMapping_FileId",
                schema: "OPP",
                table: "CompanyFileMapping",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AppUserId",
                schema: "OPP",
                table: "Contact",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CompanyId",
                schema: "OPP",
                table: "Contact",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactStatusId",
                schema: "OPP",
                table: "Contact",
                column: "ContactStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CreatorId",
                schema: "OPP",
                table: "Contact",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CustomerLeadId",
                schema: "OPP",
                table: "Contact",
                column: "CustomerLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_DistrictId",
                schema: "OPP",
                table: "Contact",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ImageId",
                schema: "OPP",
                table: "Contact",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_NationId",
                schema: "OPP",
                table: "Contact",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PositionId",
                schema: "OPP",
                table: "Contact",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ProfessionId",
                schema: "OPP",
                table: "Contact",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ProvinceId",
                schema: "OPP",
                table: "Contact",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_SexId",
                schema: "OPP",
                table: "Contact",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactActivity_ActivityPriorityId",
                schema: "OPP",
                table: "ContactActivity",
                column: "ActivityPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactActivity_ActivityStatusId",
                schema: "OPP",
                table: "ContactActivity",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactActivity_ActivityTypeId",
                schema: "OPP",
                table: "ContactActivity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactActivity_AppUserId",
                schema: "OPP",
                table: "ContactActivity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactActivity_ContactId",
                schema: "OPP",
                table: "ContactActivity",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactCallLogMapping_CallLogId",
                schema: "OPP",
                table: "ContactCallLogMapping",
                column: "CallLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmail_ContactId",
                schema: "OPP",
                table: "ContactEmail",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmail_CreatorId",
                schema: "OPP",
                table: "ContactEmail",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmail_EmailStatusId",
                schema: "OPP",
                table: "ContactEmail",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmailCCMapping_AppUserId",
                schema: "OPP",
                table: "ContactEmailCCMapping",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFileGrouping_ContactId",
                schema: "OPP",
                table: "ContactFileGrouping",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFileGrouping_CreatorId",
                schema: "OPP",
                table: "ContactFileGrouping",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFileGrouping_FileTypeId",
                schema: "OPP",
                table: "ContactFileGrouping",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFileMapping_FileId",
                schema: "OPP",
                table: "ContactFileMapping",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_AppUserId",
                schema: "OPP",
                table: "CustomerLead",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_CompanyId",
                schema: "OPP",
                table: "CustomerLead",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_CreatorId",
                schema: "OPP",
                table: "CustomerLead",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_CurrencyId",
                schema: "OPP",
                table: "CustomerLead",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_CustomerLeadLevelId",
                schema: "OPP",
                table: "CustomerLead",
                column: "CustomerLeadLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_CustomerLeadSourceId",
                schema: "OPP",
                table: "CustomerLead",
                column: "CustomerLeadSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_CustomerLeadStatusId",
                schema: "OPP",
                table: "CustomerLead",
                column: "CustomerLeadStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_DistrictId",
                schema: "OPP",
                table: "CustomerLead",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_NationId",
                schema: "OPP",
                table: "CustomerLead",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_ProfessionId",
                schema: "OPP",
                table: "CustomerLead",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_ProvinceId",
                schema: "OPP",
                table: "CustomerLead",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLead_SexId",
                schema: "OPP",
                table: "CustomerLead",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadActivity_ActivityPriorityId",
                schema: "OPP",
                table: "CustomerLeadActivity",
                column: "ActivityPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadActivity_ActivityStatusId",
                schema: "OPP",
                table: "CustomerLeadActivity",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadActivity_ActivityTypeId",
                schema: "OPP",
                table: "CustomerLeadActivity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadActivity_AppUserId",
                schema: "OPP",
                table: "CustomerLeadActivity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadActivity_CustomerLeadId",
                schema: "OPP",
                table: "CustomerLeadActivity",
                column: "CustomerLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadCallLogMapping_CallLogId",
                schema: "OPP",
                table: "CustomerLeadCallLogMapping",
                column: "CallLogId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadEmail_CreatorId",
                schema: "OPP",
                table: "CustomerLeadEmail",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadEmail_CustomerLeadId",
                schema: "OPP",
                table: "CustomerLeadEmail",
                column: "CustomerLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadEmail_EmailStatusId",
                schema: "OPP",
                table: "CustomerLeadEmail",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadEmailCCMapping_AppUserId",
                schema: "OPP",
                table: "CustomerLeadEmailCCMapping",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadFileGroup_CreatorId",
                schema: "OPP",
                table: "CustomerLeadFileGroup",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadFileGroup_CustomerLeadId",
                schema: "OPP",
                table: "CustomerLeadFileGroup",
                column: "CustomerLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadFileGroup_FileTypeId",
                schema: "OPP",
                table: "CustomerLeadFileGroup",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadFileMapping_FileId",
                schema: "OPP",
                table: "CustomerLeadFileMapping",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadItemMapping_ItemId",
                schema: "OPP",
                table: "CustomerLeadItemMapping",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLeadItemMapping_UnitOfMeasureId",
                schema: "OPP",
                table: "CustomerLeadItemMapping",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_AppUserId",
                schema: "OPP",
                table: "Opportunity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_CompanyId",
                schema: "OPP",
                table: "Opportunity",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_CreatorId",
                schema: "OPP",
                table: "Opportunity",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_CurrencyId",
                schema: "OPP",
                table: "Opportunity",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_CustomerLeadId",
                schema: "OPP",
                table: "Opportunity",
                column: "CustomerLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_LeadSourceId",
                schema: "OPP",
                table: "Opportunity",
                column: "LeadSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_OpportunityResultTypeId",
                schema: "OPP",
                table: "Opportunity",
                column: "OpportunityResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_PotentialResultId",
                schema: "OPP",
                table: "Opportunity",
                column: "PotentialResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_ProbabilityId",
                schema: "OPP",
                table: "Opportunity",
                column: "ProbabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_SaleStageId",
                schema: "OPP",
                table: "Opportunity",
                column: "SaleStageId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_ActivityPriorityId",
                schema: "OPP",
                table: "OpportunityActivity",
                column: "ActivityPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_ActivityStatusId",
                schema: "OPP",
                table: "OpportunityActivity",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_ActivityTypeId",
                schema: "OPP",
                table: "OpportunityActivity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_AppUserId",
                schema: "OPP",
                table: "OpportunityActivity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_OpportunityId",
                schema: "OPP",
                table: "OpportunityActivity",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityCallLogMapping_CallLogId",
                schema: "OPP",
                table: "OpportunityCallLogMapping",
                column: "CallLogId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityContactMapping_OpportunityId",
                schema: "OPP",
                table: "OpportunityContactMapping",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityEmail_CreatorId",
                schema: "OPP",
                table: "OpportunityEmail",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityEmail_EmailStatusId",
                schema: "OPP",
                table: "OpportunityEmail",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityEmail_OpportunityId",
                schema: "OPP",
                table: "OpportunityEmail",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityEmailCCMapping_AppUserId",
                schema: "OPP",
                table: "OpportunityEmailCCMapping",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityFileGrouping_CreatorId",
                schema: "OPP",
                table: "OpportunityFileGrouping",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityFileGrouping_FileTypeId",
                schema: "OPP",
                table: "OpportunityFileGrouping",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityFileGrouping_OpportunityId",
                schema: "OPP",
                table: "OpportunityFileGrouping",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityFileMapping_FileId",
                schema: "OPP",
                table: "OpportunityFileMapping",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityItemMapping_ItemId",
                schema: "OPP",
                table: "OpportunityItemMapping",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityItemMapping_UnitOfMeasureId",
                schema: "OPP",
                table: "OpportunityItemMapping",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_AppUserId",
                schema: "OPP",
                table: "OrderQuote",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_CompanyId",
                schema: "OPP",
                table: "OrderQuote",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_ContactId",
                schema: "OPP",
                table: "OrderQuote",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_CreatorId",
                schema: "OPP",
                table: "OrderQuote",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_DistrictId",
                schema: "OPP",
                table: "OrderQuote",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_EditedPriceStatusId",
                schema: "OPP",
                table: "OrderQuote",
                column: "EditedPriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_InvoiceDistrictId",
                schema: "OPP",
                table: "OrderQuote",
                column: "InvoiceDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_InvoiceNationId",
                schema: "OPP",
                table: "OrderQuote",
                column: "InvoiceNationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_InvoiceProvinceId",
                schema: "OPP",
                table: "OrderQuote",
                column: "InvoiceProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_NationId",
                schema: "OPP",
                table: "OrderQuote",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_OpportunityId",
                schema: "OPP",
                table: "OrderQuote",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_OrderQuoteStatusId",
                schema: "OPP",
                table: "OrderQuote",
                column: "OrderQuoteStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuote_ProvinceId",
                schema: "OPP",
                table: "OrderQuote",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuoteContent_EditedPriceStatusId",
                schema: "OPP",
                table: "OrderQuoteContent",
                column: "EditedPriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuoteContent_ItemId",
                schema: "OPP",
                table: "OrderQuoteContent",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuoteContent_OrderQuoteId",
                schema: "OPP",
                table: "OrderQuoteContent",
                column: "OrderQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuoteContent_PrimaryUnitOfMeasureId",
                schema: "OPP",
                table: "OrderQuoteContent",
                column: "PrimaryUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuoteContent_TaxTypeId",
                schema: "OPP",
                table: "OrderQuoteContent",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuoteContent_UnitOfMeasureId",
                schema: "OPP",
                table: "OrderQuoteContent",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_ContractId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_CreatorId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_CustomerId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_CustomerTypeId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_DeliveryDistrictId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "DeliveryDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_DeliveryNationId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "DeliveryNationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_DeliveryProvinceId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "DeliveryProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_DeliveryWardId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "DeliveryWardId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_EditedPriceStatusId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "EditedPriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_InvoiceDistrictId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "InvoiceDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_InvoiceNationId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "InvoiceNationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_InvoiceProvinceId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "InvoiceProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_InvoiceWardId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "InvoiceWardId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_OpportunityId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_OrderPaymentStatusId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "OrderPaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_OrganizationId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_RequestStateId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "RequestStateId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrder_SalesEmployeeId",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "SalesEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderContent_CustomerSalesOrderId",
                schema: "ORDER",
                table: "CustomerSalesOrderContent",
                column: "CustomerSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderContent_EditedPriceStatusId",
                schema: "ORDER",
                table: "CustomerSalesOrderContent",
                column: "EditedPriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderContent_ItemId",
                schema: "ORDER",
                table: "CustomerSalesOrderContent",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderContent_PrimaryUnitOfMeasureId",
                schema: "ORDER",
                table: "CustomerSalesOrderContent",
                column: "PrimaryUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderContent_TaxTypeId",
                schema: "ORDER",
                table: "CustomerSalesOrderContent",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderContent_UnitOfMeasureId",
                schema: "ORDER",
                table: "CustomerSalesOrderContent",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderPaymentHistory_CustomerSalesOrderId",
                schema: "ORDER",
                table: "CustomerSalesOrderPaymentHistory",
                column: "CustomerSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderPromotion_CustomerSalesOrderId",
                schema: "ORDER",
                table: "CustomerSalesOrderPromotion",
                column: "CustomerSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderPromotion_ItemId",
                schema: "ORDER",
                table: "CustomerSalesOrderPromotion",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderPromotion_PrimaryUnitOfMeasureId",
                schema: "ORDER",
                table: "CustomerSalesOrderPromotion",
                column: "PrimaryUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesOrderPromotion_UnitOfMeasureId",
                schema: "ORDER",
                table: "CustomerSalesOrderPromotion",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrder_BuyerStoreId",
                schema: "ORDER",
                table: "DirectSalesOrder",
                column: "BuyerStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrder_CreatorId",
                schema: "ORDER",
                table: "DirectSalesOrder",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrder_EditedPriceStatusId",
                schema: "ORDER",
                table: "DirectSalesOrder",
                column: "EditedPriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrder_OrganizationId",
                schema: "ORDER",
                table: "DirectSalesOrder",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrder_RequestStateId",
                schema: "ORDER",
                table: "DirectSalesOrder",
                column: "RequestStateId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrder_SaleEmployeeId",
                schema: "ORDER",
                table: "DirectSalesOrder",
                column: "SaleEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderContent_DirectSalesOrderId",
                schema: "ORDER",
                table: "DirectSalesOrderContent",
                column: "DirectSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderContent_EditedPriceStatusId",
                schema: "ORDER",
                table: "DirectSalesOrderContent",
                column: "EditedPriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderContent_ItemId",
                schema: "ORDER",
                table: "DirectSalesOrderContent",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderContent_PrimaryUnitOfMeasureId",
                schema: "ORDER",
                table: "DirectSalesOrderContent",
                column: "PrimaryUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderContent_UnitOfMeasureId",
                schema: "ORDER",
                table: "DirectSalesOrderContent",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderPromotion_DirectSalesOrderId",
                schema: "ORDER",
                table: "DirectSalesOrderPromotion",
                column: "DirectSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderPromotion_ItemId",
                schema: "ORDER",
                table: "DirectSalesOrderPromotion",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderPromotion_PrimaryUnitOfMeasureId",
                schema: "ORDER",
                table: "DirectSalesOrderPromotion",
                column: "PrimaryUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectSalesOrderPromotion_UnitOfMeasureId",
                schema: "ORDER",
                table: "DirectSalesOrderPromotion",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Action_MenuId",
                schema: "PER",
                table: "Action",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPageMapping_PageId",
                schema: "PER",
                table: "ActionPageMapping",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_FieldTypeId",
                schema: "PER",
                table: "Field",
                column: "FieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_MenuId",
                schema: "PER",
                table: "Field",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_MenuId",
                schema: "PER",
                table: "Permission",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_RoleId",
                schema: "PER",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_StatusId",
                schema: "PER",
                table: "Permission",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionActionMapping_PermissionId",
                schema: "PER",
                table: "PermissionActionMapping",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionContent_FieldId",
                schema: "PER",
                table: "PermissionContent",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionContent_PermissionId",
                schema: "PER",
                table: "PermissionContent",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionContent_PermissionOperatorId",
                schema: "PER",
                table: "PermissionContent",
                column: "PermissionOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionOperator_FieldTypeId",
                schema: "PER",
                table: "PermissionOperator",
                column: "FieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_StatusId",
                schema: "PER",
                table: "Role",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestWorkflowDefinitionMapping_RequestStateId",
                schema: "WF",
                table: "RequestWorkflowDefinitionMapping",
                column: "RequestStateId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestWorkflowDefinitionMapping_WorkflowDefinitionId",
                schema: "WF",
                table: "RequestWorkflowDefinitionMapping",
                column: "WorkflowDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestWorkflowStepMapping_AppUserId",
                schema: "WF",
                table: "RequestWorkflowStepMapping",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestWorkflowStepMapping_WorkflowStateId",
                schema: "WF",
                table: "RequestWorkflowStepMapping",
                column: "WorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestWorkflowStepMapping_WorkflowStepId",
                schema: "WF",
                table: "RequestWorkflowStepMapping",
                column: "WorkflowStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_CreatorId",
                schema: "WF",
                table: "WorkflowDefinition",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_ModifierId",
                schema: "WF",
                table: "WorkflowDefinition",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_StatusId",
                schema: "WF",
                table: "WorkflowDefinition",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_WorkflowTypeId",
                schema: "WF",
                table: "WorkflowDefinition",
                column: "WorkflowTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDirection_FromStepId",
                schema: "WF",
                table: "WorkflowDirection",
                column: "FromStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDirection_ToStepId",
                schema: "WF",
                table: "WorkflowDirection",
                column: "ToStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDirection_WorkflowDefinitionId",
                schema: "WF",
                table: "WorkflowDirection",
                column: "WorkflowDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowParameter_WorkflowDefinitionId",
                schema: "WF",
                table: "WorkflowParameter",
                column: "WorkflowDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStep_RoleId",
                schema: "WF",
                table: "WorkflowStep",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStep_WorkflowDefinitionId",
                schema: "WF",
                table: "WorkflowStep",
                column: "WorkflowDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Customer",
                table: "Ticket",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCallLogMapping_Customer",
                schema: "CUSTOMER",
                table: "CustomerCallLogMapping",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCallLogMapping_Company",
                schema: "OPP",
                table: "CompanyCallLogMapping",
                column: "CompanyId",
                principalSchema: "OPP",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactCallLogMapping_Contact",
                schema: "OPP",
                table: "ContactCallLogMapping",
                column: "ContactId",
                principalSchema: "OPP",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLeadCallLogMapping_CustomerLead",
                schema: "OPP",
                table: "CustomerLeadCallLogMapping",
                column: "CustomerLeadId",
                principalSchema: "OPP",
                principalTable: "CustomerLead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityCallLogMapping_Opportunity",
                schema: "OPP",
                table: "OpportunityCallLogMapping",
                column: "OpportunityId",
                principalSchema: "OPP",
                principalTable: "Opportunity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContactMapping_Contact",
                table: "ContractContactMapping",
                column: "ContactId",
                principalSchema: "OPP",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContactMapping_Contract",
                table: "ContractContactMapping",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractFileGrouping_Contract",
                table: "ContractFileGrouping",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractItemDetail_Contract",
                table: "ContractItemDetail",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractPaymentHistory_Contract",
                table: "ContractPaymentHistory",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSalesOrder_Customer",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSalesOrder_Opportunity",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "OpportunityId",
                principalSchema: "OPP",
                principalTable: "Opportunity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSalesOrder_Contract",
                schema: "ORDER",
                table: "CustomerSalesOrder",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFileMapping_Company",
                schema: "OPP",
                table: "CompanyFileMapping",
                column: "CompanyFileGroupingId",
                principalSchema: "OPP",
                principalTable: "CompanyFileGrouping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactFileMapping_ContactFileGrouping",
                schema: "OPP",
                table: "ContactFileMapping",
                column: "ContactFileGroupingId",
                principalSchema: "OPP",
                principalTable: "ContactFileGrouping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLeadFileMapping_CustomerLead",
                schema: "OPP",
                table: "CustomerLeadFileMapping",
                column: "CustomerLeadFileGroupId",
                principalSchema: "OPP",
                principalTable: "CustomerLeadFileGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityFileMapping_OpportunityFileGrouping",
                schema: "OPP",
                table: "OpportunityFileMapping",
                column: "OpportunityFileGroupingId",
                principalSchema: "OPP",
                principalTable: "OpportunityFileGrouping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Company",
                table: "Contract",
                column: "CompanyId",
                principalSchema: "OPP",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Customer",
                table: "Contract",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Opportunity",
                table: "Contract",
                column: "OpportunityId",
                principalSchema: "OPP",
                principalTable: "Opportunity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairTicket_Customer",
                table: "RepairTicket",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCustomerGroupingMapping_Customer",
                schema: "CUSTOMER",
                table: "CustomerCustomerGroupingMapping",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerEmail_Customer",
                schema: "CUSTOMER",
                table: "CustomerEmail",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerEmailHistory_Customer",
                schema: "CUSTOMER",
                table: "CustomerEmailHistory",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFeedback_Customer",
                schema: "CUSTOMER",
                table: "CustomerFeedback",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPhone_Customer",
                schema: "CUSTOMER",
                table: "CustomerPhone",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPointHistory_Customer",
                schema: "CUSTOMER",
                table: "CustomerPointHistory",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Customer",
                schema: "MDM",
                table: "Store",
                column: "CustomerId",
                principalSchema: "CUSTOMER",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyActivity_Company",
                schema: "OPP",
                table: "CompanyActivity",
                column: "CompanyId",
                principalSchema: "OPP",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactActivity_Contact",
                schema: "OPP",
                table: "ContactActivity",
                column: "ContactId",
                principalSchema: "OPP",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLeadActivity_CustomerLead",
                schema: "OPP",
                table: "CustomerLeadActivity",
                column: "CustomerLeadId",
                principalSchema: "OPP",
                principalTable: "CustomerLead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivity_Opportunity",
                schema: "OPP",
                table: "OpportunityActivity",
                column: "OpportunityId",
                principalSchema: "OPP",
                principalTable: "Opportunity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Company",
                schema: "CUSTOMER",
                table: "Customer",
                column: "CompanyId",
                principalSchema: "OPP",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Company1",
                schema: "CUSTOMER",
                table: "Customer",
                column: "ParentCompanyId",
                principalSchema: "OPP",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_CustomerLead",
                schema: "OPP",
                table: "Company",
                column: "CustomerLeadId",
                principalSchema: "OPP",
                principalTable: "CustomerLead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_AppUser",
                schema: "OPP",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_AppUser1",
                schema: "OPP",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLead_AppUser",
                schema: "OPP",
                table: "CustomerLead");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLead_AppUser1",
                schema: "OPP",
                table: "CustomerLead");

            migrationBuilder.DropForeignKey(
                name: "FK_District_Status",
                schema: "MDM",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Nation_Status",
                schema: "MDM",
                table: "Nation");

            migrationBuilder.DropForeignKey(
                name: "FK_Profession_Status",
                schema: "MDM",
                table: "Profession");

            migrationBuilder.DropForeignKey(
                name: "FK_Province_Status",
                schema: "MDM",
                table: "Province");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLead_Company",
                schema: "OPP",
                table: "CustomerLead");

            migrationBuilder.DropTable(
                name: "AuditLogProperty");

            migrationBuilder.DropTable(
                name: "BusinessConcentrationLevel");

            migrationBuilder.DropTable(
                name: "ContractContactMapping");

            migrationBuilder.DropTable(
                name: "ContractFileMapping");

            migrationBuilder.DropTable(
                name: "ContractItemDetail");

            migrationBuilder.DropTable(
                name: "ContractPaymentHistory");

            migrationBuilder.DropTable(
                name: "F1_ResourceActionPageMapping");

            migrationBuilder.DropTable(
                name: "ImproveQualityServing");

            migrationBuilder.DropTable(
                name: "KnowledgeArticleKeyword");

            migrationBuilder.DropTable(
                name: "KnowledgeArticleOrganizationMapping");

            migrationBuilder.DropTable(
                name: "KpiGeneralContentKpiPeriodMapping");

            migrationBuilder.DropTable(
                name: "KpiItemContentKpiCriteriaItemMapping");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "RepairTicket");

            migrationBuilder.DropTable(
                name: "ScheduleMaster");

            migrationBuilder.DropTable(
                name: "SLAAlertFRTMail");

            migrationBuilder.DropTable(
                name: "SLAAlertFRTPhone");

            migrationBuilder.DropTable(
                name: "SLAAlertFRTUser");

            migrationBuilder.DropTable(
                name: "SLAAlertMail");

            migrationBuilder.DropTable(
                name: "SLAAlertPhone");

            migrationBuilder.DropTable(
                name: "SLAAlertUser");

            migrationBuilder.DropTable(
                name: "SLAEscalationFRTMail");

            migrationBuilder.DropTable(
                name: "SLAEscalationFRTPhone");

            migrationBuilder.DropTable(
                name: "SLAEscalationFRTUser");

            migrationBuilder.DropTable(
                name: "SLAEscalationMail");

            migrationBuilder.DropTable(
                name: "SLAEscalationPhone");

            migrationBuilder.DropTable(
                name: "SLAEscalationUser");

            migrationBuilder.DropTable(
                name: "SmsQueue");

            migrationBuilder.DropTable(
                name: "StoreAssets");

            migrationBuilder.DropTable(
                name: "StoreConsultingServiceMapping");

            migrationBuilder.DropTable(
                name: "StoreCooperativeAttitudeMapping");

            migrationBuilder.DropTable(
                name: "StoreCoverageCapacity");

            migrationBuilder.DropTable(
                name: "StoreDeliveryTimeMapping");

            migrationBuilder.DropTable(
                name: "StoreExtend");

            migrationBuilder.DropTable(
                name: "StoreImageMapping");

            migrationBuilder.DropTable(
                name: "StoreInfulenceLevelMarketMapping");

            migrationBuilder.DropTable(
                name: "StoreMarketPriceMapping");

            migrationBuilder.DropTable(
                name: "StoreMeansOfDelivery");

            migrationBuilder.DropTable(
                name: "StorePersonnel");

            migrationBuilder.DropTable(
                name: "StoreRelationshipCustomerMapping");

            migrationBuilder.DropTable(
                name: "StoreRepresent");

            migrationBuilder.DropTable(
                name: "StoreWarrantyService");

            migrationBuilder.DropTable(
                name: "TicketGeneratedId");

            migrationBuilder.DropTable(
                name: "TicketOfDepartment");

            migrationBuilder.DropTable(
                name: "TicketOfUser");

            migrationBuilder.DropTable(
                name: "CustomerCallLogMapping",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "CustomerCCEmailHistory",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "CustomerCustomerGroupingMapping",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "CustomerEmail",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "CustomerFeedback",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "CustomerPhone",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "CustomerPointHistory",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "RatingStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "SocialChannelType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "AppUserRoleMapping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "CustomerLevel",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "EventMessage",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "InventoryHistory",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "ItemImageMapping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "ProductImageMapping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "ProductProductGroupingMapping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "UnitOfMeasureGroupingContent",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Variation",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "CompanyActivity",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CompanyCallLogMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CompanyEmailCCMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CompanyFileMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "ContactActivity",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "ContactCallLogMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "ContactEmailCCMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "ContactFileMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerLeadActivity",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerLeadCallLogMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerLeadEmailCCMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerLeadFileMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerLeadItemMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OpportunityActivity",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OpportunityCallLogMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OpportunityContactMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OpportunityEmailCCMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OpportunityFileMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OpportunityItemMapping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OrderQuoteContent",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerSalesOrderContent",
                schema: "ORDER");

            migrationBuilder.DropTable(
                name: "CustomerSalesOrderPaymentHistory",
                schema: "ORDER");

            migrationBuilder.DropTable(
                name: "CustomerSalesOrderPromotion",
                schema: "ORDER");

            migrationBuilder.DropTable(
                name: "DirectSalesOrderContent",
                schema: "ORDER");

            migrationBuilder.DropTable(
                name: "DirectSalesOrderPromotion",
                schema: "ORDER");

            migrationBuilder.DropTable(
                name: "ActionPageMapping",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "PermissionActionMapping",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "PermissionContent",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "RequestWorkflowDefinitionMapping",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "RequestWorkflowParameterMapping",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "RequestWorkflowStepMapping",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "WorkflowDirection",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "ContractFileGrouping");

            migrationBuilder.DropTable(
                name: "KnowledgeArticle");

            migrationBuilder.DropTable(
                name: "KpiGeneralContent");

            migrationBuilder.DropTable(
                name: "KpiCriteriaItem",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "KpiItemContent");

            migrationBuilder.DropTable(
                name: "NotificationStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "OrderCategory",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "RepairStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "SLAAlertFRT");

            migrationBuilder.DropTable(
                name: "SLAAlert");

            migrationBuilder.DropTable(
                name: "SLAEscalationFRT");

            migrationBuilder.DropTable(
                name: "SLAEscalation");

            migrationBuilder.DropTable(
                name: "SmsQueueStatus");

            migrationBuilder.DropTable(
                name: "ConsultingService",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CooperativeAttitude",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "StoreDeliveryTime",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "InfulenceLevelMarket",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "MarketPrice",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "RelationshipCustomerType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "CustomerEmailHistory",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "CustomerGrouping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "EmailType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CustomerFeedbackType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "PhoneType",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "ProductGrouping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "VariationGrouping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "CompanyEmail",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CompanyFileGrouping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "ContactEmail",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "ContactFileGrouping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerLeadEmail",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerLeadFileGroup",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "ActivityPriority",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "ActivityStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "ActivityType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "OpportunityEmail",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "OpportunityFileGrouping",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OrderQuote",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CustomerSalesOrder",
                schema: "ORDER");

            migrationBuilder.DropTable(
                name: "DirectSalesOrder",
                schema: "ORDER");

            migrationBuilder.DropTable(
                name: "Page",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "Action",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "Field",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "PermissionOperator",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "WorkflowParameter",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "WorkflowState",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "WorkflowStep",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "KnowledgeGroup");

            migrationBuilder.DropTable(
                name: "KMSStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "KpiCriteriaGeneral",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "KpiGeneral");

            migrationBuilder.DropTable(
                name: "KpiItem");

            migrationBuilder.DropTable(
                name: "MailTemplate");

            migrationBuilder.DropTable(
                name: "SmsTemplate");

            migrationBuilder.DropTable(
                name: "CallLog");

            migrationBuilder.DropTable(
                name: "SLAPolicy");

            migrationBuilder.DropTable(
                name: "SLAStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "TicketResolveType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "TicketSource");

            migrationBuilder.DropTable(
                name: "TicketStatus",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Item",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Warehouse",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "EmailStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "FileType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "OrderQuoteStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "OrderPaymentStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "EditedPriceStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "RequestState",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "FieldType",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "PER");

            migrationBuilder.DropTable(
                name: "WorkflowDefinition",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "KpiPeriod",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "KpiYear",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CallCategory",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CallEmotion");

            migrationBuilder.DropTable(
                name: "CallStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CallType");

            migrationBuilder.DropTable(
                name: "EntityReference",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "SLATimeUnit",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "TicketIssueLevel");

            migrationBuilder.DropTable(
                name: "TicketPriority");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "ContactStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "ContractStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "ContractType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Opportunity",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "PaymentStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "StoreGrouping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "StoreStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "StoreType",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "WorkflowType",
                schema: "WF");

            migrationBuilder.DropTable(
                name: "TicketGroup");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "ProductType",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "TaxType",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "UnitOfMeasureGrouping",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "UsedVariation",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "OpportunityResultType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "PotentialResult",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Probability",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "SaleStage",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "BusinessType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CustomerResource",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "CustomerType",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Ward",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Color",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropTable(
                name: "Image",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "AppUser",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "CompanyStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CustomerLead",
                schema: "OPP");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CustomerLeadLevel",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CustomerLeadSource",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "CustomerLeadStatus",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "District",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Nation",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Profession",
                schema: "MDM");

            migrationBuilder.DropTable(
                name: "Sex",
                schema: "ENUM");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "MDM");
        }
    }
}
