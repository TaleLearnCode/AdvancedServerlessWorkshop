# #############################################################################
# Client Configurations
# #############################################################################

data "azurerm_client_config" "current" {}

# ############################################################################
# Random Values
# ############################################################################

resource "random_integer" "identifier" {
  min = 1000
  max = 9999
  keepers = {
    test_name = var.resource_name
  }
}

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
    Customer    = "Platform"
  }
}

#############################################################################
# Resource Group
#############################################################################

resource "azurerm_resource_group" "flight_tracker" {
  name     = "${module.resource_group.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  location = module.azure_regions.region.region_cli
  tags     = local.tags
}

output "resource_group_name" {
  value = azurerm_resource_group.flight_tracker.name
}

# #############################################################################
# Managed Identity
# #############################################################################

resource "azurerm_user_assigned_identity" "flight_tracker" {
  name                = "${module.managed_identity.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  location            = azurerm_resource_group.flight_tracker.location
  resource_group_name = azurerm_resource_group.flight_tracker.name
  tags                = local.tags
}

# #############################################################################
#                           Key Vault
# #############################################################################

resource "azurerm_key_vault" "flight_tracker" {
  name                       = "${module.key_vault.name.abbreviation}${var.resource_short_name}${random_integer.identifier.result}${var.azure_environment}${module.azure_regions.region.region_short}"
  location                   = azurerm_resource_group.flight_tracker.location
  resource_group_name        = azurerm_resource_group.flight_tracker.name
  tenant_id                  = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days = 7
  purge_protection_enabled   = true
  sku_name                   = "standard"
  enable_rbac_authorization  = true
}

resource "azurerm_role_assignment" "key_vault_current_user" {
  scope                = azurerm_key_vault.flight_tracker.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = data.azurerm_client_config.current.object_id
}

resource "azurerm_role_assignment" "key_vault_managed_identity" {
  scope                = azurerm_key_vault.flight_tracker.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = azurerm_user_assigned_identity.flight_tracker.principal_id
}

# #############################################################################
#                           App Configuration
# #############################################################################

resource "azurerm_app_configuration" "flight_tracker" {
  name                       = "${module.app_config.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name        = azurerm_resource_group.flight_tracker.name
  location                   = azurerm_resource_group.flight_tracker.location
  sku                        = "standard"
  local_auth_enabled         = true
  public_network_access      = "Enabled"
  purge_protection_enabled   = false
  soft_delete_retention_days = 1
  tags = local.tags
}

resource "azurerm_role_assignment" "app_configuration" {
  scope                = azurerm_app_configuration.flight_tracker.id
  role_definition_name = "App Configuration Data Owner"
  principal_id         = data.azurerm_client_config.current.object_id
}

# #############################################################################
#                                Log Analytics
# #############################################################################

resource "azurerm_log_analytics_workspace" "flight_tracker" {
  name                = "${module.log_analytics_workspace.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  location            = module.azure_regions.region.region_cli
  resource_group_name = azurerm_resource_group.flight_tracker.name
  sku                 = var.log_analytics_workspace_sku
  retention_in_days   = var.log_analytics_workspace_retention_in_days
  tags                = local.tags
}

# #############################################################################
# Application Insights
# #############################################################################

resource "azurerm_application_insights" "flight_tracker" {
  name                = "${module.application_insights.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  location            = module.azure_regions.region.region_cli
  resource_group_name = azurerm_resource_group.flight_tracker.name
  workspace_id        = azurerm_log_analytics_workspace.flight_tracker.id
  application_type    = "web"
  tags                = local.tags
}

# #############################################################################
# Aszure Communication Services
# #############################################################################

resource "azurerm_communication_service" "flight_tracker" {
  name                = "${module.communication_services.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  data_location       = var.communication_services_data_location
  tags                = local.tags
}

resource "azurerm_email_communication_service" "flight_tracker" {
  name                = "${module.communication_services.name.abbreviation}email-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  data_location       = var.communication_services_data_location
  tags                = local.tags
}

# #############################################################################
# Container App Environment
# #############################################################################

#resource "azurerm_container_app_environment" "flight_tracker" {
#  name                               = "${module.container_apps_environment.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
#  resource_group_name                = azurerm_resource_group.flight_tracker.name
#  location                           = azurerm_resource_group.flight_tracker.location
#  log_analytics_workspace_id         = azurerm_log_analytics_workspace.flight_tracker.id
#  infrastructure_resource_group_name = "${module.resource_group.name.abbreviation}-${var.resource_name}${module.container_apps_environment.name.abbreviation}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
#  workload_profile {
#    name                  = "consumption"
#    workload_profile_type = "Consumption"
#    maximum_count         = 2
#    minimum_count         = 0
#  }
#}

# #############################################################################
# Event Hubs
# #############################################################################

resource "azurerm_eventhub_namespace" "flight_tracker" {
  name                     = "${module.event_hubs_namespace.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  location                 = module.azure_regions.region.region_cli
  resource_group_name      = azurerm_resource_group.flight_tracker.name
  sku                      = "Standard"
  capacity                 = 1
  auto_inflate_enabled     = true
  maximum_throughput_units = 5
  tags                     = local.tags
}

resource "azurerm_eventhub" "flight_tracker" {
  name                = "flight-tracker"
  namespace_name      = azurerm_eventhub_namespace.flight_tracker.name
  resource_group_name = azurerm_resource_group.flight_tracker.name
  partition_count     = 1
  message_retention   = 1
}

resource "azurerm_eventhub_consumer_group" "flight_tracker" {
  name                = "flight-tracker"
  namespace_name      = azurerm_eventhub_namespace.flight_tracker.name
  eventhub_name       = azurerm_eventhub.flight_tracker.name
  resource_group_name = azurerm_resource_group.flight_tracker.name
}

resource "azurerm_eventhub_authorization_rule" "flight_tracker_listener" {
  name                = "flight-tracker-listener"
  namespace_name      = azurerm_eventhub_namespace.flight_tracker.name
  eventhub_name       = azurerm_eventhub.flight_tracker.name
  resource_group_name = azurerm_resource_group.flight_tracker.name
  listen              = true
  send                = false
  manage              = false
}

resource "azurerm_eventhub_authorization_rule" "flight_tracker_sender" {
  name                = "flight-tracker-sender"
  namespace_name      = azurerm_eventhub_namespace.flight_tracker.name
  eventhub_name       = azurerm_eventhub.flight_tracker.name
  resource_group_name = azurerm_resource_group.flight_tracker.name
  listen              = false
  send                = true
  manage              = false
}

# #############################################################################
# Cosmos DB
# #############################################################################

resource "azurerm_cosmosdb_account" "flight_tracker" {
  name                = "${module.cosmos.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  location            = module.azure_regions.region.region_cli
  resource_group_name = azurerm_resource_group.flight_tracker.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"
  tags                = local.tags
  geo_location {
    location          = module.azure_regions.region.region_cli
    failover_priority = 0
  }
  consistency_policy {
    consistency_level = "Session"
  }
  capabilities {
    name = "EnableServerless"
  }
}

resource "azurerm_cosmosdb_sql_database" "telemetry" {
  name                = "telemetry"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  account_name        = azurerm_cosmosdb_account.flight_tracker.name
}

resource "azurerm_cosmosdb_sql_database" "flightstatus" {
  name                = "flight-status"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  account_name        = azurerm_cosmosdb_account.flight_tracker.name
}

resource "azurerm_cosmosdb_sql_container" "flightstatus" {
  name                = "flight-status"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  account_name        = azurerm_cosmosdb_account.flight_tracker.name
  database_name       = azurerm_cosmosdb_sql_database.telemetry.name
  partition_key_path  = "/airlineCode"
}

# #############################################################################
# SQL Server
# #############################################################################

resource "azurerm_mssql_server" "flight_tracker" {
  name                         = "${module.sql_server.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name          = azurerm_resource_group.flight_tracker.name
  location                     = module.azure_regions.region.region_cli
  version                      = "12.0"
  administrator_login          = "CloudSA"
  administrator_login_password = var.sql_admin_password
  tags                         = local.tags
}

resource "azurerm_mssql_database" "flight_tracker" {
  name                        = "${module.sql_database.name.abbreviation}-${var.resource_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  server_id                   = azurerm_mssql_server.flight_tracker.id
  sku_name                    = "GP_S_Gen5_1"
  auto_pause_delay_in_minutes = 60
  max_size_gb                 = 2
  min_capacity                = 0.5
  collation                   = "SQL_Latin1_General_CP1_CI_AS"
}

# #############################################################################
# Storage Accounts
# #############################################################################

resource "azurerm_storage_account" "flight_tracker" {
  name                     = "${module.storage_account.name.abbreviation}${var.resource_short_name}${random_integer.identifier.result}${var.azure_environment}${module.azure_regions.region.region_short}"
  resource_group_name      = azurerm_resource_group.flight_tracker.name
  location                 = module.azure_regions.region.region_cli
  account_tier             = "Standard"
  account_replication_type = "LRS"
  tags                     = local.tags
}

resource "azurerm_storage_table" "flight_status" {
  name                 = "SubmittedFlightPlans"
  storage_account_name = azurerm_storage_account.flight_tracker.name
}

# #############################################################################
# func-flightplan (Submit Flight Plan)
# ############################################################################# 

resource "azurerm_storage_account" "submit_flight_plan" {
  name                     = "${module.storage_account.name.abbreviation}${var.submit_flight_plan_name}${random_integer.identifier.result}${var.azure_environment}${module.azure_regions.region.region_short}"
  resource_group_name      = azurerm_resource_group.flight_tracker.name
  location                 = module.azure_regions.region.region_cli
  account_tier             = "Standard"
  account_replication_type = "LRS"
  tags                     = local.tags
}

resource "azurerm_service_plan" "submit_flight_plan" {
  name                = "${module.app_service_plan.name.abbreviation}-${var.submit_flight_plan_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  location            = module.azure_regions.region.region_cli
  os_type             = "Linux"
  sku_name            = "Y1"
  tags                = local.tags
}

resource "azurerm_linux_function_app" "submit_flight_plan" {
  name                       = "${module.function_app.name.abbreviation}-${var.submit_flight_plan_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name        = azurerm_resource_group.flight_tracker.name
  location                   = module.azure_regions.region.region_cli
  storage_account_name       = azurerm_storage_account.submit_flight_plan.name
  storage_account_access_key = azurerm_storage_account.submit_flight_plan.primary_access_key
  service_plan_id            = azurerm_service_plan.submit_flight_plan.id
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
    application_insights_connection_string = "${azurerm_application_insights.flight_tracker.connection_string}"
    application_insights_key               = "${azurerm_application_insights.flight_tracker.instrumentation_key}"
  }
  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"               = "dotnet-isolated"
    "SCM_DO_BUILD_DURING_DEPLOYMENT"         = "0"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = "1",
    "StorageConnectionString"                = azurerm_storage_account.flight_tracker.primary_connection_string
    "SubmittedFlightPlansTableName"          = azurerm_storage_table.flight_status.name
    "ACSConnectionString"                    = azurerm_communication_service.flight_tracker.primary_connection_string
    "NoReplyEmailAddress"                    = var.submit_flight_plan_sender_email
    "NotificationEmailAddress"               = var.submit_flight_plan_receiver_email
    "ApprovalTimeoutMinutes"                 = var.sumbit_flight_plan_approval_timeout
  }
}

# #############################################################################
# func-receivetlm (Receive Telemetry)
# ############################################################################# 

resource "azurerm_storage_account" "receive_telemetry" {
  name                     = "${module.storage_account.name.abbreviation}${var.receive_telemetry_name}${random_integer.identifier.result}${var.azure_environment}${module.azure_regions.region.region_short}"
  resource_group_name      = azurerm_resource_group.flight_tracker.name
  location                 = module.azure_regions.region.region_cli
  account_tier             = "Standard"
  account_replication_type = "LRS"
  tags                     = local.tags
}

resource "azurerm_service_plan" "receive_telemetry" {
  name                = "${module.app_service_plan.name.abbreviation}-${var.receive_telemetry_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  location            = module.azure_regions.region.region_cli
  os_type             = "Linux"
  sku_name            = "Y1"
  tags                = local.tags
}

resource "azurerm_linux_function_app" "receive_telemetry" {
  name                       = "${module.function_app.name.abbreviation}-${var.receive_telemetry_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name        = azurerm_resource_group.flight_tracker.name
  location                   = module.azure_regions.region.region_cli
  storage_account_name       = azurerm_storage_account.receive_telemetry.name
  storage_account_access_key = azurerm_storage_account.receive_telemetry.primary_access_key
  service_plan_id            = azurerm_service_plan.receive_telemetry.id
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
    application_insights_connection_string = "${azurerm_application_insights.flight_tracker.connection_string}"
    application_insights_key               = "${azurerm_application_insights.flight_tracker.instrumentation_key}"
  }
  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"               = "dotnet-isolated"
    "SCM_DO_BUILD_DURING_DEPLOYMENT"         = "0"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = "1",
    "CosmosConnectionString"                 = azurerm_cosmosdb_account.flight_tracker.primary_sql_connection_string
    "CosmosDatabase"                         = azurerm_cosmosdb_sql_database.telemetry.name
    "EventHubConnection"                     = azurerm_eventhub_authorization_rule.flight_tracker_sender.primary_connection_string
    "EventHubName"                           = azurerm_eventhub.flight_tracker.name
  }
}

# #############################################################################
# func-processtlm (Process Telemetry)
# ############################################################################# 

resource "azurerm_storage_account" "process_telemetry" {
  name                     = "${module.storage_account.name.abbreviation}${var.process_telemetry_name}${random_integer.identifier.result}${var.azure_environment}${module.azure_regions.region.region_short}"
  resource_group_name      = azurerm_resource_group.flight_tracker.name
  location                 = module.azure_regions.region.region_cli
  account_tier             = "Standard"
  account_replication_type = "LRS"
  tags                     = local.tags
}

resource "azurerm_service_plan" "process_telemetry" {
  name                = "${module.app_service_plan.name.abbreviation}-${var.process_telemetry_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = azurerm_resource_group.flight_tracker.name
  location            = module.azure_regions.region.region_cli
  os_type             = "Linux"
  sku_name            = "Y1"
  tags                = local.tags
}

resource "azurerm_linux_function_app" "process_telemetry" {
  name                       = "${module.function_app.name.abbreviation}-${var.process_telemetry_name}${random_integer.identifier.result}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name        = azurerm_resource_group.flight_tracker.name
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
    application_insights_connection_string = "${azurerm_application_insights.flight_tracker.connection_string}"
    application_insights_key               = "${azurerm_application_insights.flight_tracker.instrumentation_key}"
  }
  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"               = "dotnet-isolated"
    "SCM_DO_BUILD_DURING_DEPLOYMENT"         = "0"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = "1",
    "CosmosConnectionString"                 = azurerm_cosmosdb_account.flight_tracker.primary_sql_connection_string
    "CosmosDatabase"                         = azurerm_cosmosdb_sql_database.telemetry.name
    "EventHubConnectionString"               = azurerm_eventhub_authorization_rule.flight_tracker_sender.primary_connection_string
    "EventHubName"                           = azurerm_eventhub.flight_tracker.name
  }
}