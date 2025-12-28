import { InstanceStatusEnum } from "../Enums/instanceStatusEnum.enum"
import { InstanceDto } from "./instanceDto.interface"
import { ChatDto } from "./chatDto.interface"

export interface InstanceExtensionDto {
  instanceId: string
  instance: InstanceDto
  base64?: string
  hash?: string
  integration?: string
  phoneNumber?: string
  status?: InstanceStatusEnum
  createdAt?: string
  updatedAt?: string
  connectedAt?: string
  chats: ChatDto[]
}