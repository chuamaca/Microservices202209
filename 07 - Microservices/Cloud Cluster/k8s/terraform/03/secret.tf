
# crea un service principal y rellena los siguientes datos para autenticar
provider "azurerm" {
  features {}
  subscription_id = "48b47fa2-e2d4-4eb4-92be-f7410898af0b"
  client_id       = "934d71bc-e765-4ecf-b4b0-99cd7aacb078" # appID
  client_secret   = "Sif89SzqtPf~7dk6zOC6gK1LH9YdGLK4Ar"   # password
  tenant_id       = "359f9a7d-18ce-42bf-80c8-7cff9e947cec" # tenant
}