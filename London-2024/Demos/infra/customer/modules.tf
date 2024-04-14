module "azure_regions" {
  source = "github.com/TaleLearnCode/terraform-azure-regions"
  azure_region = var.azure_region
}

module "resource_group" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "resource-group"
}

module "cosmos" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "azure-cosmos-db-for-nosql-account"
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

module "application_insights" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "application-insights"
}