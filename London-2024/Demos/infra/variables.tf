#############################################################################
# Environmental Variables
#############################################################################

variable "azure_region" {
  type        = string
  description = "Location where to create the Azure resources."
}

variable "azure_environment" {
  type        = string
  description = "The deployment environment of the Azure resouces being created."
}


#############################################################################
# Tag values
#############################################################################

variable "tag_product" {
  type        = string
  default     = "Flight Tracker"
  description = "The product or service that the resources are being created for."
}

variable "tag_cost_center" {
  type        = string
  default     = "Advanced Serverless Workshop"
  description = "The cost center that the resources are being created for."
}

variable "tag_criticality" {
  type        = string
  default     = "Medium"
  description = "The criticality of the resources being created."
}

variable "tag_disaster_recovery" {
  type        = string
  default     = "Dev"
  description = "Business criticality of the application, workload, or service. Valid values are Mission Critical, Critical, Essential, Dev."
}

# ############################################################################
# General values
# ############################################################################

variable "resource_name" {
  type        = string
  default     = "flighttracker"
  description = "The name of the resources to be created."
}