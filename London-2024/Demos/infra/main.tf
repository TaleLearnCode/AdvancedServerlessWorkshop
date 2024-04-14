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