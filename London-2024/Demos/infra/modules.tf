module "azure_regions" {
  source = "github.com/TaleLearnCode/terraform-azure-regions"
  azure_region = var.azure_region
}

module "resource_group" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "resource-group"
}

module "event_hubs_namespace" {
  source = "git::git@ssh.dev.azure.com:v3/JasperEnginesTransmissions/JETDEV/TerraformModule_AzureResourceTypes"
  resource_type = "event-hubs-namespace"
}