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

variable "resource_short_name" {
  type        = string
  default     = "flttrack"
  description = "The short name of the resources to be created."
}

# ############################################################################
# Log Analytics Workspace values
# ############################################################################

variable "log_analytics_workspace_sku" {
  type        = string
  default     = "PerGB2018"
  description = "The SKU of the Log Analytics Workspace."
}

variable "log_analytics_workspace_retention_in_days" {
  type        = number
  default     = 30
  description = "The retention period of the Log Analytics Workspace."
}

# ############################################################################
# Azure Communication Services values
# ############################################################################

variable "communication_services_data_location" {
  type        = string
  description = "The location where the Communication Services data is stored at rest."
}

# ############################################################################
# SQL Server values
# ############################################################################

variable "sql_admin_password" {
  type        = string
  description = "The password for the SQL Server admin."
}

# ############################################################################
# Function App values
# ############################################################################

variable "submit_flight_plan_name" {
  type        = string
  default     = "flightplan"
  description = "The name of the Function App that submits flight plans."
}

variable "process_telemetry_name" {
  type        = string
  default     = "processtlm"
  description = "The name of the Function App that processes telemetry."
}

variable "receive_telemetry_name" {
  type        = string
  default     = "receivetlm"
  description = "The name of the Function App that receives telemetry."
}

# ############################################################################
# Submit Flight Plan values
# ############################################################################

variable "submit_flight_plan_sender_email" {
  type        = string
  description = "The email address to use as the sender when sending notifications."
}

variable "submit_flight_plan_receiver_email" {
  type        = string
  description = "The email address to use as the receiver when sending notifications."
}

variable "sumbit_flight_plan_approval_timeout" {
  type        = number
  default     = 5
  description = "The number of minutes to wait for an approval before timing out."
}