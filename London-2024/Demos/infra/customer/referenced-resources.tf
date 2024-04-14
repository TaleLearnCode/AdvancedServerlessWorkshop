data "azurerm_resource_group" "flight_tracker" {
  name     = "${module.resource_group.name.abbreviation}-${var.resource_name}${var.random_identifier}-${var.azure_environment}-${module.azure_regions.region.region_short}"
}

data "azurerm_cosmosdb_account" "flight_tracker" {
  name                = "${module.cosmos.name.abbreviation}-${var.resource_name}${var.random_identifier}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = data.azurerm_resource_group.flight_tracker.name
}

data "azurerm_cosmosdb_sql_database" "telemetry" {
  name                = "telemetry"
  resource_group_name = data.azurerm_resource_group.flight_tracker.name
  account_name        = data.azurerm_cosmosdb_account.flight_tracker.name
}

data "azurerm_storage_account" "flight_tracker" {
  name                     = "${module.storage_account.name.abbreviation}${var.resource_short_name}${var.random_identifier}${var.azure_environment}${module.azure_regions.region.region_short}"
  resource_group_name      = data.azurerm_resource_group.flight_tracker.name
}

data "azurerm_application_insights" "flight_tracker" {
  name                = "${module.application_insights.name.abbreviation}-${var.resource_name}${var.random_identifier}-${var.azure_environment}-${module.azure_regions.region.region_short}"
  resource_group_name = data.azurerm_resource_group.flight_tracker.name
}