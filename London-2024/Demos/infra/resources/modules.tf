module "azure_regions" {
  source = "github.com/TaleLearnCode/terraform-azure-regions"
  azure_region = var.azure_region
}

module "resource_group" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "resource-group"
}

module "managed_identity" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "managed-identity"
}

module "cosmos" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "azure-cosmos-db-for-nosql-account"
}

module "key_vault" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "key-vault"
}

module "app_config" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "app-configuration-store"
}

module "log_analytics_workspace" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "log-analytics-workspace"
}

module "application_insights" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "application-insights"
}

module "communication_services" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "communication-services"
}

module "event_hubs_namespace" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "event-hubs-namespace"
}

module "sql_server" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "azure-sql-database-server"
}

module "sql_database" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "azure-sql-database"
}

module "storage_account" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "storage-account"
}

module "app_service_plan" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "app-service-plan"
}

module "function_app" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "function-app"
}

module "container_apps_environment" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "container-apps-environment"
}