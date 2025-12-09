import { MessageStatusEnum } from "../Enums/messageStatusEnum.enum"
import type { ContactDto } from "./contactDto.interface"

export interface MessageReactionDto {
  reaction: string
  fromMe: boolean
  createdAt: Date
}

export interface MessageDto {
  id: string
  content: string
  base64?: string
  sentAt: Date
  status: MessageStatusEnum
  isFromMe: boolean
  contactId: string
  contact?: ContactDto
  quotedMessage?: MessageDto
  quotedMessageId?: string
  messageReactions: MessageReactionDto[]
}
