import { InstanceExtensionDto } from "./instance-extension.interface"

export interface InstanceDto {
  id: string
  name: string
  instanceExtension: InstanceExtensionDto | null
}
