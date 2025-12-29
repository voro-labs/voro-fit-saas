import { ContactDto } from "./contact.interface"
import { GroupDto } from "./group.interface"
import { InstanceDto } from "./instance.interface"
import { MessageDto } from "./message.interface"

export interface ChatDto {
  id: string
  remoteJid: string
  isGroup: boolean
  instanceId: string
  instance: InstanceDto
  lastMessageAt: Date
  updatedAt: Date
  contactId?: string
  contact?: ContactDto
  groupId?: string
  group?: GroupDto
  messages: MessageDto[]
}
