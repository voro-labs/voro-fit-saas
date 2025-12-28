import { InstanceExtensionDto } from "./instanceExtensionDto.interface"

export interface InstanceDto {
  id: string
  name: string
  instanceExtension: InstanceExtensionDto | null
}
