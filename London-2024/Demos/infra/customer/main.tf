# #############################################################################
# Client Configurations
# #############################################################################

data "azurerm_client_config" "current" {}

# #############################################################################
# Tags
# #############################################################################

locals {
  tags = {
    Product     = var.tag_product
    Criticality = var.tag_criticality
    CostCenter  = "${var.tag_cost_center}-${var.azure_environment}"
    DR          = var.tag_disaster_recovery
    Env         = var.azure_environment
    Customer    = var.customer
  }
}

# #############################################################################
# Cosmos Containers
# #############################################################################

resource "azurerm_cosmosdb_sql_container" "flightstatus" {
  name                = var.customer
  resource_group_name = data.azurerm_resource_group.flight_tracker.name
  account_name        = data.azurerm_cosmosdb_account.flight_tracker.name
  database_name       = data.azurerm_cosmosdb_sql_database.telemetry.name
  partition_key_path  = "/flightPlanId"
}

# #############################################################################
# Storage Accounts
# #############################################################################

resource "azurerm_storage_container" "telemetry_dl" {
  name                  = "telemetry-${lower(var.customer)}"
  storage_account_name  = data.azurerm_storage_account.flight_tracker.name
  container_access_type = "private"
}

# #############################################################################
# func-processtlm (Process Telemetry)
# ############################################################################# 

resource "azurerm_storage_account" "process_telemetry" {
  name                     = "${module.storage_account.name.abbreviation}${var.telemetry_function_name}${lower(var.customer)}${var.random_identifier}${var.azure_environment}${module.azure_regions.region.region_short}"
  resource_group_name      = data.azurerm_resource_group.flight_tracker.name
  location                 = module.azure_regions.region.region_cli
  account_tier             = "Standard"
  account_replication_type = "LRS"
  tags                     = local.tags
}

resource "azurerm_service_plan" "process_telemetry" {
  name                = "${module.app_service_plan.name.abbreviation}-${var.telemetry_function_name}${lower(var.customer)}${var.random_identifier}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = data.azurerm_resource_group.flight_tracker.name
  location            = module.azure_regions.region.region_cli
  os_type             = "Linux"
  sku_name            = "Y1"
  tags                = local.tags
}

resource "azurerm_linux_function_app" "process_telemetry" {
  name                       = "${module.function_app.name.abbreviation}-${var.telemetry_function_name}${lower(var.customer)}${var.random_identifier}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name        = data.azurerm_resource_group.flight_tracker.name
  location                   = module.azure_regions.region.region_cli
  storage_account_name       = azurerm_storage_account.process_telemetry.name
  storage_account_access_key = azurerm_storage_account.process_telemetry.primary_access_key
  service_plan_id            = azurerm_service_plan.process_telemetry.id
  tags                       = local.tags
  site_config {
    ftps_state             = "FtpsOnly"
    application_stack {
      dotnet_version              = "8.0"
      use_dotnet_isolated_runtime = true
    }
    cors {
      allowed_origins = ["https://portal.azure.com"]
    }
    application_insights_connection_string = "${data.azurerm_application_insights.flight_tracker.connection_string}"
    application_insights_key               = "${data.azurerm_application_insights.flight_tracker.instrumentation_key}"
  }
  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"               = "dotnet-isolated"
    "SCM_DO_BUILD_DURING_DEPLOYMENT"         = "0"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = "1",
    "CosmosConnectionString"                 = data.azurerm_cosmosdb_account.flight_tracker.primary_sql_connection_string
    "CosmosTelemetryDatabaseName"            = data.azurerm_cosmosdb_sql_database.telemetry.name
    "CosmosTelemetryContainerName"           = var.customer
    "SqlLeaseContainerName"                  = "sqlLeases"
    "BlobConnectionString"                   = data.azurerm_storage_account.flight_tracker.primary_connection_string
    "BlobContainerName"                      = "telemetry-${lower(var.customer)}"
    "BlobLeaseContainerName"                 = "blobLeases"
    "CosmosFlightStatusDatabaseName"         = "flightstatus"
    "CosmosFlightStatusContainerName"        = "flightstatus"
    "FlightStatusLeaseContainerName"         = "flightStatusLeases"
  }
}